using N5.Core.Repository;
using N5.DataAccess.Interfaces;
using N5.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using N5.Entities.Customer.DataTransfer.DTO;

namespace MergeConsumer.Services
{
    public class MergeService : IMergeService
    {
        private ICustomersDataAccess _customersDataAccess;
        private IEnumerable<CustomerFieldSourceMap> _fieldMaps;

        public MergeService(ICustomersDataAccess customersDataAccess, IRepository<CustomerFieldSourceMap, EntityFrameworkCustomerContext> fieldMapsRepository)
        {
            _customersDataAccess = customersDataAccess;
            _fieldMaps = fieldMapsRepository.ListIncludeMembers(x => x.SourceField, x => x.SourceField.Source,
                                                                x => x.CustomerField, x => x.CustomerField.Customer);
        }

        public void Save(string customerId, MappedCustomerMessage message)
        {
            var currentDocument = _customersDataAccess.Get<CustomerDocument>(customerId);
            var upsertCustomer = new CustomerDocument(customerId);
            var dataToMerge = JObject.FromObject(message.Data);

            if (currentDocument == null)
            {
                upsertCustomer.Data = message.Data.ToDictionary(x => x.Key, x => x.Value.First().Value);
                upsertCustomer.Full = dataToMerge;
            }
            else
            {
                var currentCustomerFull = new JObject(currentDocument.Full);

                currentCustomerFull.Merge(dataToMerge, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                });

                upsertCustomer.Full = currentCustomerFull;
                upsertCustomer.Data = currentDocument.Data;

                foreach (var customerFieldName in message.Data.Select(x => x.Key))
                {
                    var filters = _fieldMaps.Where(x => x.CustomerField.FieldName.Equals(customerFieldName, StringComparison.InvariantCultureIgnoreCase));
                    var token = currentCustomerFull.GetValue(customerFieldName, StringComparison.InvariantCultureIgnoreCase);

                    foreach (var fieldMap in filters.Where(x => x.SourceField != null).OrderBy(x => x.Order))
                    {
                        var customerValue = token[fieldMap.SourceField.SourceId.ToString()];
                        if (customerValue != null)
                        {
                            upsertCustomer.Data[customerFieldName] = ((JValue)customerValue).Value;
                            break;
                        }
                    }
                }
            }

            _customersDataAccess.Put(upsertCustomer);
        }
    }
}
