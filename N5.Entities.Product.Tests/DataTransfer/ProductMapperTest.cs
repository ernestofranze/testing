using Moq;
using N5.Core.Repository;
using N5.Entities.Product.DataTransfer;
using N5.Entities.Product.DataTransfer.DTO;
using System;
using System.Collections.Generic;
using Xunit;

namespace N5.Entities.Product.Tests.DataTransfer
{
    public class ProductMapperTest
    {
        [Fact]
        public void Handle_ValidParam_ReturnMappedCustomerMessageObject()
        {
            var mockRepository = new Mock<IRepository<ProductTypeFieldSourceMap, EntityFrameworkProductContext>>();
            var productMapper = new ProductMapper(mockRepository.Object);

            var productMessage = new ProductMessage()
            {
                CustomerId = 1,
                ProductId = 1,
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

            var result = productMapper.Handle(productMessage);

            Assert.IsType<MappedProductMessage>(result);
        }

        [Fact]
        public void Handle_InvalidParam_Throws()
        {
            var mockRepository = new Mock<IRepository<ProductTypeFieldSourceMap, EntityFrameworkProductContext>>();
            var productMapper = new ProductMapper(mockRepository.Object);
            var productMessage = new ProductMessage();

            Assert.Throws<NullReferenceException>(() => productMapper.Handle(productMessage));
        }
    }
}
