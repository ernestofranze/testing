using Microsoft.EntityFrameworkCore;
using N5.Administration.Filters;
using N5.Core.Configuration;
using N5.Entities.Customer;
using N5.Entities.Product;
using N5.Entities.Source;

namespace N5.Entities.Migrations
{
    public class EntityFrameworkSharedContext : DbContext
    {
        public DbSet<Source.Source> Sources { get; set; }
        public DbSet<SourceField> SourceFields { get; set; }

        public DbSet<ProductTypeFieldSourceMap> ProductTypeFieldSourceMaps { get; set; }
        public DbSet<CustomerFieldSourceMap> CustomerFieldSourceMaps { get; set; }

        public DbSet<ProductTypeField> ProductTypeFields { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<CustomerField> CustomerFields { get; set; }
        public DbSet<Customer.Customer> Customers { get; set; }        

        public DbSet<BooleanFilter> BooleanFilters { get; set; }
        public DbSet<BulkOptionsFilter> BulkOptionsFilters { get; set; }
        public DbSet<DefinedRangeFilter> DefinedRangeFilters { get; set; }
        public DbSet<FieldValueFilter> FieldValueFilters { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<OpenRangeFilter> OpenRangeFilters { get; set; }
        public DbSet<RangeDefinition> RangeDefinitions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfiguration.GetConnectionString(ConnectionStrings.DefaultConnection));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerFieldSourceMap>()
                .HasKey(c => new { c.CustomerFieldID, c.SourceFieldID});
            modelBuilder.Entity<ProductTypeFieldSourceMap>()
                .HasKey(c => new { c.ProductTypeFieldID, c.SourceFieldID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
