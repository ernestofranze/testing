using System.Runtime.Serialization;

namespace N5.DataAccess.DTO
{
    [DataContract]
    public class Query
    {
        [DataMember(Name = "bool")]
        public Bool Bool { get; set; }
    }
}
