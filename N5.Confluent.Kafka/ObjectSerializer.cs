using Confluent.Kafka.Serialization;
using N5.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5.Confluent.Kafka
{
    public class ObjectSerializer<T> : ISerializer<T>,IDeserializer<T> where T : class
    {
        private JsonSerializer _jsonSerializer;

        public ObjectSerializer()
        {
            _jsonSerializer = new JsonSerializer();
        }

        public T Deserialize(byte[] data)
        {
            return _jsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(data));
        }

        public byte[] Serialize(T data)
        {
            return Encoding.UTF8.GetBytes(_jsonSerializer.Serialize(data));
        }
    }
}
