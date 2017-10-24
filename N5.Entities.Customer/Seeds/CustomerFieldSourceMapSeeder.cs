using N5.Core.Repository;
using System.Collections.Generic;

namespace N5.Entities.Customer.Seeds
{
    public class CustomerFieldSourceMapSeeder : IDbSeed
    {                
        private IRepository<CustomerFieldSourceMap, EntityFrameworkCustomerContext> _repository;
        private IList<CustomerFieldSourceMap> _defaultValues = new List<CustomerFieldSourceMap>()
        {
            new CustomerFieldSourceMap() { CustomerFieldID = 1, SourceFieldID = 1, Order = 1 },
            new CustomerFieldSourceMap() { CustomerFieldID = 1, SourceFieldID = 9, Order = 2 },
            new CustomerFieldSourceMap() { CustomerFieldID = 5, SourceFieldID = 3, Order = 1 },
            new CustomerFieldSourceMap() { CustomerFieldID = 8, SourceFieldID = 2, Order = 1 }
        };

        public CustomerFieldSourceMapSeeder(IRepository<CustomerFieldSourceMap, EntityFrameworkCustomerContext> repository)
        {
            _repository = repository;
            _repository.Truncate();
        }        

        public void Generate()
        {
            foreach (var customerFieldSourceMap in _defaultValues)
            {
                _repository.Add(customerFieldSourceMap);
            }
        }
    }
}
