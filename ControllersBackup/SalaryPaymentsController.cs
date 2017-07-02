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
    public class SalaryPaymentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SalaryPayments

        public ActionResult CreateViewSalaryPayments()
        {
            ViewBag.employees = db.Employees;
            return View("_create_view_salary_payments");
        }
        public ActionResult Index()
        {
            return View(db.SalaryPayments.ToList());
        }

        // GET: SalaryPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            if (salaryPayment == null)
            {
                return HttpNotFound();
            }
            return View(salaryPayment);
        }

        // GET: SalaryPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalaryPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaryPaymentId,Amount,EmployeeId,PaidDate,Remark")] SalaryPayment salaryPayment)
        {
            if (ModelState.IsValid)
            {
                db.SalaryPayments.Add(salaryPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salaryPayment);
        }

        // GET: SalaryPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            if (salaryPayment == null)
            {
                return HttpNotFound();
            }
            return View(salaryPayment);
        }

        // POST: SalaryPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryPaymentId,Amount,EmployeeId,PaidDate,Remark")] SalaryPayment salaryPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salaryPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salaryPayment);
        }

        // GET: SalaryPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            if (salaryPayment == null)
            {
                return HttpNotFound();
            }
            return View(salaryPayment);
        }

        // POST: SalaryPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            db.SalaryPayments.Remove(salaryPayment);
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
