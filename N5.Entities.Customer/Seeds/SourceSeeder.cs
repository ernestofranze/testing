using N5.Core.Repository;
using System.Collections.Generic;

namespace N5.Entities.Customer.Seeds
{
    public class SourceSeeder : IDbSeed
    {
        private IRepository<Source.Source, EntityFrameworkCustomerContext> _customerRepository;
        private IList<string> _defaultValues = new List<string>()
        {
            "CORE",
            "SAP",
            "CC_System",
            "Loans_System",
            "ERP"
        };

        public SourceSeeder(IRepository<Source.Source, EntityFrameworkCustomerContext> customerRepository)
        {
            _customerRepository = customerRepository;
            _customerRepository.Truncate();
        }        

        public void Generate()
        {
            var index = 1;
            foreach (var source in _defaultValues)
            {
                _customerRepository.Add(new Source.Source() { Id = index, Description = source });
                index++;
            }
        }
    }
}
