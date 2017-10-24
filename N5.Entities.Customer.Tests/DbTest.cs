using N5.Entities.Customer;
using Xunit;

namespace N5.Entities.Customer.Tests
{
    public class DbTest
    {
        [Fact]
        public void ShouldConnectoToPostgressDB()
        {
            using (var db = new EntityFrameworkCustomerContext())
            {
                db.Database.EnsureCreated();
                var newCustomer = new Customer() { Id = 132 };
                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }
        }

    }
}
