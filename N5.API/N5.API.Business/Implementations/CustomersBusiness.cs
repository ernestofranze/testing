using N5.API.Business.DTO;
using N5.API.Business.Interfaces;
using N5.DataAccess.DTO;
using N5.DataAccess.Interfaces;
using N5.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5.API.Business.Implementations
{
    public class CustomersBusiness : ICustomersBusiness
    {
        private ICustomersDataAccess _customersDataAccess;

        public CustomersBusiness(ICustomersDataAccess customersDataAccess)
        {
            _customersDataAccess = customersDataAccess;
        }

        public dynamic GetCustomer(string id)
        {
            return _customersDataAccess.Get<CustomerDocument>(id);
        }

        public dynamic Search(IEnumerable<FilterViewModel> param)
        {
            var dto = new Query()
            {
                Bool = new Bool()
                {
                    Must = new List<IMust>() {
                            new Term() { TermField = new Dictionary<string, object>() { { "data.nombre", "Bradley Harrington" } } },
                            new Range() { RangeFields = new Dictionary<string, Dictionary<string, object>> () { { "data.edad", new Dictionary<string, object>() { { "gte", 10 }, { "lte", 30 } } } } }
                        }
                }
            };

            return _customersDataAccess.Search<CustomerDocument>(dto);
        }
    }
}
