using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5.Entities.Source
{
    [Table("source")]
    public class Source
    {
        public Source()
        {
        }

        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("description")]
        public string Description { get; set; }
        
        public IEnumerable<SourceField> SourceFields { get; set; }
    }
}
