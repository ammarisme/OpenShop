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
    public class MaterialInStoresController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/MaterialInStores
        public IQueryable<MaterialInStore> GetMaterialInStores()
        {
            return db.MaterialInStores;
        }

        // GET: api/MaterialInStores/5
        [ResponseType(typeof(MaterialInStore))]
        public IHttpActionResult GetMaterialInStore(int id)
        {
            MaterialInStore materialInStore = db.MaterialInStores.Find(id);
            if (materialInStore == null)
            {
                return NotFound();
            }

            return Ok(materialInStore);
        }

        // PUT: api/MaterialInStores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaterialInStore(int id, MaterialInStore materialInStore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialInStore.Id)
            {
                return BadRequest();
            }

            db.Entry(materialInStore).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialInStoreExists(id))
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

        // POST: api/MaterialInStores
        [ResponseType(typeof(MaterialInStore))]
        public IHttpActionResult PostMaterialInStore(MaterialInStore materialInStore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialInStores.Add(materialInStore);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materialInStore.Id }, materialInStore);
        }

        // DELETE: api/MaterialInStores/5
        [ResponseType(typeof(MaterialInStore))]
        public IHttpActionResult DeleteMaterialInStore(int id)
        {
            MaterialInStore materialInStore = db.MaterialInStores.Find(id);
            if (materialInStore == null)
            {
                return NotFound();
            }

            db.MaterialInStores.Remove(materialInStore);
            db.SaveChanges();

            return Ok(materialInStore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialInStoreExists(int id)
        {
            return db.MaterialInStores.Count(e => e.Id == id) > 0;
        }
    }
}