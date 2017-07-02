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
    public class MaterialsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: api/Materials
        public IQueryable<Material> GetMaterials()
        {
            return db.Materials;
        }

        [HttpPost]
        public IHttpActionResult AddMaterialsRecieved(JObject jsonBody)
        {
            JObject materials = (JObject)jsonBody["MaterialInMaterialsRecieved"];

            jsonBody.Remove("MaterialInMaterialsRecieved");
            MaterialsRecieved materialsRecieved = jsonBody.ToObject<MaterialsRecieved>();

            db.MaterialsRecieveds.Add(materialsRecieved);

            db.SaveChanges();

            int MaterialsRecievedId = materialsRecieved.MaterialsRecievedId;

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)materials.Children<JToken>();
            int i = 1;// to control the interation
            foreach (JToken token in tokens)
            {
                    JToken material = token.Children().First();
                    MaterialInMaterialsRecieved materialDb = material.ToObject<MaterialInMaterialsRecieved>();
                    materialDb.MaterialsRecievedId = MaterialsRecievedId;
                    db.MaterialInMaterialsRecieveds.Add(materialDb);
                    i++;
            }
            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }

        public IQueryable<MaterialsRecieved> GetMaterialsRecieved() 
        {
            return db.MaterialsRecieveds;
        }

        public IQueryable<MaterialInMaterialsRecieved> GetMaterialsinMaterialsRecieved(int id) 
        {
            return db.MaterialInMaterialsRecieveds.Where(MR => MR.MaterialsRecievedId == id);
        }


        public IQueryable<MaterialSpecification> GetSpecificationsOfMaterial(int id)
        {
            return db.MaterialSpecifications.Where(s => s.MaterialId == id);
        }

        public IHttpActionResult AddMaterial(JObject jsonBody)
        {
            JObject specifications = (JObject)jsonBody["SpecificationsInMaterial"]; // this variable must be present in the javascript

            jsonBody.Remove("SpecificationsInMaterial");

            Material material = jsonBody.ToObject<Material>(); // the job card object

            db.Materials.Add(material);

            db.SaveChanges(); // save the shit

            int materialId = material.MaterialId; // the foregin key to be used for the -> material in job Cards

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)specifications.Children<JToken>();
            int i = 1;// to control the interation
            foreach (JToken token in tokens)
            {
                    JToken specificationJson = token.Children().First();
                    MaterialSpecification specificationInstance = specificationJson.ToObject<MaterialSpecification>();
                    specificationInstance.MaterialId= materialId;
                    db.MaterialSpecifications.Add(specificationInstance);
                    i++;
            }
            db.SaveChanges();
            return Ok(materialId);
        }

        // GET: api/Materials/5
        [ResponseType(typeof(Material))]
        public IHttpActionResult GetMaterial(int id)
        {
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return NotFound();
            }

            return Ok(material);
        }




        //// PUT: api/Materials/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutMaterial(int id, Material material)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != material.MaterialId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(material).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MaterialExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Materials
        //[ResponseType(typeof(Material))]
        //public IHttpActionResult PostMaterial(Material material)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Materials.Add(material);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = material.MaterialId }, material);
        //}

        //// DELETE: api/Materials/5
        //[ResponseType(typeof(Material))]
        //public IHttpActionResult DeleteMaterial(int id)
        //{
        //    Material material = db.Materials.Find(id);
        //    if (material == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Materials.Remove(material);
        //    db.SaveChanges();

        //    return Ok(material);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialExists(int id)
        {
            return db.Materials.Count(e => e.MaterialId == id) > 0;
        }
    }
}