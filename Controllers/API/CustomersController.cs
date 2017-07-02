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
using RetailTradingPortal.DAL;
using RetailTradingPortal.Models;
using RetailTradingPortal.Areas.Customers.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace RetailTradingPortal.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Customers
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(string id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [Authorize]
        public IHttpActionResult AddCustomer(AddCustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer dbCustomer = new Customer();
            dbCustomer.FirstName = customer.FirstName;
            dbCustomer.LastName = customer.LastName;
            dbCustomer.BillingAddress = customer.BillingAddress;
            dbCustomer.City = customer.City;
            dbCustomer.Status = "Active";
            dbCustomer.PhoneNumber = customer.PhoneNumber;
            dbCustomer.Remark = customer.Remark;

            db.Customers.Add(dbCustomer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(dbCustomer.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dbCustomer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(string id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }
    }
}