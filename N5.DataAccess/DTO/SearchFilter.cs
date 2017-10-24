using System.Runtime.Serialization;

namespace N5.DataAccess.DTO
{
    [DataContract]
    public class SearchFilter
    {
        [DataMember(Name = "query")]
        public Query Query { get; set; }
    }
}
