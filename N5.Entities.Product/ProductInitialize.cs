using N5.Core;
using N5.Entities.Product.Seeds;
using System.Collections.Generic;

namespace N5.Entities.Product
{
    public class ProductInitialize : IInitialize
    {
        private IEnumerable<IDbSeed> _seeds;
        public ProductInitialize(IEnumerable<IDbSeed> seeds)
        {
            _seeds = seeds;
        }
        public void Initialize()
        {
            using (var db = new EntityFrameworkProductContext())
            {
                db.Database.EnsureCreated();
                db.SaveChanges();
            }
            ProductContainerBuilder.Build();
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
