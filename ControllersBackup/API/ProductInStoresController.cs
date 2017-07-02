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
    public class ProductInStoresController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/ProductInStores
        public IQueryable<ProductInStore> GetProductInStores()
        {
            return db.ProductInStores;
        }

        // GET: api/ProductInStores/5
        [ResponseType(typeof(ProductInStore))]
        public IHttpActionResult GetProductInStore(int id)
        {
            ProductInStore productInStore = db.ProductInStores.Find(id);
            if (productInStore == null)
            {
                return NotFound();
            }

            return Ok(productInStore);
        }

        // PUT: api/ProductInStores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductInStore(int id, ProductInStore productInStore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productInStore.Id)
            {
                return BadRequest();
            }

            db.Entry(productInStore).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInStoreExists(id))
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

        // POST: api/ProductInStores
        [ResponseType(typeof(ProductInStore))]
        public IHttpActionResult PostProductInStore(ProductInStore productInStore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductInStores.Add(productInStore);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productInStore.Id }, productInStore);
        }

        // DELETE: api/ProductInStores/5
        [ResponseType(typeof(ProductInStore))]
        public IHttpActionResult DeleteProductInStore(int id)
        {
            ProductInStore productInStore = db.ProductInStores.Find(id);
            if (productInStore == null)
            {
                return NotFound();
            }

            db.ProductInStores.Remove(productInStore);
            db.SaveChanges();

            return Ok(productInStore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductInStoreExists(int id)
        {
            return db.ProductInStores.Count(e => e.Id == id) > 0;
        }
    }
}