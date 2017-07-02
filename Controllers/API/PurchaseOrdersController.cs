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
using Newtonsoft.Json.Linq;
namespace ETrading.Controllers.API
{
    
     
    public class PurchaseOrdersController : ApiController
    {   
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<string> statuses; // list of possible statuses

        // GET: api/PurchaseOrders/GetPurchaseOrder
        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return db.PurchaseOrder.ToList();
        }

        [HttpPost]
        public IHttpActionResult AddPurchaseOrder(JObject jsonBody)
        {
            JObject materials = (JObject)jsonBody["MaterialsInPurchaseOrder"]; // this variable must be present in the javascript

            jsonBody.Remove("MaterialsInPurchaseOrder");

            PurchaseOrder purchaseOrder = jsonBody.ToObject<PurchaseOrder>(); // the job card object

            db.PurchaseOrder.Add(purchaseOrder);

            db.SaveChanges(); // save the shit

            int purchaseOrderId = purchaseOrder.PurchaseOrderId; // the foregin key to be used for the -> proudcts

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)materials.Children<JToken>();

            foreach (JToken token in tokens)
            {
                JToken materialsJson = token.Children().First();
                MaterialInPurchaseOrder materialInstance = materialsJson.ToObject<MaterialInPurchaseOrder>();
                materialInstance.PurchaseOrderId= purchaseOrderId;
                db.MaterialInPurchaseOrders.Add(materialInstance);
            }

            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }
        // GET: api/PurchaseOrders/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult GetPurchaseOrder(int id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrder);
        }

        public IQueryable<MaterialInPurchaseOrder> GetMaterialsOfPurchaseOrder(int id) {
            return db.MaterialInPurchaseOrders.Where(m => m.PurchaseOrderId == id);
        }

        public List<string> GetStatuses() {
            statuses = new List<string> { };
            statuses.Add("Active");
            statuses.Add("Pending");
            statuses.Add("Closed");
            statuses.Add("Done");

            return statuses;
        }
        // PUT: api/PurchaseOrders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseOrder.PurchaseOrderId)
            {
                return BadRequest();
            }

            db.Entry(purchaseOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderExists(id))
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

        // POST: api/PurchaseOrders
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseOrder.Add(purchaseOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseOrder.PurchaseOrderId }, purchaseOrder);
        }

        // DELETE: api/PurchaseOrders/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult DeletePurchaseOrder(int id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            db.PurchaseOrder.Remove(purchaseOrder);
            db.SaveChanges();

            return Ok(purchaseOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseOrderExists(int id)
        {
            return db.PurchaseOrder.Count(e => e.PurchaseOrderId == id) > 0;
        }
    }
}