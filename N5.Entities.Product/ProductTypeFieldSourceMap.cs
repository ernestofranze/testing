using N5.Entities.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace N5.Entities.Product
{
    [Table("ProductTypeFieldSourceMap")]
    public class ProductTypeFieldSourceMap
    {
        public ProductTypeFieldSourceMap()
        {
        }

        public long ProductTypeFieldID { get; set; }
        public long SourceFieldID { get; set; }

        public ProductTypeField ProductTypeField { get; set; }
        public SourceField SourceField { get; set; }

        public int Order { get; set; }
    }
}