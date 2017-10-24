using System.Collections.Generic;
using System.Runtime.Serialization;

namespace N5.DataAccess.DTO
{
    [DataContract]
    public class Bool
    {
        [DataMember(Name = "must")]
        public IEnumerable<IMust> Must { get; set; }
    }
}
