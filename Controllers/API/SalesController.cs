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
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;

namespace RetailTradingPortal.Controllers.API
{
    public class SalesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Sales
        //public IHttpActionResult GetSales()
        //{
        //    var sales = (
        //        from sale in db.OnlineOrders
        //        join customers in db.Customers on sale.CustomerId equals customers.CustomerId
        //        where customers.CustomerId == sale.CustomerId
        //        select new { 
        //            OrderId = sale.OnlineOrderId,
        //            OrderDate = sale.OrderDate,
        //            OrderDueDate = sale.OrderDueDate,
        //            OrderStatus = sale.OrderStatus,
        //            CustomerFullName  = customers.FirstName + " " + customers.LastName ,
        //            DeliveredDate = sale.DeliveredDate ,
        //            DeliveryStatus = sale.DeliveryStatus,
        //            DeliveryMode = sale.DeliveryMode,
        //            PaymentMethod = sale.PaymentMethod,
        //            PaymentDuration = sale.PaymentDuration ,
        //            Remark = sale.Remark
        //        }
        //        );
        //    return Ok(sales);
        //}

        public class SaleStatus
        {
            public int OrderId { get; set; }

            public string Status { get; set; }
        }
        public IHttpActionResult ChangeSaleStatus(JObject jsonBody)
        {

            SaleStatus sale = jsonBody.ToObject<SaleStatus>();
            if (db.OnlineOrders.Count(s => s.OnlineOrderId == sale.OrderId) > 0)
            {
                OnlineOrder originalSale = db.OnlineOrders.Find(sale.OrderId);
                originalSale.OrderStatus = sale.Status;
                db.Entry(originalSale).State = EntityState.Modified;
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddOrder(JObject jsonBody)
        {
            JObject products = (JObject)jsonBody["ProductsRetailOrder"]; // this variable must be present in the javascript

            jsonBody.Remove("ProductsRetailOrder");

            OnlineOrder retailOrder= jsonBody.ToObject<OnlineOrder>(); // the job card object

            //retailOrder.CustomerId = User.Identity.GetUserId();

            db.OnlineOrders.Add(retailOrder);

            db.SaveChanges(); 

            int retailOrderId = retailOrder.OnlineOrderId; // the foregin key to be used for the -> proudcts

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

            foreach (JToken token in tokens)
            {
                JToken productJson = token.Children().First();
                ProductInOnlineOrder productInstance = productJson.ToObject<ProductInOnlineOrder>();
                productInstance.OnlineOrderId = retailOrderId;
                db.ProductsInOnlineOrders.Add(productInstance);
            }

            db.SaveChanges();
            return this.Request.CreateResponse(HttpStatusCode.Created, retailOrderId);
        }


        [HttpPost]
        public HttpResponseMessage AddOnlineOrder(JObject jsonBody)
        {
            JObject products = (JObject)jsonBody["ProductsWholesaleOrder"]; // this variable must be present in the javascript

            jsonBody.Remove("ProductsWholesaleOrder");

            OnlineOrder wholesaleOrder = jsonBody.ToObject<OnlineOrder>(); // the job card object

            //wholesaleOrder.AccountId = User.Identity.GetUserId();

            db.OnlineOrders.Add(wholesaleOrder);

            db.SaveChanges();

            int retailOrderId = wholesaleOrder.OnlineOrderId; // the foregin key to be used for the -> proudcts

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

            foreach (JToken token in tokens)
            {
                JToken productJson = token.Children().First();
                ProductInOnlineOrder productInstance = productJson.ToObject<ProductInOnlineOrder>();
                productInstance.OnlineOrderId = retailOrderId;
                db.ProductsInOnlineOrders.Add(productInstance);
            }

            db.SaveChanges();
            return this.Request.CreateResponse(HttpStatusCode.Created, retailOrderId);
        }

        // GET: api/Sales/5
        [ResponseType(typeof(OnlineOrder))]
        public IHttpActionResult GetRetailOrder(int id)
        {
            OnlineOrder retailOrder = db.OnlineOrders.Find(id);
            if (retailOrder == null)
            {
                return NotFound();
            }

            return Ok(retailOrder);
        }

        public IHttpActionResult GetProductsInRetailOrder(int id)
        {
            var products = db.ProductsInOnlineOrders.Where(p => p.OnlineOrderId == id);
            return Ok(products);
        }

        // PUT: api/Sales/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRetailOrder(int id, OnlineOrder retailOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != retailOrder.OnlineOrderId)
            {
                return BadRequest();
            }

            db.Entry(retailOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RetailOrderExists(id))
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

        // POST: api/Sales
        [ResponseType(typeof(OnlineOrder))]
        public IHttpActionResult PostRetailOrder(OnlineOrder retailOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OnlineOrders.Add(retailOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = retailOrder.OnlineOrderId }, retailOrder);
        }

        // DELETE: api/Sales/5
        [ResponseType(typeof(OnlineOrder))]
        public IHttpActionResult DeleteRetailOrder(int id)
        {
            OnlineOrder retailOrder = db.OnlineOrders.Find(id);
            if (retailOrder == null)
            {
                return NotFound();
            }

            db.OnlineOrders.Remove(retailOrder);
            db.SaveChanges();

            return Ok(retailOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RetailOrderExists(int id)
        {
            return db.OnlineOrders.Count(e => e.OnlineOrderId == id) > 0;
        }
    }
}