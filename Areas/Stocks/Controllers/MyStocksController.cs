using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RetailTradingPortal.Models;
using RetailTradingPortal.DAL;
using RetailTradingPortal.Areas.Stocks.Models;
using Microsoft.AspNet.Identity;
namespace RetailTradingPortal.Areas.Stocks.Controllers
{

    public class MyStocksController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext(); 
        

        /*
         * @purpose - Viewing the page to add stocks recieved
         */
        [Authorize]
        public ActionResult AddStocks()
        {
            CreateProductStocksViewModel createStocksViewModel = new CreateProductStocksViewModel();
            createStocksViewModel.ProductId = db.Products;
            return View(createStocksViewModel);
        }

        /*
         * @purpose - Viewing current stock of all products
         */
        [Authorize]
        public ActionResult ViewCurrentStock()
        {
            return View();
        }

        /*
         * @purpose - Deduct wasted stock quantities from the product table
         */
        [Authorize]
        public ActionResult DeductStockWaste()
        {
            StockDeductionViewModel model = new StockDeductionViewModel();
            model.ProductId = db.Products;
            return View(model);
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
