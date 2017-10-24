using N5.Core;
using N5.Entities.Customer.Seeds;
using System.Collections.Generic;

namespace N5.Entities.Customer
{
    public class CustomerInitialize : IInitialize
    {
        private IEnumerable<IDbSeed> _seeds;
        public CustomerInitialize(IEnumerable<IDbSeed> seeds)
        {
            _seeds = seeds;
        }
        public void Initialize()
        {
            using (var db = new EntityFrameworkCustomerContext())
            {
                db.Database.EnsureCreated();                
                db.SaveChanges();
            }
            CustomerContainerBuilder.Build();
        }

        public void Seed()
        {
            foreach (var seed in _seeds)
            {
                seed.Generate();
            }
        }
    }
}
