using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RetailShop.Models;
using RetailShop.DAL;
using RetailShop.Areas.Products.Models;
namespace RetailShop.Areas.Products.Controllers
{

    public class ProductController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext(); 
        
        public ActionResult ViewEditProducts()
        {
            return View();
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        public ActionResult ShowWholesaleProduct(int id)
        {
            WholesaleProductViewModel retailProduct = new WholesaleProductViewModel();
            retailProduct.Product = db.Products.Find(id);
            retailProduct.Wholesaler = db.Wholesalers.Find(retailProduct.Product.ApplicationUserId);
            return View(retailProduct);
        }

        public ActionResult ShowRetailProduct(int id)
        {
            RetailProductViewModel retailProduct = new RetailProductViewModel();
            retailProduct.Product = db.Products.Find(id);
            retailProduct.Retailer = db.Retailers.Find(retailProduct.Product.ApplicationUserId);
            return View(retailProduct);
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
