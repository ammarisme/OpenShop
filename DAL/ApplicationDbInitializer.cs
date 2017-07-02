using IntegrationSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace IntegrationSystem.DAL
{
    public class ApplicationDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {

        protected override  void Seed(ApplicationDbContext db)
        {
            Product product1 = new Product { ProductId = 1, Unit = "unit", StocksQuantity = 100, ShortDescription = "A short description", ProductName = "Table", LongDescription = "A longer description", RetailPrice = 2000 };
            Product product2 = new Product { ProductId = 2, Unit = "unit", StocksQuantity = 100, ShortDescription = "A short description", ProductName = "Laptop", LongDescription = "A longer description", RetailPrice = 2000 };
            Product product3 = new Product { ProductId = 3, Unit = "unit", StocksQuantity = 100, ShortDescription = "A short description", ProductName = "Anything", LongDescription = "A longer description", RetailPrice = 2000 };


            db.Products.Add(product1);
            db.Products.Add(product2);
            db.Products.Add(product3);

            db.SaveChanges();
        }
    }
}