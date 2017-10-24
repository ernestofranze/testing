using System.Collections.Generic;
using System.Runtime.Serialization;

namespace N5.DataAccess.DTO
{
    [DataContract]
    public class Range : IMust
    {
        [DataMember(Name = "range")]
        public Dictionary<string, Dictionary<string, object>> RangeFields { get; set; }
    }
}
