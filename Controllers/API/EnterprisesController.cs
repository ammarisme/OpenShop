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
using WholesaleTradingPortal.DAL;
using WholesaleTradingPortal.Models;

namespace WholesaleTradingPortal.Controllers.API
{
    public class EnterprisesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Enterprises
        public IQueryable<Enterprise> GetEnterprises()
        {
            return db.Enterprises;
        }

        // GET the Enterprise name given the id
        public IHttpActionResult GetEnterpriseName(int id)
        {
            Enterprise Enterprise = db.Enterprises.Find(id);
            return Ok(Enterprise.EnterpriseName);
        }

        // GET: api/Enterprises/5
        [ResponseType(typeof(Enterprise))]
        public IHttpActionResult GetEnterprise(string id)
        {
            Enterprise Enterprise = db.Enterprises.Find(id);
            if (Enterprise == null)
            {
                return NotFound();
            }

            return Ok(Enterprise);
        }

        // PUT: api/Enterprises/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnterprise(int id, Enterprise Enterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Enterprise.EnterpriseId)
            {
                return BadRequest();
            }

            db.Entry(Enterprise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnterpriseExists(id))
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

        // POST: api/Enterprises
        [ResponseType(typeof(Enterprise))]
        public IHttpActionResult PostEnterprise(Enterprise Enterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Enterprises.Add(Enterprise);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EnterpriseExists(Enterprise.EnterpriseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = Enterprise.EnterpriseId }, Enterprise);
        }

        // DELETE: api/Enterprises/5
        [ResponseType(typeof(Enterprise))]
        public IHttpActionResult DeleteEnterprise(string id)
        {
            Enterprise Enterprise = db.Enterprises.Find(id);
            if (Enterprise == null)
            {
                return NotFound();
            }

            db.Enterprises.Remove(Enterprise);
            db.SaveChanges();

            return Ok(Enterprise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnterpriseExists(int id)
        {
            return db.Enterprises.Count(e => e.EnterpriseId == id) > 0;
        }
    }
}