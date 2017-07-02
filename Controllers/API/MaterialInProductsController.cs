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
    public class MaterialInProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MaterialInProducts
        public IQueryable<MaterialInProduct> GetMaterialInProducts()
        {
            return db.MaterialInProducts;
        }

        // GET: api/MaterialInProducts/5
        [ResponseType(typeof(MaterialInProduct))]
        public IHttpActionResult GetMaterialInProduct(int id)
        {
            MaterialInProduct materialInProduct = db.MaterialInProducts.Find(id);
            if (materialInProduct == null)
            {
                return NotFound();
            }

            return Ok(materialInProduct);
        }

        // PUT: api/MaterialInProducts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaterialInProduct(int id, MaterialInProduct materialInProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialInProduct.MaterialInProductId)
            {
                return BadRequest();
            }

            db.Entry(materialInProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialInProductExists(id))
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

        // POST: api/MaterialInProducts
        [ResponseType(typeof(MaterialInProduct))]
        public IHttpActionResult PostMaterialInProduct(MaterialInProduct materialInProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialInProducts.Add(materialInProduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materialInProduct.MaterialInProductId }, materialInProduct);
        }

        // DELETE: api/MaterialInProducts/5
        [ResponseType(typeof(MaterialInProduct))]
        public IHttpActionResult DeleteMaterialInProduct(int id)
        {
            MaterialInProduct materialInProduct = db.MaterialInProducts.Find(id);
            if (materialInProduct == null)
            {
                return NotFound();
            }

            db.MaterialInProducts.Remove(materialInProduct);
            db.SaveChanges();

            return Ok(materialInProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialInProductExists(int id)
        {
            return db.MaterialInProducts.Count(e => e.MaterialInProductId == id) > 0;
        }
    }
}