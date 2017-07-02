using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailShop.Areas.WholesalePortal.Models;
using RetailShop.DAL;
using RetailShop.Models;

namespace RetailShop.Areas.WholesalePortal.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: WholesalePortal/Wholesaler
        public ActionResult Shop(string id)
        {
            ShopViewModel shopViewModel = new ShopViewModel();
            shopViewModel.Wholesaler = db.Wholesalers.Where(w => w.Id == id).Single<Wholesaler>();
            shopViewModel.ProductId = db.Products.Where(p => p.ApplicationUserId == id);
            
            return View(shopViewModel);
        }

        public ActionResult Products()
        {
            return View();
        }



    }
}