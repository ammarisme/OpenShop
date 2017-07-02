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
using RetailTradingPortal.Areas.Products.Models;
using RetailTradingPortal.Areas.PurchaseOrders.Models;

namespace RetailTradingPortal.Controllers.API
{
    public class PurchaseOrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/WholeSales
        public IQueryable<PurchaseOrder> GetPurchaseOrders()
        {
            return db.PurchaseOrders;
        }

        [HttpPost]
        public HttpResponseMessage AddOrder(JObject jsonBody)
        {
            JObject products = (JObject)jsonBody["ProductsInPurchaseOrders"]; // this variable must be present in the javascript

            jsonBody.Remove("ProductsInPurchaseOrders");

            PurchaseOrder wholesaleOrder = jsonBody.ToObject<PurchaseOrder>(); // the job card object

            wholesaleOrder.AccountId = User.Identity.GetUserId();

            db.PurchaseOrders.Add(wholesaleOrder);

            db.SaveChanges();

            int wholesaleOrderId = wholesaleOrder.OrderId; // the foregin key to be used for the -> proudcts

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

            foreach (JToken token in tokens)
            {
                JToken productJson = token.Children().First();
                ProductInPurchaseOrder productInstance = productJson.ToObject<ProductInPurchaseOrder>();
                productInstance.PurchaseOrderId = wholesaleOrderId;
                db.ProductsInPurchaseOrders.Add(productInstance);
            }

            db.SaveChanges();
            return this.Request.CreateResponse(HttpStatusCode.Created,wholesaleOrderId);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateProductInWholesaleOrder(ProductInPurchaseOrder product)
        {
            if (!WholesaleOrderExists(product.PurchaseOrderId))
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
            // else
            db.Entry(product).State = EntityState.Modified;

            db.SaveChanges();

            return StatusCode(HttpStatusCode.Created);
        }

        public IHttpActionResult GetProductsInPurchaseOrder(int id)
        {
            // get all the products of the PO
            var products = db.ProductsInPurchaseOrders.Where(p => p.PurchaseOrderId == id);
            List<ProductsInPurchaseOrderView> productsInPo = new List<ProductsInPurchaseOrderView>();

            var joinNative = (
                              from pop in products
                              join p in db.Products on pop.ProductId equals p.ProductId
                              where p.ProductId == pop.ProductId
                              select new { 
                                ProductInPurchaseOrderId = pop.ProductInPurchaseOrderId ,
                                ProductId = p.ProductId ,
                                ProductName = p.ProductName,
                                Quantity = pop.Quantity,
                                Cost = pop.Cost,
                                Remark = pop.Remark
                              }
                                  );

            
            return Ok(joinNative);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateOrder(PurchaseOrder order)
        {
            if (!WholesaleOrderExists(order.OrderId))
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
            // else
            db.Entry(order).State = EntityState.Modified;

            db.SaveChanges();

            return StatusCode(HttpStatusCode.Created);
        }

        // GET: api/WholeSales/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult GetWholesaleOrder(int id)
        {
            PurchaseOrder wholesaleOrder = db.PurchaseOrders.Find(id);
            if (wholesaleOrder == null)
            {
                return NotFound();
            }

            return Ok(wholesaleOrder);
        }

        // PUT: api/WholeSales/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWholesaleOrder(int id, PurchaseOrder wholesaleOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wholesaleOrder.OrderId)
            {
                return BadRequest();
            }

            db.Entry(wholesaleOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WholesaleOrderExists(id))
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

        // POST: api/WholeSales
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult PostWholesaleOrder(PurchaseOrder wholesaleOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseOrders.Add(wholesaleOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = wholesaleOrder.OrderId }, wholesaleOrder);
        }

        // DELETE: api/WholeSales/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult DeleteWholesaleOrder(int id)
        {
            PurchaseOrder wholesaleOrder = db.PurchaseOrders.Find(id);
            if (wholesaleOrder == null)
            {
                return NotFound();
            }

            db.PurchaseOrders.Remove(wholesaleOrder);
            db.SaveChanges();

            return Ok(wholesaleOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WholesaleOrderExists(int id)
        {
            return db.PurchaseOrders.Count(e => e.OrderId == id) > 0;
        }
    }
}