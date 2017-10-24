using N5.Core.Repository;
using System.Collections.Generic;

namespace N5.Entities.Product.Seeds
{
    public class ProductTypeFieldSeeder : IDbSeed
    {
        private IRepository<ProductTypeField, EntityFrameworkProductContext> _productRepository;
        private IList<ProductTypeField> _defaultValues = new List<ProductTypeField>()
        {            
            new ProductTypeField() { Id = 1, FieldName = "nombre", FieldTypeDescription = "System.String", ProductTypeId = 1 },            
            new ProductTypeField() { Id = 2, FieldName = "avatar", FieldTypeDescription = "System.Byte[]", ProductTypeId = 1 },
            new ProductTypeField() { Id = 3, FieldName = "nombre", FieldTypeDescription = "System.String", ProductTypeId = 2 },
            new ProductTypeField() { Id = 4, FieldName = "avatar", FieldTypeDescription = "System.Byte[]", ProductTypeId = 2 }
        };

        public ProductTypeFieldSeeder(IRepository<ProductTypeField, EntityFrameworkProductContext> productRepository)
        {
            _productRepository = productRepository;
            _productRepository.Truncate();
        }        

        public void Generate()
        {
            foreach (var productTypeField in _defaultValues)
            {
                _productRepository.Add(productTypeField);
            }
        }
    }
}