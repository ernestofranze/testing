using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace N5.Entities.Product
{
    [Table("ProductType")]
    public class ProductType
    {
        public ProductType()
        {
        }

        [Column("Id")]
        [Key]
        public long Id { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        public IEnumerable<ProductTypeField> ProductTypeFields { get; set; }
    }
}
