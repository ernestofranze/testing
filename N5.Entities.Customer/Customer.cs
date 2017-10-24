using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5.Entities.Customer
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
        }

        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("description")]
        public string Description { get; set; }

        public IEnumerable<CustomerField> CustomerFields { get; set; }
    }
}
