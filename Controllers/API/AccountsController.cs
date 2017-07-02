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
using WholesaleTradingPortal.DAL;
using WholesaleTradingPortal.Models;
using System.Threading.Tasks;
using WholesaleTradingPortal.Areas.Accounts.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WholesaleTradingPortal.Controllers.API
{
    public class AccountsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserManager<ApplicationUser> UManager;
        private UserStore<ApplicationUser> UStore;

        private RoleManager<IdentityRole> RManager;
        private RoleStore<IdentityRole> RStore;

        public AccountsController()
        {

            UStore = new UserStore<ApplicationUser>(db);
            UManager = new UserManager<ApplicationUser>(UStore);

            RStore = new RoleStore<IdentityRole>(db);
            RManager = new RoleManager<IdentityRole>(RStore);
        }

        // GET: api/Accounts
        public IHttpActionResult GetAccounts()
        {
            var result = (
                from acc in db.Accounts
                select new { 
                Id = acc.Id,
                Address = acc.Address,
                Name = acc.FirstName + " " + acc.LastName,
                Status = acc.Status,
                PhoneNumber = acc.PhoneNumber2,
                Designation = acc.Designation
                }
                );
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> ChangePassword(UserCredentialsModel usermodel)
        {
            
            ApplicationUser user = await UManager.FindByIdAsync(usermodel.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.PasswordHash = UManager.PasswordHasher.HashPassword(usermodel.Password);
            var result = await UManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                //throw exception......
            }
            return Ok();
        }

        // GET: api/Accounts/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult GetAccount(string id)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        //[Authorize]
        public IHttpActionResult EditAccount(Account account)
        {
            if (ModelState.IsValid)
            {
                
                Account original = db.Accounts.Find(account.Id);
                original.FirstName = account.FirstName;
                original.LastName = account.LastName;
                original.Address = account.Address;
                original.PhoneNumber2 = account.PhoneNumber2;
                original.Designation = account.Designation;

                db.Entry(original).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                    return StatusCode(HttpStatusCode.Conflict);
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }


        // POST api/Account/Register
        [Authorize]
        public async Task<HttpResponseMessage> Register(AccountRegistrationModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,"Invalid data sent");
            //}


            IdentityResult result=null;
            using (var context = db)
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new Account { UserName = model.Email, Email = model.Email, Status = "registered", PhoneNumber2 = model.PhoneNumber2, Designation = model.Designation, Address = model.Address, FirstName = model.FirstName };

                if (userManager.FindByEmail(user.Email)==null)
                {
                    // Create the user and add it to the Staff role
                result = await userManager.CreateAsync(user, model.Password);

                IdentityRole UserRole = db.Roles.Find(model.Id);
                 
                await userManager.AddToRoleAsync(user.Id, UserRole.Name);

                }

            }

            if (result==null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An account with the same email already exist.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Account created successfully");
        }

        // DELETE: api/Accounts/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult DeleteAccount(string id)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(account);
            db.SaveChanges();

            return Ok(account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountExists(string id)
        {
            return db.Accounts.Count(e => e.Id == id) > 0;
        }

    }
}