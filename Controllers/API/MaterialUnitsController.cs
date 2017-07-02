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
    public class MaterialUnitsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MaterialUnits
        public IQueryable<MaterialUnit> GetMaterialUnits()
        {
            return db.MaterialUnits;
        }

        // GET: api/MaterialUnits/5
        [ResponseType(typeof(MaterialUnit))]
        public IHttpActionResult GetMaterialUnit(int id)
        {
            MaterialUnit materialUnit = db.MaterialUnits.Find(id);
            if (materialUnit == null)
            {
                return NotFound();
            }

            return Ok(materialUnit);
        }

        // PUT: api/MaterialUnits/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaterialUnit(int id, MaterialUnit materialUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialUnit.MaterialUnitId)
            {
                return BadRequest();
            }

            db.Entry(materialUnit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialUnitExists(id))
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

        // POST: api/MaterialUnits
        [ResponseType(typeof(MaterialUnit))]
        public IHttpActionResult PostMaterialUnit(MaterialUnit materialUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialUnits.Add(materialUnit);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materialUnit.MaterialUnitId }, materialUnit);
        }

        // DELETE: api/MaterialUnits/5
        [ResponseType(typeof(MaterialUnit))]
        public IHttpActionResult DeleteMaterialUnit(int id)
        {
            MaterialUnit materialUnit = db.MaterialUnits.Find(id);
            if (materialUnit == null)
            {
                return NotFound();
            }

            db.MaterialUnits.Remove(materialUnit);
            db.SaveChanges();

            return Ok(materialUnit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialUnitExists(int id)
        {
            return db.MaterialUnits.Count(e => e.MaterialUnitId == id) > 0;
        }
    }
}