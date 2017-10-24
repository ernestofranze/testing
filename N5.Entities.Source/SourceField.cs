using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5.Entities.Source
{
    [Table("sourcefield")]
    public class SourceField
    {
        private string _fieldType;
        private string _fieldName;

        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("fieldname")]
        public string FieldName { get { return _fieldName; } set { _fieldName = value; } }

        [Column("fieldtype")]
        public string FieldTypeDescription
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        [Column("sourceId")]
        public long SourceId { get; set; }

        public Source Source { get; set; }

        public SourceField(Source source, string fieldName, string fieldType)
        {
            Source = source;
            _fieldName = fieldName;
            _fieldType = fieldType;
        }

        public SourceField()
        {
        }
    }
}
