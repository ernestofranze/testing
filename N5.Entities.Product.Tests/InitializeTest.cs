using Autofac;
using N5.Core;
using N5.Entities.Product.Seeds;
using System.Collections.Generic;
using Xunit;

namespace N5.Entities.Product.Tests
{
    public class InitializeTest
    {
        [Fact]
        public void ShouldInitialize()
        {
            IContainer container = ProductContainerBuilder.Build();
            IInitialize initialize = new ProductInitialize(container.Resolve<IEnumerable<IDbSeed>>());
            initialize.Initialize();
            initialize.Seed();
        }
    }
}
