using N5.Confluent.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace N5.Entities.Customer.DataTransfer.DTO
{
    [DataContract(Name = "MappedCustomerMessage", Namespace = "Customer")]
    public class MappedCustomerMessage : KafkaMessage
    {
        [DataMember(Name = "CustomerId")]
        public long CustomerId { get; set; }

        [DataMember(Name = "Data")]
        public Dictionary<string, Dictionary<string, object>> Data { get; set; } 
    }
}
