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

namespace RetailTradingPortal.Controllers.API
{
    public class ServicesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Services
        public IQueryable<Service> GetServices()
        {
            return db.Services;
        }

        public IHttpActionResult GetEnterprisesOfService(int id)
        {
            IEnumerable<EnterpriseService> enterpriseServices = db.EnterpriseServices.Where(s => s.ServiceId==id);
            var result =
            (
            from es in enterpriseServices
            join enterprises in db.Enterprises on es.EnterpriseId equals enterprises.EnterpriseId
            where es.EnterpriseId == enterprises.EnterpriseId
            select new
            {
                EnterpriseId = enterprises.EnterpriseId,
                EnterpriseTypeId = enterprises.EnterpriseTypeId,
                EnterpriseName = enterprises.EnterpriseName,
                EntepriseAddress = enterprises.EntepriseAddress,
                Rating = enterprises.Rating,
                BusinessPhoneNumber = enterprises.Rating,
                Status = enterprises.Status,
                BRCNumber = enterprises.BRCNumber,
                Category = enterprises.Category,
                Currency = enterprises.Currency,
                Country = enterprises.Country,
                Region = enterprises.Region,
                Uri = enterprises.Uri
            }
            );
            return Ok(result);
        }

        // return services that an account is connected to
        public IHttpActionResult GetAccountServices(string id)
        {
            IEnumerable<AccountService> e_services = db.AccountServices.Where(acc_serv => acc_serv.Id==id);

            var result =
            (
            from acc_service in e_services
            join services in db.Services on acc_service.ServiceId equals services.ServiceId
            where acc_service.ServiceId == services.ServiceId
            select new { 
                ServiceId= services.ServiceId,
                Type= services.Type,
                Uri= services.Uri,
                Status= services.Status
            }
            );

            return Ok(result);
        }

        //return accounts that are connected to a service
        public IHttpActionResult GetServiceAccounts(int id)
        {
            IEnumerable<AccountService> e_services = db.AccountServices.Where(acc_serv => acc_serv.ServiceId == id);

            var result =
            (
            from acc_service in e_services
            join accounts in db.Accounts on acc_service.Id equals accounts.Id
            where acc_service.Id == accounts.Id
            select new
            {
                Id = accounts.Id,
                Name = accounts.FirstName +" "+accounts.LastName,
                Address = accounts.Address,
                PhoneNumber = accounts.PhoneNumber2,
                Status = accounts.Status,
                Designation = accounts.Designation
            }
            );

            return Ok(result);
        }
        // GET: api/Services/5
        [ResponseType(typeof(Service))]
        public IHttpActionResult GetService(int id)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutService(int id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.ServiceId)
            {
                return BadRequest();
            }

            db.Entry(service).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        [ResponseType(typeof(Service))]
        public HttpResponseMessage AddService(Service service)
        {
            
            db.Services.Add(service);
            db.SaveChanges();

            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }

        public HttpResponseMessage UpdateService(Service service)
        {

            Service original = db.Services.Find(service.ServiceId);
            original.Type = service.Type;
            original.Uri = service.Uri;

            db.Entry(original).State = EntityState.Modified;

            db.SaveChanges();

            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(Service))]
        public IHttpActionResult DeleteService(int id)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            db.Services.Remove(service);
            db.SaveChanges();

            return Ok(service);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceExists(int id)
        {
            return db.Services.Count(e => e.ServiceId == id) > 0;
        }
    }
}