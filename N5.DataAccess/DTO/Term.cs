using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace N5.DataAccess.DTO
{
    [DataContract]
    public class Term : IMust
    {
        [DataMember(Name = "term")]
        public IDictionary<string, object> TermField { get; set; }
    }
}
