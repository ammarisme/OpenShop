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

namespace ETrading.Controllers
{
    public class VendorStatusController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/VendorStatus
        public IQueryable<VendorStatus> GetVendorStatuses()
        {
            return db.VendorStatuses;
        }

        // GET: api/VendorStatus/5
        [ResponseType(typeof(VendorStatus))]
        public IHttpActionResult GetVendorStatus(int id)
        {
            VendorStatus vendorStatus = db.VendorStatuses.Find(id);
            if (vendorStatus == null)
            {
                return NotFound();
            }

            return Ok(vendorStatus);
        }

        // PUT: api/VendorStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendorStatus(int id, VendorStatus vendorStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorStatus.VendorStatusId)
            {
                return BadRequest();
            }

            db.Entry(vendorStatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorStatusExists(id))
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

        // POST: api/VendorStatus
        [ResponseType(typeof(VendorStatus))]
        public IHttpActionResult PostVendorStatus(VendorStatus vendorStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendorStatuses.Add(vendorStatus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendorStatus.VendorStatusId }, vendorStatus);
        }

        // DELETE: api/VendorStatus/5
        [ResponseType(typeof(VendorStatus))]
        public IHttpActionResult DeleteVendorStatus(int id)
        {
            VendorStatus vendorStatus = db.VendorStatuses.Find(id);
            if (vendorStatus == null)
            {
                return NotFound();
            }

            db.VendorStatuses.Remove(vendorStatus);
            db.SaveChanges();

            return Ok(vendorStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorStatusExists(int id)
        {
            return db.VendorStatuses.Count(e => e.VendorStatusId == id) > 0;
        }
    }
}