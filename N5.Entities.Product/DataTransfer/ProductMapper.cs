using N5.Core.Repository;
using N5.Entities.Product.DataTransfer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace N5.Entities.Product.DataTransfer
{
    public class ProductMapper
    {
        private IRepository<ProductTypeFieldSourceMap, EntityFrameworkProductContext> _fieldMapsRepository;
        private IEnumerable<ProductTypeFieldSourceMap> _fieldMaps;

        public ProductMapper(IRepository<ProductTypeFieldSourceMap, EntityFrameworkProductContext> fieldMapsRepository)
        {
            _fieldMapsRepository = fieldMapsRepository;

            _fieldMaps = _fieldMapsRepository.ListIncludeMembers(x => x.SourceField, x => x.SourceField.Source,
                                                                 x => x.ProductTypeField, x => x.ProductTypeField.ProductType);
        }

        public MappedProductMessage Handle(ProductMessage message)
        {
            var mapped = new MappedProductMessage() { ProductId = message.ProductId,
                                                      CustomerId = message.CustomerId,
                                                      Data = new Dictionary<string, Dictionary<string, object>>() };

            var sourceFieldsMaps = _fieldMaps.Where(s => s.SourceField.Source.Id == message.SourceId)
                                                .ToList();

            foreach (var data in message.Data)
            {
                var values = new Dictionary<string, object>();

                foreach (var item in sourceFieldsMaps.Where(fm => fm.SourceField.FieldName == data.Key))
                {
                    if (item.ProductTypeField != null)
                    {
                        values.Add(message.SourceId.ToString(), Convert.ChangeType(data.Value, item.ProductTypeField.FieldType));
                        mapped.Data.Add(item.ProductTypeField.FieldName, values);
                    }
                }
            }

            return mapped;
        }
    }
}
