using Microsoft.EntityFrameworkCore;
using N5.Core.Configuration;
using N5.Entities.Source;


namespace N5.Entities.Customer
{
    public class EntityFrameworkCustomerContext : DbContext
    {
        public EntityFrameworkCustomerContext()
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Source.Source> Sources { get; set; }
        public DbSet<CustomerField> CustomerFields { get; set; }
        public DbSet<SourceField> SourceFields { get; set; }
        public DbSet<CustomerFieldSourceMap> CustomerFieldSourceMaps { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfiguration.GetConnectionString(ConnectionStrings.DefaultConnection));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerFieldSourceMap>()
                .HasKey(c => new { c.CustomerFieldID, c.SourceFieldID});

            base.OnModelCreating(modelBuilder);
        }
    }
}
