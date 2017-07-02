using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailShop.Areas.WholesalePortal.Controllers
{
    public class OrderController : Controller
    {
        // GET: WholesalePortal/Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Placement()
        {
            return View();
        }
    }
}