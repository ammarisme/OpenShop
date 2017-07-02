using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleTradingPortal.Areas.Default.Models;
using WholesaleTradingPortal.DAL;
using WholesaleTradingPortal.Models;

namespace WholesaleTradingPortal.Areas.Default.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Product> products = db.Products.Where(s => s.StocksQuantity > 0);
            return View(products);
        }

        [HttpPost]
        public ActionResult ByCategory(List<string> categories)
        {
            IEnumerable<Product> products = null;
            foreach (var category in categories)
            {
                if (products == null)
                {
                    // initate Enterprises
                    products = db.Products.Where(p => p.Category == category);
                }
                else
                {
                    /// just do the concat
                    products = products.Concat(db.Products.Where(p => p.Category == category));
                }
            }

            ProductsByCategoryViewModel model = new ProductsByCategoryViewModel();
            model.Products = products.Where(s => s.StocksQuantity > 0);
            model.Category = categories.ToString();
            return View(model);
        }

        [HttpGet]
        public ActionResult ByCategory()
        {
            ProductsByCategoryViewModel model = new ProductsByCategoryViewModel();
            model.Category = "All";

            IEnumerable<Product> products = db.Products.Where(s => s.StocksQuantity > 0);
            model.Products = products;

            return View(model);
        }
        public ActionResult Category(string id)
        {
            IEnumerable<Product> products = db.Products.Where(p => p.Category == id && p.StocksQuantity > 0);
            return View(products);
        }

        public ActionResult Product(int id)
        {
            if (db.Products.Where(p => p.ProductId == id).Count() > 0)
            {
            ProductViewModel model = new ProductViewModel();
            model.Product = db.Products.Find(id);
            model.Enterprise = db.Enterprises.Find(model.Product.EnterpriseId);

            return View(model);
            }
            return View("ProductNotFound");
        }

        [HttpGet]
        public ActionResult BySeller(int id)
        {
            IEnumerable<Product> products = db.Products.Where(p => p.EnterpriseId == id && p.StocksQuantity > 0);
            return View(products);
        }

        [HttpGet]
        public ActionResult Search(string id)
        {
            
            IEnumerable<Product> productsWithName = db.Products.Where(p => p.ProductName.Contains(id));
            IEnumerable<Product> productsWithShortDescription = db.Products.Where(p => p.ShortDescription.Contains(id));
            IEnumerable<Product> productsWithLongDescription  = db.Products.Where(p => p.LongDescription.Contains(id));
            IEnumerable<Product> productsWithCategoryName = db.Products.Where(p=> p.Category.Contains(id));

            IEnumerable<Product> products = new List<Product>();
            products = products.Concat(productsWithName);
            products = products.Concat(productsWithShortDescription);
            products = products.Concat(productsWithLongDescription);
            products = products.Concat(productsWithCategoryName);

            ProductsSerchViewModel model = new ProductsSerchViewModel();
            model.Products = products.Where( p => p.StocksQuantity  > 0);
            model.SearchTerm = id;

            return View(model);
            
        }

    }
}
