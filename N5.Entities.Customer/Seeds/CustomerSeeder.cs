using N5.Core.Repository;
using System.Collections.Generic;

namespace N5.Entities.Customer.Seeds
{
    public class CustomerSeeder : IDbSeed
    {
        private IRepository<Customer, EntityFrameworkCustomerContext> _customerRepository;
        private IList<string> _defaultValues = new List<string>()
        {
            "Persona física"
        };

        public CustomerSeeder(IRepository<Customer, EntityFrameworkCustomerContext> customerRepository)
        {
            _customerRepository = customerRepository;
            _customerRepository.Truncate();
        }        

        public void Generate()
        {
            var index = 1;
            foreach (var source in _defaultValues)
            {
                _customerRepository.Add(new Customer() { Id = index, Description = source });
                index++;
            }
        }
    }
}
