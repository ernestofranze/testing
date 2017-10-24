using System.Runtime.Serialization;

namespace N5.Confluent.Core
{
    [DataContract]
    public abstract class KafkaMessage
    {
        [DataMember(Name = "SourceId")]
        public long SourceId { get; set; }
    }
}
