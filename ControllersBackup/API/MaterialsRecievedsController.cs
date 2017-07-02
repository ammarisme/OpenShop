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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ETrading.Controllers.API
{
    public class MaterialsRecievedsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/MaterialsRecieveds
        public IQueryable<MaterialsRecieved> GetMaterialsRecieveds()
        {
            return db.MaterialsRecieveds;
        }

        // GET: api/MaterialsRecieveds/5
        [ResponseType(typeof(MaterialsRecieved))]
        public IHttpActionResult GetMaterialsRecieved(int id)
        {
            MaterialsRecieved materialsRecieved = db.MaterialsRecieveds.Find(id);
            if (materialsRecieved == null)
            {
                return NotFound();
            }
            return Ok(materialsRecieved);
        }

        [HttpPost]
        public IHttpActionResult AddMaterialsRecieved(JObject jsonBody)
        {
            JObject materials = (JObject)jsonBody["MaterialInMaterialsRecieved"];

            int length = (int)materials["length"];

            jsonBody.Remove("MaterialInMaterialsRecieved");
            MaterialsRecieved materialsRecieved = jsonBody.ToObject<MaterialsRecieved>();

            db.MaterialsRecieveds.Add(materialsRecieved);
            
            db.SaveChanges();

            int MaterialsRecievedId = materialsRecieved.MaterialsRecievedId;

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)materials.Children<JToken>();
            int i= 1 ;// to control the interation
            foreach (JToken token in tokens)
            {
                if (i <= length){
                JToken material = token.Children().First();
                MaterialInMaterialsRecieved materialDb = material.ToObject<MaterialInMaterialsRecieved>();
                materialDb.MaterialsRecievedId = MaterialsRecievedId;
                db.MaterialInMaterialsRecieveds.Add(materialDb);
                i++;
                }else{
                    break;
                }
            }
            db.SaveChanges();
            return Ok(MaterialsRecievedId);
        }

        // PUT: api/MaterialsRecieveds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaterialsRecieved(int id, MaterialsRecieved materialsRecieved)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialsRecieved.MaterialsRecievedId)
            {
                return BadRequest();
            }

            db.Entry(materialsRecieved).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialsRecievedExists(id))
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

        // POST: api/MaterialsRecieveds
        [ResponseType(typeof(MaterialsRecieved))]
        public IHttpActionResult PostMaterialsRecieved(MaterialsRecieved materialsRecieved)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialsRecieveds.Add(materialsRecieved);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materialsRecieved.MaterialsRecievedId }, materialsRecieved);
        }

        // DELETE: api/MaterialsRecieveds/5
        [ResponseType(typeof(MaterialsRecieved))]
        public IHttpActionResult DeleteMaterialsRecieved(int id)
        {
            MaterialsRecieved materialsRecieved = db.MaterialsRecieveds.Find(id);
            if (materialsRecieved == null)
            {
                return NotFound();
            }

            db.MaterialsRecieveds.Remove(materialsRecieved);
            db.SaveChanges();

            return Ok(materialsRecieved);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialsRecievedExists(int id)
        {
            return db.MaterialsRecieveds.Count(e => e.MaterialsRecievedId == id) > 0;
        }
    }
}