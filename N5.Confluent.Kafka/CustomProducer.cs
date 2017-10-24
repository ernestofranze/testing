using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using N5.Core;
using N5.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace N5.Confluent.Kafka
{
    public class CustomProducer<TKey,TValue> : IDisposable
    {
        private ISerializer<TValue> _valueSerializer;
        private ISerializer<TKey> _keySerializer;
        private Producer<TKey, TValue> _producer;
        public CustomProducer(ISerializer<TValue> valueSerializer, ISerializer<TKey> keySerializer)
        {
            _valueSerializer = valueSerializer;
            _keySerializer = keySerializer;
            _producer = new Producer<TKey, TValue>(AppConfiguration.GetDictionary(ConfigurationKeys.KAFKAPRODUCERKEY), _keySerializer, _valueSerializer);
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        public async Task<Message<TKey,TValue>> Produce(string topic, TKey key, TValue value)
        {
            return await _producer.ProduceAsync(topic, key, value);
        }
    }
}
