using Elasticsearch.Net;
using N5.Administration.Filters;
using N5.DataAccess.DTO;
using N5.DataAccess.Interfaces;
using N5.Core.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Nest;
using N5.Entities.Customer;

namespace N5.DataAccess.Implementations
{
    public class CustomersDataAccess : ICustomersDataAccess
    {
        private IElasticClient _elasticClient;

        public CustomersDataAccess(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public T Get<T>(string id) where T : class
        {
            var doc = _elasticClient.Get<T>(id, idx => idx.Index("customers").Type("customer"));
            return doc.Source;
        }

        public IEnumerable<T> Search<T>(Query filter) where T : class
        {
            var body = JsonConvert.SerializeObject(filter);

            var doc = _elasticClient.Search<T>(s => s.Index("customers").Type("customer").Query(q => q.Raw(body)));

            return doc.Documents;
        }

        public void Put<T>(T document) where T : class
        {
            _elasticClient.Index<T>(document, p => p.Index("customers").Type("customer"));
        }
    }
}