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
    public class MaterialInMaterialsRecievedsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MaterialInMaterialsRecieveds
        public IQueryable<MaterialInMaterialsRecieved> GetMaterialInMaterialsRecieveds()
        {
            return db.MaterialInMaterialsRecieveds;
        }

        // GET: api/MaterialInMaterialsRecieveds/5
        [ResponseType(typeof(MaterialInMaterialsRecieved))]
        public IHttpActionResult GetMaterialInMaterialsRecieved(int id)
        {
            MaterialInMaterialsRecieved materialInMaterialsRecieved = db.MaterialInMaterialsRecieveds.Find(id);
            if (materialInMaterialsRecieved == null)
            {
                return NotFound();
            }

            return Ok(materialInMaterialsRecieved);
        }

        // PUT: api/MaterialInMaterialsRecieveds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaterialInMaterialsRecieved(int id, MaterialInMaterialsRecieved materialInMaterialsRecieved)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialInMaterialsRecieved.MaterialInMaterialsRecievedId)
            {
                return BadRequest();
            }

            db.Entry(materialInMaterialsRecieved).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialInMaterialsRecievedExists(id))
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

        // POST: api/MaterialInMaterialsRecieveds
        [ResponseType(typeof(MaterialInMaterialsRecieved))]
        public IHttpActionResult PostMaterialInMaterialsRecieved(MaterialInMaterialsRecieved materialInMaterialsRecieved)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialInMaterialsRecieveds.Add(materialInMaterialsRecieved);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materialInMaterialsRecieved.MaterialInMaterialsRecievedId }, materialInMaterialsRecieved);
        }

        // DELETE: api/MaterialInMaterialsRecieveds/5
        [ResponseType(typeof(MaterialInMaterialsRecieved))]
        public IHttpActionResult DeleteMaterialInMaterialsRecieved(int id)
        {
            MaterialInMaterialsRecieved materialInMaterialsRecieved = db.MaterialInMaterialsRecieveds.Find(id);
            if (materialInMaterialsRecieved == null)
            {
                return NotFound();
            }

            db.MaterialInMaterialsRecieveds.Remove(materialInMaterialsRecieved);
            db.SaveChanges();

            return Ok(materialInMaterialsRecieved);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialInMaterialsRecievedExists(int id)
        {
            return db.MaterialInMaterialsRecieveds.Count(e => e.MaterialInMaterialsRecievedId == id) > 0;
        }
    }
}