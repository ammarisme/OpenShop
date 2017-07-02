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
    public class ProductInCustomerOrdersController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/ProductInCustomerOrders
        public IQueryable<ProductInCustomerOrder> GetProductInCustomerOrder()
        {
            return db.ProductInCustomerOrder;
        }

        // GET: api/ProductInCustomerOrders/5
        [ResponseType(typeof(ProductInCustomerOrder))]
        public IHttpActionResult GetProductInCustomerOrder(int id)
        {
            ProductInCustomerOrder productInCustomerOrder = db.ProductInCustomerOrder.Find(id);
            if (productInCustomerOrder == null)
            {
                return NotFound();
            }

            return Ok(productInCustomerOrder);
        }

        // PUT: api/ProductInCustomerOrders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductInCustomerOrder(int id, ProductInCustomerOrder productInCustomerOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productInCustomerOrder.ProductsInCustomerOrderId)
            {
                return BadRequest();
            }

            db.Entry(productInCustomerOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInCustomerOrderExists(id))
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

        // POST: api/ProductInCustomerOrders
        [ResponseType(typeof(ProductInCustomerOrder))]
        public IHttpActionResult PostProductInCustomerOrder(ProductInCustomerOrder productInCustomerOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductInCustomerOrder.Add(productInCustomerOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productInCustomerOrder.ProductsInCustomerOrderId }, productInCustomerOrder);
        }

        // DELETE: api/ProductInCustomerOrders/5
        [ResponseType(typeof(ProductInCustomerOrder))]
        public IHttpActionResult DeleteProductInCustomerOrder(int id)
        {
            ProductInCustomerOrder productInCustomerOrder = db.ProductInCustomerOrder.Find(id);
            if (productInCustomerOrder == null)
            {
                return NotFound();
            }

            db.ProductInCustomerOrder.Remove(productInCustomerOrder);
            db.SaveChanges();

            return Ok(productInCustomerOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductInCustomerOrderExists(int id)
        {
            return db.ProductInCustomerOrder.Count(e => e.ProductsInCustomerOrderId == id) > 0;
        }
    }
}