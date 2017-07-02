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
    public class JobCardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobCards
        public ActionResult Index()
        {
            return View(db.JobCards.ToList());
        }

        /*
         * Creating a Job Card
         * A job card includes -
         * a list of materials used
         * a list of products that to be produced
         */
        public ActionResult CreateJobCard()
        {
            ViewBag.materials = db.Materials;
            ViewBag.products = db.Products;
            ViewBag.customerOrders = db.CustomerOrders;
            ViewBag.employees = db.Employees;
            return View("_create_job_cards");
        }

        public ActionResult ChangeJobCards()
        {
            ViewBag.materials = db.Materials;
            ViewBag.statuses = (new JobCard()).JobCardStatuses;
            return View("_edit_job_card");
        }

        public ActionResult ViewJobCards()
        {
            return View("_view_job_cards");
        }
        // GET: JobCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCard jobCard = db.JobCards.Find(id);
            if (jobCard == null)
            {
                return HttpNotFound();
            }
            return View(jobCard);
        }

        // GET: JobCards/Create
        

        // POST: JobCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobCardId,IssuedDate,Status,DueDate,CustomerOrderId,EmployeeId")] JobCard jobCard)
        {
            if (ModelState.IsValid)
            {
                db.JobCards.Add(jobCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobCard);
        }

        // GET: JobCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCard jobCard = db.JobCards.Find(id);
            if (jobCard == null)
            {
                return HttpNotFound();
            }
            return View(jobCard);
        }

        // POST: JobCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobCardId,IssuedDate,Status,DueDate,CustomerOrderId,EmployeeId")] JobCard jobCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobCard);
        }

        // GET: JobCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCard jobCard = db.JobCards.Find(id);
            if (jobCard == null)
            {
                return HttpNotFound();
            }
            return View(jobCard);
        }

        // POST: JobCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobCard jobCard = db.JobCards.Find(id);
            db.JobCards.Remove(jobCard);
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
