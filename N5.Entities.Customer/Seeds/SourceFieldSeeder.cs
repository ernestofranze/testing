using N5.Core.Repository;
using N5.Entities.Source;
using System.Collections.Generic;

namespace N5.Entities.Customer.Seeds
{
    public class SourceFieldSeeder : IDbSeed
    {
        private IRepository<SourceField, EntityFrameworkCustomerContext> _customerRepository;
        private IList<SourceField> _defaultValues = new List<SourceField>()
        {
            new SourceField() { Id = 1, SourceId = 1, FieldName = "name", FieldTypeDescription = "System.String" },
            new SourceField() { Id = 2, SourceId = 1, FieldName = "age", FieldTypeDescription = "System.Int32" },
            new SourceField() { Id = 3, SourceId = 1, FieldName = "phone", FieldTypeDescription = "System.String" },
            new SourceField() { Id = 4, SourceId = 1, FieldName = "mobile_phone", FieldTypeDescription = "System.String" },
            new SourceField() { Id = 5, SourceId = 1, FieldName = "email", FieldTypeDescription = "System.String" },
            new SourceField() { Id = 6, SourceId = 2, FieldName = "birthdate", FieldTypeDescription = "System.DateTime" },
            new SourceField() { Id = 7, SourceId = 2, FieldName = "address", FieldTypeDescription = "System.String" },
            new SourceField() { Id = 8, SourceId = 2, FieldName = "is_client", FieldTypeDescription = "System.Boolean" },
            new SourceField() { Id = 9, SourceId = 3, FieldName = "name", FieldTypeDescription = "System.String" }
        };

        public SourceFieldSeeder(IRepository<SourceField, EntityFrameworkCustomerContext> customerRepository)
        {
            _customerRepository = customerRepository;
            _customerRepository.Truncate();
        }        

        public void Generate()
        {            
            foreach (var sourceField in _defaultValues)
            {
                _customerRepository.Add(sourceField);                
            }
        }
    }
}