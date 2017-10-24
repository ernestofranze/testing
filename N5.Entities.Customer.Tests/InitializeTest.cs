using Autofac;
using N5.Core;
using N5.Entities.Customer.Seeds;
using System.Collections.Generic;
using Xunit;

namespace N5.Entities.Customer.Tests
{
    public class InitializeTest
    {
        [Fact]
        public void ShouldInitialize()
        {
            IContainer container = CustomerContainerBuilder.Build();
            IInitialize initialize = new CustomerInitialize(container.Resolve<IEnumerable<IDbSeed>>());
            initialize.Initialize();
            initialize.Seed();
        }
    }
}