using N5.Core.Repository;
using System.Collections.Generic;

namespace N5.Entities.Customer.Seeds
{
    public class CustomerFieldSeeder : IDbSeed
    {
        private IRepository<CustomerField, EntityFrameworkCustomerContext> _customerRepository;
        private IList<CustomerField> _defaultValues = new List<CustomerField>()
        {
            new CustomerField() { Id = 1, CustomerId = 1, FieldName = "nombre", FieldTypeDescription = "System.String" },
            new CustomerField() { Id = 2, CustomerId = 1, FieldName = "apellido", FieldTypeDescription = "System.String" },
            new CustomerField() { Id = 3, CustomerId = 1, FieldName = "avatar", FieldTypeDescription = "System.Byte[]" },
            new CustomerField() { Id = 4, CustomerId = 1, FieldName = "fecha_de_nacimiento", FieldTypeDescription = "System.DateTime" },
            new CustomerField() { Id = 5, CustomerId = 1, FieldName = "telefono", FieldTypeDescription = "System.String" },
            new CustomerField() { Id = 6, CustomerId = 1, FieldName = "celular", FieldTypeDescription = "System.String" },
            new CustomerField() { Id = 7, CustomerId = 1, FieldName = "email", FieldTypeDescription = "System.String" },
            new CustomerField() { Id = 8, CustomerId = 1, FieldName = "edad", FieldTypeDescription = "System.Int32" }
        };

        public CustomerFieldSeeder(IRepository<CustomerField, EntityFrameworkCustomerContext> customerRepository)
        {
            _customerRepository = customerRepository;
            _customerRepository.Truncate();
        }       

        public void Generate()
        {            
            foreach (var customerField in _defaultValues)
            {
                _customerRepository.Add(customerField);
            }
        }
    }
}