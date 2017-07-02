using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETrading.DAL;
using ETrading.Models;

namespace ETrading.Controllers
{
    public class CustomerOrdersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private ApplicationDbContext userDb = new ApplicationDbContext();
        
        /*Shows the html to create a customer order*/
        public ActionResult CreateCustomerOrder()
        {
            ViewBag.customers = userDb.Customers;//.Select(c => new { c.CustomerId , c.CustomerFullName});
            ViewBag.products = db.Products.Select(p => new { p.ProductId , p.ProductName});

            return View("_create_customer_order");
        }
        
        /*Viewing all the orders placed by customers*/
        public ActionResult ViewCustomerOrders()
        {
            return View("_view_customer_orders");
        }

        // GET: CustomerOrders
        public ActionResult Index()
        {
            return View(db.CustomerOrders.ToList());
        }

        // GET: CustomerOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            if (customerOrder == null)
            {
                return HttpNotFound();
            }
            return View(customerOrder);
        }

        // GET: CustomerOrders/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: CustomerOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerOrderId,CustomerId,OrderTime,DeliveryDate,Description")] CustomerOrder customerOrder)
        {
            if (ModelState.IsValid)
            {
                db.CustomerOrders.Add(customerOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerOrder);
        }

        // GET: CustomerOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            if (customerOrder == null)
            {
                return HttpNotFound();
            }
            return View(customerOrder);
        }

        // POST: CustomerOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerOrderId,CustomerId,OrderTime,DeliveryDate,Description")] CustomerOrder customerOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerOrder);
        }

        // GET: CustomerOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            if (customerOrder == null)
            {
                return HttpNotFound();
            }
            return View(customerOrder);
        }

        // POST: CustomerOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            db.CustomerOrders.Remove(customerOrder);
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
