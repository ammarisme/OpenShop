using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ETrading.DAL;
using ETrading.Models;

namespace ETrading.Controllers.API
{
    public class SalaryPaymentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SalaryPayments
        public IQueryable<SalaryPayment> GetSalaryPayments()
        {
            return db.SalaryPayments;
        }

        // GET: api/SalaryPayments/5
        [ResponseType(typeof(SalaryPayment))]
        public IHttpActionResult GetSalaryPayment(int id)
        {
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            if (salaryPayment == null)
            {
                return NotFound();
            }

            return Ok(salaryPayment);
        }

        // PUT: api/SalaryPayments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalaryPayment(int id, SalaryPayment salaryPayment)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != salaryPayment.SalaryPaymentId)
            {
                return BadRequest();
            }

            db.Entry(salaryPayment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryPaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SalaryPayments
        [ResponseType(typeof(SalaryPayment))]
        public IHttpActionResult AddSalaryPayment(SalaryPayment salaryPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalaryPayments.Add(salaryPayment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = salaryPayment.SalaryPaymentId }, salaryPayment);
        }

        // DELETE: api/SalaryPayments/5
        [ResponseType(typeof(SalaryPayment))]
        public IHttpActionResult DeleteSalaryPayment(int id)
        {
            SalaryPayment salaryPayment = db.SalaryPayments.Find(id);
            if (salaryPayment == null)
            {
                return NotFound();
            }

            db.SalaryPayments.Remove(salaryPayment);
            db.SaveChanges();

            return Ok(salaryPayment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalaryPaymentExists(int id)
        {
            return db.SalaryPayments.Count(e => e.SalaryPaymentId == id) > 0;
        }
    }
}