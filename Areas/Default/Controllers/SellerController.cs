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
    public class SellersController : Controller
        
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Enterprise> Enterprises = db.Enterprises;
            return View(Enterprises);
        }

        [HttpPost]
        public ActionResult ByLocation(List<string> locations)
        {
            IEnumerable<Enterprise> Enterprises = null;
            foreach (var location in locations)
            {
                if (Enterprises == null)
                {
                    // initate Enterprises
                    Enterprises = db.Enterprises.Where(r => r.EnterpriseAddress == location);
                }
                else
                {
                    /// just do the concat
                    Enterprises = Enterprises.Concat(db.Enterprises.Where(r => r.EnterpriseAddress == location));
                }
            }

            return View(Enterprises);
        }

        [HttpGet]
        public ActionResult ByLocation()
        {
            IEnumerable<Enterprise> Enterprises = db.Enterprises;
            return View(Enterprises);
        }


        [HttpPost]
        public ActionResult ByCategory(List<string> categories)
        {
            IEnumerable<Enterprise> Enterprises = null;
            foreach (var category in categories)
            {
                if (Enterprises == null)
                {
                    // initate Enterprises
                    Enterprises = db.Enterprises.Where(r => r.Category == category);
                }
                else
                {
                    /// just do the concat
                    Enterprises = Enterprises.Concat(db.Enterprises.Where(r => r.Category == category));
                }
            }

            return View(Enterprises);
        }

        [HttpGet]
        public ActionResult ByCategory()
        {
            IEnumerable<Enterprise> Enterprises = db.Enterprises;
            return View(Enterprises);
        }

        public ActionResult Seller(int id)
        {
            if (db.Enterprises.Where(e => e.EnterpriseId == id).Count() > 0)
            {
            Enterprise Enterprise = db.Enterprises.Find(id);
            return View(Enterprise);
            }
            return View("SellerNotFound");
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
