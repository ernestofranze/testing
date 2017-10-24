using N5.Core.Repository;
using System.Collections.Generic;

namespace N5.Entities.Product.Seeds
{
    public class ProductTypeFieldSourceMapSeeder : IDbSeed
    {
        private IRepository<ProductTypeFieldSourceMap, EntityFrameworkProductContext> _productRepository;
        private IList<ProductTypeFieldSourceMap> _defaultValues = new List<ProductTypeFieldSourceMap>()
        {
            new ProductTypeFieldSourceMap() { ProductTypeFieldID = 1, SourceFieldID = 1, Order = 1 },
            new ProductTypeFieldSourceMap() { ProductTypeFieldID = 2, SourceFieldID = 1, Order = 2 }
        };

        public ProductTypeFieldSourceMapSeeder(IRepository<ProductTypeFieldSourceMap, EntityFrameworkProductContext> productRepository)
        {
            _productRepository = productRepository;
            _productRepository.Truncate();
        }

        public void Generate()
        {
            foreach (var productTypeFieldMap in _defaultValues)
            {
                _productRepository.Add(productTypeFieldMap);
            }
        }
    }
}