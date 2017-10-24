using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace N5.Entities.Product.DataTransfer.DTO
{
    public class MappedProductMessage
    {
        [DataMember(Name = "ProductId")]
        public long ProductId { get; set; }

        [DataMember(Name = "CustomerId")]
        public long CustomerId { get; set; }

        [DataMember(Name = "Data")]
        public Dictionary<string, Dictionary<string, object>> Data { get; set; }
    }
}
