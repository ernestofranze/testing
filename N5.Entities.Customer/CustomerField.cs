using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5.Entities.Customer
{
    [Table("customerfield")]
    public class CustomerField
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

        [Column("customerId")]
        public long CustomerId { get; set; }

        public Customer Customer { get; set; }

        public virtual ICollection<CustomerFieldSourceMap> SourceFields { get; set; }

        [NotMapped]
        public Type FieldType
        {
            get { return Type.GetType(_fieldType); }
        }
        
        public CustomerField(Customer customer, string fieldName, string fieldType)
        {
            _fieldName = fieldName;
            _fieldType = fieldType;
            Customer = customer;
        }

        public CustomerField()
        {
        }
    }
}