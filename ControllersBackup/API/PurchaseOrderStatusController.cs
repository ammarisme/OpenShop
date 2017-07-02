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
    public class PurchaseOrderStatusController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/PurchaseOrderStatus
        public IQueryable<PurchaseOrderStatus> GetPurchaseOrderStatus()
        {
            return db.PurchaseOrderStatus;
        }

        // GET: api/PurchaseOrderStatus/5
        [ResponseType(typeof(PurchaseOrderStatus))]
        public IHttpActionResult GetPurchaseOrderStatus(int id)
        {
            PurchaseOrderStatus purchaseOrderStatus = db.PurchaseOrderStatus.Find(id);
            if (purchaseOrderStatus == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrderStatus);
        }

        // PUT: api/PurchaseOrderStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseOrderStatus(int id, PurchaseOrderStatus purchaseOrderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseOrderStatus.PurchaseOrderStatusId)
            {
                return BadRequest();
            }

            db.Entry(purchaseOrderStatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderStatusExists(id))
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

        // POST: api/PurchaseOrderStatus
        [ResponseType(typeof(PurchaseOrderStatus))]
        public IHttpActionResult PostPurchaseOrderStatus(PurchaseOrderStatus purchaseOrderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseOrderStatus.Add(purchaseOrderStatus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseOrderStatus.PurchaseOrderStatusId }, purchaseOrderStatus);
        }

        // DELETE: api/PurchaseOrderStatus/5
        [ResponseType(typeof(PurchaseOrderStatus))]
        public IHttpActionResult DeletePurchaseOrderStatus(int id)
        {
            PurchaseOrderStatus purchaseOrderStatus = db.PurchaseOrderStatus.Find(id);
            if (purchaseOrderStatus == null)
            {
                return NotFound();
            }

            db.PurchaseOrderStatus.Remove(purchaseOrderStatus);
            db.SaveChanges();

            return Ok(purchaseOrderStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseOrderStatusExists(int id)
        {
            return db.PurchaseOrderStatus.Count(e => e.PurchaseOrderStatusId == id) > 0;
        }
    }
}