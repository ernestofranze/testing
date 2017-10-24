using Confluent.Kafka.Serialization;
using Microsoft.Hadoop.Avro;
using N5.Confluent.Core;
using System.IO;

namespace N5.Confluent.Kafka
{
    public class Serializer<T> : ISerializer<T>, IDeserializer<T> where T : class
    {
        private IAvroSerializer<T> _serializer;

        public Serializer()
        {
            _serializer = AvroSerializer.Create<T>();
        }

        public T Deserialize(byte[] data)
        {
            using (var buffer = new MemoryStream(data))
            {
                return _serializer.Deserialize(buffer);
            }
        }

        public byte[] Serialize(T data)
        {
            using (var buffer = new MemoryStream())
            {
                _serializer.Serialize(buffer, data);

                return buffer.ToArray();
            }
        }
    }
}
