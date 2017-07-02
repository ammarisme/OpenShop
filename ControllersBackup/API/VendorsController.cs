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
    public class VendorsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Vendors
        public List<Vendor> GetVendors()
        {
            return db.Vendor.ToList();
        }

        // GET: api/Vendors/5
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult GetVendors(int id)
        {
            Vendor vendor = db.Vendor.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        // PUT: api/Vendors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendor(int id, Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendor.VendorId)
            {
                return BadRequest();
            }

            db.Entry(vendor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        //// POST: api/Vendors
        //[ResponseType(typeof(Vendor))]
        //public IHttpActionResult PostVendor(Vendor vendor)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Vendor.Add(vendor);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = vendor.VendorId }, vendor);
        //}

        [ResponseType(typeof(Vendor))]
        public IHttpActionResult AddOrUpdateVendor(Vendor vendor)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (VendorExists(vendor.VendorId))
            {
                db.Entry(vendor).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
            db.Vendor.Add(vendor);
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendor.VendorId }, vendor);
        }
        // DELETE: api/Vendors/5
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult DeleteVendor(int id)
        {
            Vendor vendor = db.Vendor.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            db.Vendor.Remove(vendor);
            db.SaveChanges();

            return Ok(vendor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorExists(int id)
        {
            return db.Vendor.Count(e => e.VendorId == id) > 0;
        }
    }
}