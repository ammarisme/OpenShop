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
using RetailTradingPortal.Areas.Quotations.Models;

namespace RetailTradingPortal.Areas.Quotations.Controllers
{

    public class MyQuotationsController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult SentQuotations()
        {
            SentQuotationsViewModel sentQuotations = new SentQuotationsViewModel();
            sentQuotations.SentQuotations = db.Quotations.Where(q => q.Status == "Sent");

            return View(sentQuotations);
        }

        public ActionResult RequestsRecieved()
        {
            //string EnterpriseId = User.Identity.GetUserId();

            ////RequestedQuotationsViewModel requestedQuotations = new RequestedQuotationsViewModel();
            ////requestedQuotations.RequestedQuotations = db.Quotations.Where(q=>  q.Status=="Request");
            ////requestedQuotations.Enterprise = db.Accounts.Where(r => r.Id == EnterpriseId).Single<Account>();

            //return View(requestedQuotations);
            return View();
        }

        //public ActionResult SentQuotations()
        //{
        //    string wholesalerId = User.Identity.GetUserId();

        //    SentQuotationsViewModel sentQuotations = new SentQuotationsViewModel();
        //    sentQuotations.SentQuotations =  db.Quotations.Where(q => q.WholesalerId==wholesalerId && q.Status=="Sent");
        //    sentQuotations.Wholesaler = db.Wholesalers.Where(r => r.Id == wholesalerId).Single<Wholesaler>();

        //    return View(sentQuotations);
        //}
        //public ActionResult SendQuotation(int id)
        //{
        //    SendQuotationsViewModel quotation = new SendQuotationsViewModel();
        //    quotation.Quotation = db.Quotations.Where(q=> q.QuotationId == id).Single<Quotation>();
        //    return View(quotation);
        //}

        //public ActionResult Request(string id)
        //{
        //    RequestQuotationViewModel model = new RequestQuotationViewModel();
        //    model.Wholesaler = db.Wholesalers.Where(w => w.Id == id).Single<Wholesaler>();
        //    string EnterpriseId = User.Identity.GetUserId();
        //    model.Enterprise = db.Enterprises.Where(r => r.Id == EnterpriseId).Single<Enterprise>();
        //    return View(model);
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
