using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace N5.Entities.Customer
{
    public class CustomerDocument
    {
        public string Id { get; set; }

        public CustomerDocument(string id)
        {
            Id = id;
            Data = new Dictionary<string, object>();
        }
        public JObject Full { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
