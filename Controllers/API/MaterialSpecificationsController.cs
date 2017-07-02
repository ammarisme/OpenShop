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
    public class MaterialSpecificationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MaterialSpecifications
        public IQueryable<MaterialSpecification> GetMaterialSpecifications()
        {
            return db.MaterialSpecifications;
        }

        // GET: api/MaterialSpecifications/5
        [ResponseType(typeof(MaterialSpecification))]
        public IHttpActionResult GetMaterialSpecification(int id)
        {
            MaterialSpecification materialSpecification = db.MaterialSpecifications.Find(id);
            if (materialSpecification == null)
            {
                return NotFound();
            }

            return Ok(materialSpecification);
        }

        // PUT: api/MaterialSpecifications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaterialSpecification(int id, MaterialSpecification materialSpecification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialSpecification.MaterialSpecificationId)
            {
                return BadRequest();
            }

            db.Entry(materialSpecification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialSpecificationExists(id))
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

        // POST: api/MaterialSpecifications
        [ResponseType(typeof(MaterialSpecification))]
        public IHttpActionResult PostMaterialSpecification(MaterialSpecification materialSpecification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialSpecifications.Add(materialSpecification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materialSpecification.MaterialSpecificationId }, materialSpecification);
        }

        // DELETE: api/MaterialSpecifications/5
        [ResponseType(typeof(MaterialSpecification))]
        public IHttpActionResult DeleteMaterialSpecification(int id)
        {
            MaterialSpecification materialSpecification = db.MaterialSpecifications.Find(id);
            if (materialSpecification == null)
            {
                return NotFound();
            }

            db.MaterialSpecifications.Remove(materialSpecification);
            db.SaveChanges();

            return Ok(materialSpecification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialSpecificationExists(int id)
        {
            return db.MaterialSpecifications.Count(e => e.MaterialSpecificationId == id) > 0;
        }
    }
}