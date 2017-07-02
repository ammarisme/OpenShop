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
    public class JobCardStatus {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class JobCardsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        [HttpPost]
        public IHttpActionResult AddJobCard(JObject jsonBody)
        {
            JObject materials = (JObject)jsonBody["MaterialsInJobCard"]; // this variable must be present in the javascript
            JObject products = (JObject)jsonBody["ProductsInJobCard"]; // this variable must be present in the javascript

            jsonBody.Remove("MaterialsInJobCard");
            jsonBody.Remove("ProductsInJobCard");

            JobCard jobCard = jsonBody.ToObject<JobCard>(); // the job card object

            db.JobCards.Add(jobCard);

            db.SaveChanges(); // save the shit

            int jobCardId = jobCard.JobCardId; // the foregin key to be used for the -> material in job Cards

            // Adding materials in job card to table
            JEnumerable<JToken> materialsJson = (JEnumerable<JToken>)materials.Children<JToken>();

            foreach (JToken token in materialsJson)
            {
                    JToken materialJson = token.Children().First();
                    MaterialInJobCard materialInstance = materialJson.ToObject<MaterialInJobCard>();
                    materialInstance.JobCardId = jobCardId;
                    db.MaterialInJobCards.Add(materialInstance);
            }

            // Adding products in job card to table
            JEnumerable<JToken> productsJson = (JEnumerable<JToken>)products.Children<JToken>();

            foreach (JToken token in productsJson)
            {
                JToken productJson = token.Children().First();
                ProductInJobCard productInstance = productJson.ToObject<ProductInJobCard>();
                productInstance.JobCardId = jobCardId;
                db.ProductInJobCard.Add(productInstance);
            }

            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }


        public IHttpActionResult AddDamagedJobCards(JObject jsonBody)
        {
            
            int jobCardId = int.Parse(((JValue)jsonBody["JobCardId"]).ToString()); // this variable must be present in the javascript
            jsonBody.Remove("JobCardId");

            //JobCard damagedMaterials = jsonBody.ToObject<JobCard>(); // the job card object

            JObject damagedMaterials = (JObject)jsonBody["DamagedMaterialInJobCard"];
            
            JEnumerable<JToken> tokens = (JEnumerable<JToken>) damagedMaterials.Children<JToken>();

            foreach (JToken token in tokens)
            {
                JToken materialJson = token.Children().First();
                MaterialInJobCard material = materialJson.ToObject<MaterialInJobCard>();
                material.JobCardId = jobCardId;
                db.MaterialInJobCards.Add(material);
            }

            db.SaveChanges();
            return Ok(jsonBody);
        }


        public IHttpActionResult ChangeJobCardStatus(JobCard jobCard)
        {
            JobCard job = db.JobCards.Find(jobCard.JobCardId);
            job.CompletedDate = jobCard.CompletedDate;
            job.Status = jobCard.Status;
            job.Remark = jobCard.Remark;
            db.Entry(job).State= EntityState.Modified; 

            //db.Entry(jobCard).Property(j => j.CompletedDate).IsModified = true;
            //db.Entry(jobCard).Property(j => j.Status).IsModified = true;
            //db.Entry(jobCard).Property(j => j.Remark).IsModified = true;
           
            db.SaveChanges();
            return Ok(jobCard);
        }
        // GET: api/JobCards
        public IQueryable<JobCard> GetJobCards()
        {
            return db.JobCards;
        }

        public IQueryable<MaterialInJobCard> GetMaterialsInJobCard(int id)
        {
            IQueryable<MaterialInJobCard> materialsInJobCard = db.MaterialInJobCards.Where(m => m.JobCardId ==id);
            return materialsInJobCard;
        }

        public IQueryable<ProductInJobCard> GetProductsInJobCard(int id)
        {
            IQueryable<ProductInJobCard> productsInJobCard = db.ProductInJobCard.Where(m => m.JobCardId == id);
            return productsInJobCard;
        }
        // GET: api/JobCards/5
        [ResponseType(typeof(JobCard))]
        public IHttpActionResult GetJobCard(int id)
        {
            JobCard jobCard = db.JobCards.Find(id);
            if (jobCard == null)
            {
                return NotFound();
            }

            return Ok(jobCard);
        }

        
        // PUT: api/JobCards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobCard(int id, JobCard jobCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobCard.JobCardId)
            {
                return BadRequest();
            }

            db.Entry(jobCard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobCardExists(id))
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

        // POST: api/JobCards
        [ResponseType(typeof(JobCard))]
        public IHttpActionResult PostJobCard(JobCard jobCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobCards.Add(jobCard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobCard.JobCardId }, jobCard);
        }

        // DELETE: api/JobCards/5
        [ResponseType(typeof(JobCard))]
        public IHttpActionResult DeleteJobCard(int id)
        {
            JobCard jobCard = db.JobCards.Find(id);
            if (jobCard == null)
            {
                return NotFound();
            }

            db.JobCards.Remove(jobCard);
            db.SaveChanges();

            return Ok(jobCard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobCardExists(int id)
        {
            return db.JobCards.Count(e => e.JobCardId == id) > 0;
        }
    }
}