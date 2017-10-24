using Autofac;
using Moq;
using N5.Core;
using N5.Core.Repository;
using N5.Entities.Customer.DataTransfer;
using N5.Entities.Customer.DataTransfer.DTO;
using N5.Entities.Customer.Seeds;
using System;
using System.Collections.Generic;
using Xunit;

namespace N5.Entities.Customer.Tests.DataTransfer
{
    public class CustomerMapperTest
    {
        [Fact]
        public void Handle_ValidParam_ReturnMappedCustomerMessageObject()
        {
            var mockRepository = new Mock<IRepository<CustomerFieldSourceMap, EntityFrameworkCustomerContext>>();
            var customerMapper = new CustomerMapper(mockRepository.Object);

            var customerMessage = new CustomerMessage()
            {
                CustomerId = 1,
                Data = new Dictionary<string, string>()
                {
                    { "name", "messi" },
                    { "phone", "123123156" }
                },
                ArrayData = new Dictionary<string, string[]>()
                {
                    { "cuentas", new string[] {"905270296", "900904026", "914637420" } }
                }
            };

            var result = customerMapper.Handle(customerMessage);

            Assert.IsType<MappedCustomerMessage>(result);
        }

        [Fact]
        public void Handle_InvalidParam_Throws()
        {
            var mockRepository = new Mock<IRepository<CustomerFieldSourceMap, EntityFrameworkCustomerContext>>();
            var customerMapper = new CustomerMapper(mockRepository.Object);
            var customerMessage = new CustomerMessage();
            
            Assert.Throws<NullReferenceException>(() => customerMapper.Handle(customerMessage));            
        }
    }
}