using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace N5.Entities.Product
{
    [Table("ProductTypeField")]
    public class ProductTypeField
    {
        private string _fieldType;
        private string _fieldName;

        [Column("Id")]
        [Key]
        public long Id { get; set; }

        [Column("Fieldname")]
        public string FieldName { get { return _fieldName; } set { _fieldName = value; } }

        [Column("Fieldtype")]
        public string FieldTypeDescription
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        [Column("ProductTypeId")]
        public long ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }
        
        [NotMapped]
        public Type FieldType
        {
            get { return Type.GetType(_fieldType); }
        }

        public ProductTypeField()
        {
        }
    }
}
