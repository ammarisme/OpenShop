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
using Newtonsoft.Json.Linq;

namespace ETrading.Controllers.API
{
    public class CustomerOrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CustomerOrders
        public IQueryable<CustomerOrder> GetCustomerOrders()
        {
            return db.CustomerOrders;
        }

        public IQueryable<ProductInCustomerOrder> GetProductsOfCustomerOrder(int id)
        {
            return db.ProductInCustomerOrder.Where(p => p.CustomerOrderId == id);

        }

        // GET: api/CustomerOrders/5
        [ResponseType(typeof(CustomerOrder))]
        public IHttpActionResult GetCustomerOrder(int id)
        {
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return Ok(customerOrder);
        }

        [HttpPost]
        public IHttpActionResult AddCustomerOrder(JObject jsonBody)
        {
            JObject products = (JObject)jsonBody["ProductsInCustomerOrder"]; // this variable must be present in the javascript

            jsonBody.Remove("ProductsInCustomerOrder");

            CustomerOrder customerOrder = jsonBody.ToObject<CustomerOrder>(); // the job card object

            db.CustomerOrders.Add(customerOrder);

            db.SaveChanges(); // save the shit

            int CustomerOrderId = customerOrder.CustomerOrderId; // the foregin key to be used for the -> proudcts

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

            foreach (JToken token in tokens)
            {
                JToken productJson = token.Children().First();
                ProductInCustomerOrder productInstance = productJson.ToObject<ProductInCustomerOrder>();
                productInstance.CustomerOrderId = CustomerOrderId;
                db.ProductInCustomerOrder.Add(productInstance);
            }

            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }
        // PUT: api/CustomerOrders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerOrder(int id, CustomerOrder customerOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerOrder.CustomerOrderId)
            {
                return BadRequest();
            }

            db.Entry(customerOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerOrderExists(id))
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

        // POST: api/CustomerOrders
        [ResponseType(typeof(CustomerOrder))]
        public IHttpActionResult PostCustomerOrder(CustomerOrder customerOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerOrders.Add(customerOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customerOrder.CustomerOrderId }, customerOrder);
        }

        // DELETE: api/CustomerOrders/5
        [ResponseType(typeof(CustomerOrder))]
        public IHttpActionResult DeleteCustomerOrder(int id)
        {
            CustomerOrder customerOrder = db.CustomerOrders.Find(id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            db.CustomerOrders.Remove(customerOrder);
            db.SaveChanges();

            return Ok(customerOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerOrderExists(int id)
        {
            return db.CustomerOrders.Count(e => e.CustomerOrderId == id) > 0;
        }
    }
}