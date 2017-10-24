using Microsoft.EntityFrameworkCore;
using N5.Core.Configuration;
using N5.Entities.Source;

namespace N5.Entities.Product
{
    public class EntityFrameworkProductContext : DbContext
    {
        public DbSet<Source.Source> Sources { get; set; }
        public DbSet<SourceField> SourceFields { get; set; }
        public DbSet<ProductTypeFieldSourceMap> ProductTypeFieldSourceMap { get; set; }
        public DbSet<ProductTypeField> ProductFields { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfiguration.GetConnectionString(ConnectionStrings.DefaultConnection));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTypeFieldSourceMap>()
                .HasKey(c => new { c.ProductTypeFieldID, c.SourceFieldID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
