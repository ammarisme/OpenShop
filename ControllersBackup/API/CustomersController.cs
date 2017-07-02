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
using ETrading.Models;
using ETrading.DAL;

namespace ETrading.Controllers.API
{
    public class CustomersController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        private ApplicationDbContext appDb= new ApplicationDbContext();
        [HttpGet]
        public IEnumerable<CustomerOrder> CustomerOrders(string id)
        {
            IEnumerable<CustomerOrder> list = db.CustomerOrders.Where(a => a.CustomerId  == id);

            return list;
        }

        // GET: api/Customers/GetCustomers
        
        public IQueryable<Customer> GetCustomers()
        {
            return appDb.Customers;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(string id)
        {
            Customer customer = appDb.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }
            var response = Request.CreateResponse<Customer>(HttpStatusCode.OK, customer);
            Console.Write(response.Content.ToString());
            return Ok(customer);
        }

        [ResponseType(typeof(Customer))]
        public IHttpActionResult AddOrUpdateCustomer(Customer customer)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (CustomerExists(customer.Id))
            {
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                appDb.Customers.Add(customer);
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.Id}, customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(string id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
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
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            appDb.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.Id}, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(string id)
        {
            Customer customer = appDb.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            appDb.Customers.Remove(customer);
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

        private bool CustomerExists(string id)
        {
            return appDb.Customers.Count(e => e.Id== id) > 0;
        }
    }
}