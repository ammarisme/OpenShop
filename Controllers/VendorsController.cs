using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETrading.Controllers
{
    public class VendorsController : Controller
    {
        // GET: Vendors/ViewVendors
        public ActionResult ViewEditVendors()
        {
            return View("_view_edit_vendors");
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View("_create");
        }
        
        // POST: Vendor/Create
        //// GET: Vendor
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Vendor/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Vendor/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Vendor/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Vendor/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Vendor/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
