using Autofac;
using Autofac.Extensions.DependencyInjection;
using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using N5.API.Business.Implementations;
using N5.DataAccess.Implementations;
using N5.Core.Configuration;
using N5.Core.Repository;
using System;
using Nest;

namespace N5.API.Configuration
{
    public static class Initializer
    {
        public static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection.
            services.AddMvc();

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            //
            // Note that Populate is basically a foreach to add things
            // into Autofac that are in the collection. If you register
            // things in Autofac BEFORE Populate then the stuff in the
            // ServiceCollection can override those things; if you register
            // AFTER Populate those registrations can override things
            // in the ServiceCollection. Mix and match as needed.
            builder.Populate(services);

            var node = new Uri(AppConfiguration.GetConfiguration(ConfigurationKeys.ElasticSearchEndpointUri));
            var config = new ConnectionSettings(node);
            builder.RegisterType<ElasticClient>().WithParameter("settings", config).As<IElasticClient>();

            builder.RegisterGeneric(typeof(PostgresqlRepository<,>)).As(typeof(IRepository<,>));
            builder.RegisterAssemblyTypes(typeof(AdministrationDataAccess).Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(AdministrationBusiness).Assembly).AsImplementedInterfaces();

            //builder.RegisterType<FakeAdministrationBusiness>().As<IAdministrationBusiness>();
            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(builder.Build());
        }
    }
}
