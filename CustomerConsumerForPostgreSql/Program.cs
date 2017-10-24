using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Judo.Kafka;
using Judo.SchemaRegistryClient;
using Microsoft.Hadoop.Avro;
using N5.Confluent.Kafka;
using N5.Core.Configuration;
using N5.Entities.Customer.DataTransfer.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerConsumerForPostgreSql
{
    class Program
    {
        private static readonly string _bootstrapServers = AppConfiguration.GetConfiguration(ConfigurationKeys.BootstrapServers);
        private static readonly string _schemaRegistryUri = AppConfiguration.GetConfiguration(ConfigurationKeys.SchemaRegistryUri);
        private static readonly string _topic = "customerKafkaMessages";

        static void Main(string[] args)
        {
            Console.WriteLine("-- CONSUMER FOR POSTGRES --");
            var consumer = new CustomConsumer<string, MappedCustomerMessage>(new ObjectSerializer<MappedCustomerMessage>(), new ObjectSerializer<string>());

            var schemaRegistryClient = new CachedSchemaRegistryClient(_schemaRegistryUri, 200);
            var judaSerializer = new SchemaRegistryAvroSerializer(schemaRegistryClient, false);
            var valueSerializer = new AvroSerializer<CustomerKafkaMessage>(judaSerializer, _topic, false);
            var keySerializer = new AvroSerializer<string>(judaSerializer, _topic, true);

            var config = new Dictionary<string, object>
            {
                ["bootstrap.servers"] = _bootstrapServers
            };

            consumer.Consume("customers", (_, msg) =>
            {
                using (var producer = new Producer<string, CustomerKafkaMessage>(config, keySerializer, valueSerializer))
                {                    
                    var kafkaMessage = new CustomerKafkaMessage
                    {                        
                        CustomerId = Int64.Parse(msg.Key),
                        Value = JsonConvert.SerializeObject(msg.Value)
                    };

                    producer.ProduceAsync(_topic, msg.Key, kafkaMessage).ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine(task.Exception.Message);
                        }
                        else if (task.Result.Error.HasError)
                        {
                            Console.WriteLine(task.Result.Error);
                        }
                        Console.WriteLine(kafkaMessage.ToString());
                    });                   
                    Console.WriteLine("remaining: " + producer.Flush(10000));
                }
            });
        }

        public class CustomerKafkaMessage
        {                        
            public long CustomerId { get; set; }
            public string Value { get; set; }

            public override string ToString()
                => $"CustomerId={CustomerId}, Value={Value}";
        }

        public class AvroSerializer<T> : ISerializer<T>
        {
            private IKafkaSerialzier _kafkaSerializer;
            private string _topic;
            private bool _isKey;

            public AvroSerializer(IKafkaSerialzier kafkaSerializer, string topic, bool isKey)
            {
                _kafkaSerializer = kafkaSerializer;
                _topic = topic;
                _isKey = isKey;
            }
            public byte[] Serialize(T data)
            {
                return _kafkaSerializer.SerializeAsync(data, _isKey, _topic).Result;
            }
        }

        public class AvroDeserializer<T> : IDeserializer<T>
        {
            private IKafkaSerialzier _kafkaSerializer;
            private string _topic;
            private bool _isKey;

            public AvroDeserializer(IKafkaSerialzier kafkaSerializer, string topic, bool isKey)
            {
                _kafkaSerializer = kafkaSerializer;
                _topic = topic;
                _isKey = isKey;
            }

            public T Deserialize(byte[] data)
            {
                return _kafkaSerializer.DeserializeAsync<T>(data, _isKey, _topic).Result;
            }
        }

        public class SchemaRegistryAvroSerializer : IKafkaSerialzier
        {
            private readonly ISchemaRegistryClient _schemaRegistryClient;
            private readonly ConcurrentDictionary<Type, object> _serializerCache = new ConcurrentDictionary<Type, object>();
            private readonly bool _useAvroDataContractResolver;


            public SchemaRegistryAvroSerializer(ISchemaRegistryClient schemaRegistryClient, bool useAvroDataContractResolver)
            {
                _schemaRegistryClient = schemaRegistryClient;
                _useAvroDataContractResolver = useAvroDataContractResolver;
            }

            public Task<TPayload> DeserializeAsync<TPayload>(byte[] payload, bool isKey, string topic)
            {
                var subject = GetSubjectName(topic, isKey);
                var serializer = GetSerializer<TPayload>();
                using (var stream = new MemoryStream(payload))
                {
                    stream.Seek(sizeof(byte) + sizeof(uint), SeekOrigin.Begin);
                    return Task.FromResult(serializer.Deserialize(stream));
                }
            }

            public async Task<byte[]> SerializeAsync<TPayload>(TPayload payload, bool isKey, string topic)
            {
                var subject = GetSubjectName(topic, isKey);
                var serializer = GetSerializer<TPayload>();

                var schemaId = await _schemaRegistryClient.RegisterAsync(subject, serializer.ReaderSchema);
                var uintSchemaId = Convert.ToUInt32(schemaId);
                if (BitConverter.IsLittleEndian)
                {
                    uintSchemaId = SwapEndianness(uintSchemaId);
                }

                using (var stream = new MemoryStream())
                {
                    var sw = new BinaryWriter(stream);
                    sw.Write((byte)0x0);
                    sw.Write(uintSchemaId);
                    sw.Flush();
                    serializer.Serialize(stream, payload);
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream.ToArray();
                }
            }

            private IAvroSerializer<TPayload> GetSerializer<TPayload>()
            {
                var serializer = (IAvroSerializer<TPayload>)_serializerCache.GetOrAdd(typeof(TPayload),
                    type => AvroSerializer.Create<TPayload>(new AvroSerializerSettings
                    {
                        Resolver =
                            _useAvroDataContractResolver
                                ? (AvroContractResolver)new AvroDataContractResolver(true)
                                : new AvroPublicMemberContractResolver(true),
                        Surrogate = new AvroSurrogateStrategy()
                    }));

                return serializer;
            }

            private string GetSubjectName(string topic, bool isKey)
            {
                return topic + (isKey ? "-key" : "-value");
            }

            uint SwapEndianness(uint x)
            {
                return ((x & 0x000000ff) << 24) +  // First byte
                       ((x & 0x0000ff00) << 8) +   // Second byte
                       ((x & 0x00ff0000) >> 8) +   // Third byte
                       ((x & 0xff000000) >> 24);   // Fourth byte
            }
        }

        public class AvroSurrogateStrategy : IAvroSurrogate
        {
            private static readonly IAvroSurrogateStrategy[] Strategies = new IAvroSurrogateStrategy[] { new DateTimeSurrogate(), new GuidSurrogate() };
            public object GetDeserializedObject(object obj, Type targetType)
            {
                var surrogate = GetStrategy(targetType);
                if (surrogate != null)
                {
                    return surrogate.GetDeserializedObject(obj, targetType);
                }

                return obj;
            }

            public object GetObjectToSerialize(object obj, Type targetType)
            {
                var surrogate = GetStrategy(obj.GetType());
                if (surrogate != null)
                {
                    return surrogate.GetObjectToSerialize(obj, targetType);
                }

                return obj;
            }

            public Type GetSurrogateType(Type type)
            {
                var surrogate = GetStrategy(type);
                if (surrogate != null)
                {
                    return surrogate.GetSurrogateType(type);
                }

                return type;
            }

            private IAvroSurrogate GetStrategy(Type targetType)
            {
                return Strategies.FirstOrDefault(s => s.SurrogateFor(targetType));
            }
        }

        interface IAvroSurrogateStrategy : IAvroSurrogate
        {
            bool SurrogateFor(Type type);
        }

        public class DateTimeSurrogate : IAvroSurrogateStrategy
        {
            private static readonly Type[] DateTypes = new[] {
            typeof(DateTime),
            typeof(DateTimeOffset) };

            internal const string IsoFormat = "yyyy-MM-dd HH:mm:ss.fffzzz";

            public object GetDeserializedObject(object obj, Type targetType)
            {
                if (SurrogateFor(targetType) && obj is string)
                {
                    var date = DateTime.ParseExact((string)obj, IsoFormat, CultureInfo.InvariantCulture);
                    if (targetType == typeof(DateTimeOffset))
                    {
                        return new DateTimeOffset(date);
                    }
                    else
                    {
                        return date;
                    }
                }

                return obj;
            }

            public object GetObjectToSerialize(object obj, Type targetType)
            {
                if (SurrogateFor(obj.GetType()))
                {
                    if (obj is DateTime)
                    {
                        return ((DateTime)obj).ToString(IsoFormat);
                    }
                    else
                    {
                        return ((DateTimeOffset)obj).ToString(IsoFormat);
                    }

                }

                return obj;
            }

            public Type GetSurrogateType(Type type)
            {
                if (SurrogateFor(type))
                {
                    return typeof(string);
                }

                return type;
            }

            public bool SurrogateFor(Type type)
            {
                return DateTypes.Contains(type);
            }
        }

        public class GuidSurrogate : IAvroSurrogateStrategy
        {

            private static readonly Type[] GuidTypes = new[] { typeof(Guid), typeof(Guid?) };

            public object GetDeserializedObject(object obj, Type targetType)
            {
                if (IsGuid(targetType))
                {
                    return Guid.Parse(obj.ToString());
                }

                return obj;
            }

            public object GetObjectToSerialize(object obj, Type targetType)
            {
                if (IsGuid(obj.GetType()))
                {
                    return obj?.ToString();
                }

                return obj;
            }

            public Type GetSurrogateType(Type type)
            {
                if (IsGuid(type))
                {
                    return typeof(string);
                }
                return type;
            }

            public bool SurrogateFor(Type type)
            {
                return IsGuid(type);
            }

            private bool IsGuid(Type type)
            {
                return GuidTypes.Contains(type);
            }
        }
    }   
}