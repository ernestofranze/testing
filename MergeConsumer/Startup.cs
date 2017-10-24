using Autofac;
using Elasticsearch.Net;
using MergeConsumer.Services;
using N5.Core.Configuration;
using N5.Core.Repository;
using N5.DataAccess.Implementations;
using N5.Entities.Customer.DataTransfer;
using N5.Entities.Customer.Seeds;
using Nest;
using System;

namespace MergeConsumer
{
    public static class Startup
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(PostgresqlRepository<,>)).As(typeof(IRepository<,>));

            var node = new Uri(AppConfiguration.GetConfiguration(ConfigurationKeys.ElasticSearchEndpointUri));
            var config = new ConnectionSettings(node);
            builder.RegisterType<ElasticClient>().WithParameter("connectionSettings", config).As<IElasticClient>();

            builder.RegisterAssemblyTypes(typeof(CustomersDataAccess).Assembly).AsImplementedInterfaces();

            builder.RegisterType<MergeService>().As<IMergeService>();

            return builder.Build();
        }
    }
}
