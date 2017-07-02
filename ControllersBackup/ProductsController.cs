using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETrading.Models;
using ETrading.DAL;

namespace ETrading.Controllers
{

    public class ProductsController : Controller
    {
        
        private DatabaseContext db = new DatabaseContext(); // the Dal handler
        
        // GET: Products/Create
        public ActionResult ViewEditProducts()
        {
            return View("_view_edit_products");
        }

        public ActionResult CreateProduct()
        {
            ViewBag.materials = db.Materials;
            return View("_create_product");
        }

        //// GET: Products
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Products/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}


        //// POST: Products/Create
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

        //// GET: Products/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Products/Edit/5
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

        //// GET: Products/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Products/Delete/5
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
