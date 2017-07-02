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
    public class PurchaseOrdersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: PurchaseOrders
        public ActionResult Index()
        {
            return View(db.PurchaseOrder.ToList());
        }

        // GET: PurchaseOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
        public ActionResult CreatePurchaseOrder()
        {
            ViewBag.vendors = db.Vendor;
            ViewBag.materials = db.Materials;
            return View("_create_purchase_order");
        }

        // GET: PurchaseOrders/View
        public ActionResult View()
        {
            return View("_View");
        }

        // GET: PurchaseOrders/View
        public ActionResult ViewPurchaseOrders()
        {
            return View("_view_purchase_orders");
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseOrderId,VendorId,SentTime,Status")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrder.Add(purchaseOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseOrderId,VendorId,SentTime,Status")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
            db.PurchaseOrder.Remove(purchaseOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
