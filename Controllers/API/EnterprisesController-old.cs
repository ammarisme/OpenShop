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
using RetailTradingPortal.DAL;
using RetailTradingPortal.Models;
using RetailTradingPortal.Areas.Enterprises.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace RetailTradingPortal.Controllers.API
{
    public class EnterprisesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Enterprises



        public IHttpActionResult GetServicesOfEnterprise(int id)
        {
            var result = (
                from es in db.EnterpriseServices.Where(s => s.EnterpriseId == id)
                join services in db.Services on es.ServiceId equals services.ServiceId
                where es.ServiceId == services.ServiceId
                select new
                {
                    ServiceId = services.ServiceId,
                    Type = services.Type,
                    Status = services.Status
                }
                );

            return Ok(result) ;
        }
        public IQueryable<Enterprise> GetEnterprises()
        {
            return db.Enterprises;
        }

        public IHttpActionResult GetEnterpriseAccounts(int id)
        {
            IEnumerable<EnterpriseAccount> ea = db.EnterpriseAccounts.Where(es => es.EnterpriseId==id);
            var result = (
                from e_accounts in ea
                join accounts in db.Accounts on e_accounts.Id equals accounts.Id
                where e_accounts.Id ==  accounts.Id
                select new
                {
                    Id = accounts.Id,
                    Name = accounts.FirstName+ " " +accounts.LastName ,
                    Address = accounts.Address,
                    PhoneNumber = accounts.PhoneNumber2 ,
                    Status = accounts.Status,
                    Designation = accounts.Designation
                }
                );

            return Ok(result);
            
        }
        
        public HttpResponseMessage RemoveEnterpriseService(EnterpriseService enterpriseService)
        {
            if (!EnterpriseExistsById(enterpriseService.EnterpriseId) || (db.Services.Where(s=> enterpriseService.ServiceId == s.ServiceId).Count() < 0)
                || (db.EnterpriseServices.Where(es => es.EnterpriseId==enterpriseService.EnterpriseId && es.ServiceId==enterpriseService.ServiceId).Count() < 0))
            {
                return this.Request.CreateResponse(HttpStatusCode.Conflict, "");
            }
            else
            {
                EnterpriseService toBeDeleted = db.EnterpriseServices.Where(s => s.ServiceId==enterpriseService.ServiceId && s.EnterpriseId==enterpriseService.EnterpriseId).Single<EnterpriseService>();
                db.Entry(toBeDeleted).State = EntityState.Deleted;
                db.SaveChanges();
                return this.Request.CreateResponse(HttpStatusCode.OK, "");
            }
        }
        public HttpResponseMessage AddEnterpriseService(EnterpriseService enterpriseService)
        { 
            if(!EnterpriseExistsById(enterpriseService.EnterpriseId) || (db.Services.Count(s => s.ServiceId == enterpriseService.ServiceId) < 0)
                || (db.EnterpriseServices.Where(es => es.EnterpriseId==enterpriseService.EnterpriseId && es.ServiceId==enterpriseService.ServiceId).Count() > 0)
                ){
                    return this.Request.CreateResponse(HttpStatusCode.Conflict, "");
            }
            else{

                db.EnterpriseServices.Add(enterpriseService);
                db.SaveChanges();
                return this.Request.CreateResponse(HttpStatusCode.OK, "");
            }
        }

        // GET: api/Enterprises/5
        [ResponseType(typeof(Enterprise))]
        public IHttpActionResult GetEnterprise(int id)
        {
            Enterprise enterprise = db.Enterprises.Find(id);
            if (enterprise == null)
            {
                return NotFound();
            }

            return Ok(enterprise);
        }

        // PUT: api/Enterprises/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnterprise(int id, Enterprise enterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enterprise.EnterpriseId)
            {
                return BadRequest();
            }

            db.Entry(enterprise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnterpriseExists(enterprise.EnterpriseName))
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
        [Authorize]
        public IHttpActionResult AddEnterprise(Enterprise enterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Enterprises.Add(enterprise);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EnterpriseExists(enterprise.EnterpriseName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = enterprise.EnterpriseId }, enterprise);
        }

        // DELETE: api/Enterprises/5
        [ResponseType(typeof(Enterprise))]
        public IHttpActionResult DeleteEnterprise(int id)
        {
            Enterprise enterprise = db.Enterprises.Find(id);
            if (enterprise == null)
            {
                return NotFound();
            }

            db.Enterprises.Remove(enterprise);
            db.SaveChanges();

            return Ok(enterprise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnterpriseExists(string name)
        {
            return db.Enterprises.Count(e => e.EnterpriseName == name) > 0;
        }

        private bool EnterpriseExistsById(int id)
        {
            return db.Enterprises.Count(e => e.EnterpriseId == id) > 0;
        }
    }
}