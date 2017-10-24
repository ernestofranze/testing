using Autofac;
using Elasticsearch.Net;
using N5.Core.Configuration;
using N5.Core.Repository;
using N5.Entities.Customer.DataTransfer;
using N5.Entities.Customer.Seeds;
using Nest;
using System;

namespace N5.Entities.Customer
{
    public static class CustomerContainerBuilder
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(PostgresqlRepository<,>)).As(typeof(IRepository<,>));

            var node = new Uri(AppConfiguration.GetConfiguration(ConfigurationKeys.ElasticSearchEndpointUri));
            var config = new ConnectionSettings(node);
            builder.RegisterType<ElasticClient>().WithParameter("connectionSettings", config).As<IElasticClient>();

            builder.RegisterType<CustomerMapper>();

            builder.RegisterType<CustomerSeeder>().As<IDbSeed>();
            builder.RegisterType<SourceSeeder>().As<IDbSeed>().PreserveExistingDefaults();
            builder.RegisterType<CustomerFieldSeeder>().As<IDbSeed>().PreserveExistingDefaults();
            builder.RegisterType<SourceFieldSeeder>().As<IDbSeed>().PreserveExistingDefaults();
            builder.RegisterType<CustomerFieldSourceMapSeeder>().As<IDbSeed>().PreserveExistingDefaults();

            return builder.Build();
        }
    }
}
