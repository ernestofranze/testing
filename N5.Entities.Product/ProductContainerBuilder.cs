using Autofac;
using N5.Core.Repository;
using N5.Entities.Product.DataTransfer;
using N5.Entities.Product.Seeds;

namespace N5.Entities.Product
{
    public static class ProductContainerBuilder
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterGeneric(typeof(PostgresqlRepository<,>)).As(typeof(IRepository<,>));

            builder.RegisterType<ProductMapper>();

            builder.RegisterType<ProductTypeSeeder>().As<IDbSeed>();
            builder.RegisterType<ProductTypeFieldSeeder>().As<IDbSeed>();
            builder.RegisterType<ProductTypeFieldSourceMapSeeder>().As<IDbSeed>();

            return builder.Build();
        }
    }
}