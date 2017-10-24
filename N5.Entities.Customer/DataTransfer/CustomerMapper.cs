using N5.Core.Repository;
using N5.Entities.Customer.DataTransfer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace N5.Entities.Customer.DataTransfer
{
    public class CustomerMapper
    {
        private IRepository<CustomerFieldSourceMap, EntityFrameworkCustomerContext> _fieldMapsRepository;
        private IEnumerable<CustomerFieldSourceMap> _fieldMaps;

        // TO DO: Refactor que se inyecte solo los fieldmaps y no el repository
        public CustomerMapper(IRepository<CustomerFieldSourceMap, EntityFrameworkCustomerContext> fieldMapsRepository)
        {
            _fieldMapsRepository = fieldMapsRepository;
            _fieldMaps = _fieldMapsRepository.ListIncludeMembers(x => x.SourceField, x => x.SourceField.Source, 
                                                                 x => x.CustomerField, x=> x.CustomerField.Customer);
        }

        public MappedCustomerMessage Handle(CustomerMessage message)
        {           
            var mapped = new MappedCustomerMessage() { CustomerId = message.CustomerId,
                SourceId = message.SourceId,
                Data = new Dictionary<string, Dictionary<string, object>>()};

            var sourceFieldsMaps = _fieldMaps.Where(s => s.SourceField.Source.Id == message.SourceId)
                                             .ToList();

            foreach (var data in message.Data)
            {
                var values = new Dictionary<string, object>();

                foreach (var item in sourceFieldsMaps.Where(fm => fm.SourceField.FieldName == data.Key && fm.CustomerField != null))
                {
                    values.Add(message.SourceId.ToString(), Convert.ChangeType(data.Value, item.CustomerField.FieldType));
                    mapped.Data.Add(item.CustomerField.FieldName, values);
                }
            }

            return mapped;
        }
    }
}
