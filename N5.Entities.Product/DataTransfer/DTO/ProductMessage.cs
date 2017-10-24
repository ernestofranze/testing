using N5.Confluent.Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace N5.Entities.Product.DataTransfer.DTO
{
    [DataContract(Name = "ProductMessage", Namespace = "Product")]
    public class ProductMessage : KafkaMessage
    {
        [DataMember(Name = "ProductId")]
        public long ProductId { get; set; }

        [DataMember(Name = "CustomerId")]
        public long CustomerId { get; set; }

        [DataMember(Name = "Data")]
        public Dictionary<string, string> Data { get; set; }

        [DataMember(Name = "ArrayData")]
        public Dictionary<string, string[]> ArrayData { get; set; }
    }
}