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
    public class MaterialInJobCardsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/MaterialInJobCards
        public IQueryable<MaterialInJobCard> GetMaterialInJobCards()
        {
            return db.MaterialInJobCards;
        }

        // GET: api/MaterialInJobCards/5
        [ResponseType(typeof(MaterialInJobCard))]
        public IHttpActionResult GetMaterialInJobCard(int id)
        {
            MaterialInJobCard materialInJobCard = db.MaterialInJobCards.Find(id);
            if (materialInJobCard == null)
            {
                return NotFound();
            }

            return Ok(materialInJobCard);
        }

        // PUT: api/MaterialInJobCards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaterialInJobCard(int id, MaterialInJobCard materialInJobCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialInJobCard.MaterialInJobCardId)
            {
                return BadRequest();
            }

            db.Entry(materialInJobCard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialInJobCardExists(id))
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

        // POST: api/MaterialInJobCards
        [ResponseType(typeof(MaterialInJobCard))]
        public IHttpActionResult PostMaterialInJobCard(MaterialInJobCard materialInJobCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialInJobCards.Add(materialInJobCard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materialInJobCard.MaterialInJobCardId }, materialInJobCard);
        }

        // DELETE: api/MaterialInJobCards/5
        [ResponseType(typeof(MaterialInJobCard))]
        public IHttpActionResult DeleteMaterialInJobCard(int id)
        {
            MaterialInJobCard materialInJobCard = db.MaterialInJobCards.Find(id);
            if (materialInJobCard == null)
            {
                return NotFound();
            }

            db.MaterialInJobCards.Remove(materialInJobCard);
            db.SaveChanges();

            return Ok(materialInJobCard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialInJobCardExists(int id)
        {
            return db.MaterialInJobCards.Count(e => e.MaterialInJobCardId == id) > 0;
        }
    }
}