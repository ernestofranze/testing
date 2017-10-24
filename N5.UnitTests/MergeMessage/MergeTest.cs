using N5.DataAccess.Interfaces;
using System;
using Xunit;
using Moq;
using N5.Core.Repository;
using N5.Entities.Customer;
using Newtonsoft.Json.Linq;
using MergeConsumer.Services;
using System.Linq.Expressions;
using Moq.Language.Flow;
using System.Collections.Generic;
using N5.Entities.Source;
using N5.Entities.Customer.DataTransfer.DTO;
using System.Linq;
namespace N5.UnitTests.MergeMessage
{
    public class MergeTest
    {
        private Mock<ICustomersDataAccess> _dataAccess;
        private Mock<IRepository<CustomerFieldSourceMap, EntityFrameworkCustomerContext>> _repository;
        private string elasticCustomer = @"
            {
                'nombre':
                {
                    '1': 'Cliente prueba',
                    '2': 'Cliente Segundo Prueba'
                },
                'edad': 
                { 
                    '2': 35 
                }
            }";

        public MergeTest()
        {
            _dataAccess = new Mock<ICustomersDataAccess>();
            _repository = new Mock<IRepository<CustomerFieldSourceMap, EntityFrameworkCustomerContext>>();
            
            _repository.SetupIgnoreArgs(x => x.ListIncludeMembers(null)).Returns(new List<CustomerFieldSourceMap>() {
                new CustomerFieldSourceMap { CustomerField = new CustomerField {FieldName = "nombre" }, SourceField = new SourceField { SourceId = 1 } },
                new CustomerFieldSourceMap { CustomerField = new CustomerField {FieldName = "edad" },   SourceField = new SourceField { SourceId = 2 }, Order = 1 },
                new CustomerFieldSourceMap { CustomerField = new CustomerField {FieldName = "edad" },   SourceField = new SourceField { SourceId = 1 }, Order = 2 }    
            });
        }

        [Fact]
        public void ShouldSetBestNewAge()
        {
            var service = new MergeService(_dataAccess.Object, _repository.Object);
            var newCustomer = new MappedCustomerMessage
            {
                CustomerId = 1,
                SourceId = 2,
                Data = new Dictionary<string, Dictionary<string, object>> { { "edad", new Dictionary<string, object> { { "2", 36 } } } }
            };
            var customerDocument = new CustomerDocument("1")
            {
                Data = new Dictionary<string, object>{
                    { "nombre", "cliente prueba" },
                    { "edad", 35 }
                },
                Full = JObject.Parse(elasticCustomer)
            
            };

            _dataAccess.Setup(x => x.Get<CustomerDocument>(It.IsAny<string>())).Returns(customerDocument);
            _dataAccess.Setup(x => x.Put(It.IsAny<CustomerDocument>()));
            service.Save("1", newCustomer);
            _dataAccess.Verify(x => x.Put(It.Is<CustomerDocument>(y => y.Data.Any(z => z.Key == "edad" && ((int)z.Value) == 36))));
            
        }

        [Fact]
        public void ShouldCreateNewCustomer()
        {
            long sourceId = 2;
            var service = new MergeService(_dataAccess.Object, _repository.Object);
            var newCustomer = new MappedCustomerMessage
            {
                CustomerId = 1,
                SourceId = sourceId,
                Data = new Dictionary<string, Dictionary<string, object>> {
                    { "edad", new Dictionary<string, object> { { sourceId.ToString(), 36 } } },
                    { "nombre", new Dictionary<string, object> { { sourceId.ToString(), "Nuevo cliente" } } }
                }
            };
            _dataAccess.Setup(x => x.Get<CustomerDocument>(It.IsAny<string>()));
            _dataAccess.Setup(x => x.Put(It.IsAny<CustomerDocument>()));
            service.Save("1", newCustomer);
            _dataAccess.Verify(x => x.Put(It.Is<CustomerDocument>(y => y.Data.Any(z => z.Key == "nombre" && (z.Value.ToString()) == "Nuevo cliente"))));
            _dataAccess.Verify(x => x.Put(It.Is<CustomerDocument>(y => y.Data.Any(z => z.Key == "edad" && ((int)z.Value) == 36))));
        }

        [Fact]
        public void ShouldNotChangeCustomerAge()
        {
            long sourceId = 1;
            var service = new MergeService(_dataAccess.Object, _repository.Object);
            var newCustomer = new MappedCustomerMessage
            {
                CustomerId = 1,
                SourceId = sourceId,
                Data = new Dictionary<string, Dictionary<string, object>> { { "edad", new Dictionary<string, object> { { sourceId.ToString(), 50 } } } }
            };
            var customerDocument = new CustomerDocument("1")
            {
                Data = new Dictionary<string, object>{
                    { "nombre", "cliente prueba" },
                    { "edad", 35 }
                },
                Full = JObject.Parse(elasticCustomer)
            };
            
            _dataAccess.Setup(x => x.Get<CustomerDocument>(It.IsAny<string>())).Returns(customerDocument);
            _dataAccess.Setup(x => x.Put(It.IsAny<CustomerDocument>()));
            service.Save("1", newCustomer);
            _dataAccess.Verify(x => x.Put(It.Is<CustomerDocument>(y => y.Data.Any(z => z.Key == "edad" && ((long)z.Value) == 35))));

        }
    }
}
