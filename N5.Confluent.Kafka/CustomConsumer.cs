using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using N5.Core.Configuration;
using System;

namespace N5.Confluent.Kafka
{
    public class CustomConsumer<TKey, TValue>
    {
        private IDeserializer<TValue> _valueDeserializer;
        private IDeserializer<TKey> _keyDeserializer;
        private bool done = false;

        public CustomConsumer(IDeserializer<TValue> valueDeserializer, IDeserializer<TKey> keyDeserializer)
        {
            _valueDeserializer = valueDeserializer;
            _keyDeserializer = keyDeserializer;
        }
        
        public void Consume(string topic, EventHandler<Message<TKey,TValue>> onRecieved)
        {
            using (var consumer = new Consumer<TKey, TValue>(AppConfiguration.GetDictionary(ConfigurationKeys.KAFKACONSUMERKEY), _keyDeserializer, _valueDeserializer))
            {
                consumer.OnMessage += onRecieved;

                consumer.OnPartitionEOF += (_, end)
                    =>
                {
                    Console.WriteLine($"Reached end of topic {end.Topic} partition {end.Partition}, next message will be at offset {end.Offset}");
                };

                consumer.OnError += (_, error)
                    => Console.WriteLine($"Error: {error}");

                consumer.OnConsumeError += (_, msg)
                    => Console.WriteLine($"Error consuming from topic/partition/offset {msg.Topic}/{msg.Partition}/{msg.Offset}: {msg.Error}");

                consumer.OnOffsetsCommitted += (_, commit) =>
                {
                    Console.WriteLine($"[{string.Join(", ", commit.Offsets)}]");

                    if (commit.Error)
                    {
                        Console.WriteLine($"Failed to commit offsets: {commit.Error}");
                    }
                    Console.WriteLine($"Successfully committed offsets: [{string.Join(", ", commit.Offsets)}]");
                };

                consumer.OnPartitionsAssigned += (_, partitions) =>
                {
                    Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}], member id: {consumer.MemberId}");
                    consumer.Assign(partitions);
                };

                consumer.OnPartitionsRevoked += (_, partitions) =>
                {
                    Console.WriteLine($"Revoked partitions: [{string.Join(", ", partitions)}]");
                    consumer.Unassign();
                };

                consumer.OnStatistics += (_, json)
                    => Console.WriteLine($"Statistics: {json}");

                consumer.Subscribe(topic);
                
                while (!done)
                {
                    consumer.Poll(TimeSpan.FromMilliseconds(100));
                }
            }   
        }
    }
}
