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
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: WholesalePortal/Wholesaler
        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Wholesalers = db.Wholesalers;
            return View(homeViewModel);
        }

        public ActionResult Products()
        {
            return View();
        }



    }
}