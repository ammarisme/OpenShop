using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailShop.Models;
using RetailShop.Areas.WholesalePortal.Models;
using RetailShop.DAL;
using Microsoft.AspNet.Identity;
namespace RetailShop.Areas.WholesalePortal.Controllers
{
    public class QuotationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WholesalePortal/Quotation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestQuote(string id)
        {
            RequestQuotationViewModel model = new RequestQuotationViewModel();
            model.Wholesaler = db.Wholesalers.Where(w => w.Id == id).Single<Wholesaler>();
            string retailerId = User.Identity.GetUserId();
            model.Retailer = db.Retailers.Where(r => r.Id == retailerId).Single<Retailer>();
            return View(model);
        }
    }
}