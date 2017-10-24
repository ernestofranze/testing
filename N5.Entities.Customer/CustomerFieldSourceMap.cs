using N5.Entities.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace N5.Entities.Customer
{
    [Table("customerfieldsourcemap")]
    public class CustomerFieldSourceMap
    {
        public CustomerFieldSourceMap()
        {
        }

        public long CustomerFieldID { get; set; }
        public long SourceFieldID { get; set; }

        public CustomerField CustomerField { get; set; }
        public SourceField SourceField { get; set; }

        public int Order { get; set; }
    }
}
