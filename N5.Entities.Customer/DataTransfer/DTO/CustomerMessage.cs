using N5.Confluent.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace N5.Entities.Customer.DataTransfer.DTO
{
    [DataContract(Name = "CustomerMessage", Namespace = "Customer")]
    public class CustomerMessage : KafkaMessage
    {
        [DataMember(Name = "CustomerId")]
        public long CustomerId { get; set; }

        [DataMember(Name = "Data")]
        public Dictionary<string, string> Data { get; set; }

        [DataMember(Name = "ArrayData")]
        public Dictionary<string,string[]> ArrayData { get; set; }
    }
}
