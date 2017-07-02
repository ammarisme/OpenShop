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
    public class HomeController : Controller
        
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Default/Default
        public ActionResult Index()
        {
            EnterprisePageViewModel model = new EnterprisePageViewModel();
            model.Enterprises = db.Enterprises;
            return View(model);
        }

        [HttpPost]
        public ActionResult Sellers(List<string> locations)
        {
            IEnumerable<Enterprise> Enterprises = null;
            foreach(var location in locations){
                if (Enterprises == null)
                {
                    // initate Enterprises
                    Enterprises = db.Enterprises.Where(r => r.EnterpriseAddress == location);
                }
                else
                {
                    /// just do the concat
                Enterprises = Enterprises.Concat(db.Enterprises.Where(r => r.EnterpriseAddress==location));
                }
            }

            return View(Enterprises);
        }

        [HttpGet]
        public ActionResult Sellers()
        {
            IEnumerable<Enterprise> Enterprises = db.Enterprises;
            return View(Enterprises);
        }



        [HttpPost]
        public ActionResult Seller_Category(List<string> categories)
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
        public ActionResult Seller_Category()
        {
            IEnumerable<Enterprise> Enterprises = db.Enterprises;
            return View(Enterprises);
        }

        // GET: Default/Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Default/Edit/5
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
