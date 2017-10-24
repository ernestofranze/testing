using Microsoft.EntityFrameworkCore;
using N5.Core.Configuration;
using N5.Entities.Customer;

namespace N5.Administration.Filters
{
    public class EntityFrameworkFilterContext : DbContext
    {
        public DbSet<CustomerField> CustomerFields { get; set; }
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
                .HasKey(c => new { c.CustomerFieldID, c.SourceFieldID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
