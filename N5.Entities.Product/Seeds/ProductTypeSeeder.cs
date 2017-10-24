using N5.Core.Repository;
using System.Collections.Generic;

namespace N5.Entities.Product.Seeds
{
    public class ProductTypeSeeder : IDbSeed
    {
        private IRepository<ProductType, EntityFrameworkProductContext> _productRepository;
        private IList<ProductType> _defaultValues = new List<ProductType>()
        {
            new ProductType(){ Id = 1, Description = "Préstamos" },
            new ProductType(){ Id = 2, Description = "Seguros" }
        };

        public ProductTypeSeeder(IRepository<ProductType, EntityFrameworkProductContext> productRepository)
        {
            _productRepository = productRepository;
            _productRepository.Truncate();
        }

        public void Generate()
        {
            foreach (var productType in _defaultValues)
            {
                _productRepository.Add(productType);
            }
        }
    }
}