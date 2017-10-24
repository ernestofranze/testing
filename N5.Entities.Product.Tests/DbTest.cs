using N5.Entities.Product;
using Xunit;

namespace N5.Entities.Product.Tests
{
    public class DbTest
    {
        [Fact]
        public void ShouldConnectoToPostgressDB()
        {
            using (var db = new EntityFrameworkProductContext())
            {
                db.Database.EnsureCreated();
                var newProductType = new ProductType() { Id = 113 };
                db.ProductTypes.Add(newProductType);
                db.SaveChanges();
            }
        }
    }
}