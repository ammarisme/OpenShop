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
    public class MaterialInPurchaseOrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MaterialInPurchaseOrders
        public IQueryable<MaterialInPurchaseOrder> GetMaterialInPurchaseOrders()
        {
            return db.MaterialInPurchaseOrders;
        }

        // GET: api/MaterialInPurchaseOrders/5
        [ResponseType(typeof(MaterialInPurchaseOrder))]
        public IHttpActionResult GetMaterialInPurchaseOrder(int id)
        {
            MaterialInPurchaseOrder materialInPurchaseOrder = db.MaterialInPurchaseOrders.Find(id);
            if (materialInPurchaseOrder == null)
            {
                return NotFound();
            }

            return Ok(materialInPurchaseOrder);
        }

        // PUT: api/MaterialInPurchaseOrders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaterialInPurchaseOrder(int id, MaterialInPurchaseOrder materialInPurchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialInPurchaseOrder.MaterialInPurchaseOrderId)
            {
                return BadRequest();
            }

            db.Entry(materialInPurchaseOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialInPurchaseOrderExists(id))
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

        // POST: api/MaterialInPurchaseOrders/PostMaterialInPurchaseOrder
        [ResponseType(typeof(MaterialInPurchaseOrder))]
        public IHttpActionResult PostMaterialInPurchaseOrder(MaterialInPurchaseOrder materialInPurchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialInPurchaseOrders.Add(materialInPurchaseOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materialInPurchaseOrder.MaterialInPurchaseOrderId }, materialInPurchaseOrder);
        }

        // DELETE: api/MaterialInPurchaseOrders/5
        [ResponseType(typeof(MaterialInPurchaseOrder))]
        public IHttpActionResult DeleteMaterialInPurchaseOrder(int id)
        {
            MaterialInPurchaseOrder materialInPurchaseOrder = db.MaterialInPurchaseOrders.Find(id);
            if (materialInPurchaseOrder == null)
            {
                return NotFound();
            }

            db.MaterialInPurchaseOrders.Remove(materialInPurchaseOrder);
            db.SaveChanges();

            return Ok(materialInPurchaseOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialInPurchaseOrderExists(int id)
        {
            return db.MaterialInPurchaseOrders.Count(e => e.MaterialInPurchaseOrderId == id) > 0;
        }
    }
}