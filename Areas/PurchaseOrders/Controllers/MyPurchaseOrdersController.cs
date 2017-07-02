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
using Microsoft.AspNet.Identity;
using RetailTradingPortal.Areas.PurchaseOrders.Models;

namespace RetailTradingPortal.Areas.PurchaseOrders.Controllers
{

    public class MyPurchaseOrdersController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        /*
         * @purpose - Show a view where the user can create a purchase order.
         */
        [Authorize]
        public ActionResult AddPurchaseOrder()
        {
            string userId = User.Identity.GetUserId();
            CreatePurchaseOrderView createWholesaleOrder = new CreatePurchaseOrderView();
            createWholesaleOrder.ProductId = db.Products;
            createWholesaleOrder.SupplierId = db.Enterprises;
            return View(createWholesaleOrder);
        }

        [Authorize]
        public ActionResult ViewPurchaseOrders()
        {
            return View();
        }

        [Authorize]
        public ActionResult ConvertToStocks() {
            return View();
        }
        //public ActionResult Edit()
        //{
        //    string userId = User.Identity.GetUserId();
        //    var orders = db.WholeSales.Where(r => r.WholesalerId== userId);
        //    var orderList = orders.ToList();
        //    ViewBag.orders = orderList;
        //    ViewBag.userId = userId;
        //    return View();
        //}

        //public ActionResult PlaceOrder(string id)
        //{
        //    CreateWholesaleOrderView placeOrderModel = new CreateWholesaleOrderView();
        //    string userId = User.Identity.GetUserId();
        //    placeOrderModel.Enterprise = db.Enterprises.Find(userId);
        //    placeOrderModel.Wholesaler = db.Wholesalers.Find(id);

        //    return View(placeOrderModel);
        //}

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
