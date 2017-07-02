using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WholesaleTradingPortal.Models;
using WholesaleTradingPortal.Areas.Accounts.Models;
using WholesaleTradingPortal.DAL;
using RetaiEnterprise.Controllers.API;
using Newtonsoft.Json.Linq;
using System.Net;


namespace WholesaleTradingPortal.Areas.Accounts.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        private UserManager<ApplicationUser> UManager;
        private UserStore<ApplicationUser> UStore;

        private RoleManager<IdentityRole> RManager;
        private RoleStore<IdentityRole> RStore;

        private ApplicationDbContext UserDbContext;

        private ApplicationSignInManager SInManager;

        public string Authenticated { get; set; }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UManager = userManager;
            SInManager = signInManager;
            this.Authenticated = Session["Authorize"].ToString();
        }

        

        public AccountController()
        {
            

            UStore = new UserStore<ApplicationUser>(db);
            UManager = new UserManager<ApplicationUser>(UStore);

            RStore = new RoleStore<IdentityRole>(db);
            RManager = new RoleManager<IdentityRole>(RStore);
        }

        // GET controllers
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session["Authenticated"]!=null)
            {
                return RedirectToAction("MyAccount");
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "Accounts" });
            }
        }

        /*
         *@purpose - Show the Account information of the logged in user.
         *User can also edit the profile and save.
         */

        public ActionResult ManageMyAccount()
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);
            userId = user.Id;
            Account account = db.Accounts.Find(userId);
            ManageAccountViewModel model = new ManageAccountViewModel { Id = account.Id, Email = account.Email, FirstName = account.FirstName, LastName = account.LastName, PhoneNumber2 = account.PhoneNumber2, Designation = account.Designation, Address = account.Address, Status = account.Status };

            ViewBag.id = userId;
            return View(model);
        }

        /*
         * @purpose - A logged in user can change his own password
         */
        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.Id = User.Identity.GetUserId();
            return View();
        }
        /*
         * @purpose - SHow view to Create a new account with staff priviledges
         */
        [Authorize]
        public ActionResult AddAccount() {
            AddAccountViewModel model = new AddAccountViewModel();
            model.Id = db.Roles;
            return View(model);
        }
        /*
         * @purpose -  View all account information
         */
        public ActionResult AllAccounts() {
            return View();
        }

        // GET : a view to enter reg information
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /*
         *@purpose - User can view all other staff accounts and edit them 
         */
        public ActionResult ManageAccounts() {
            return View();
        }

        // show the login view
        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            if (Session["Authenticated"]=="Authenticated")
            {
                return RedirectToAction("MyAccount");
            }
            ViewBag.ReturnUrl = "";
            return View();
        }

        public ActionResult MyAccount() {
            if (Session["Authenticated"] == "Authenticated")
            {
                ManageAccountViewModel model = new ManageAccountViewModel();
                string email = Session["UserEmail"].ToString();

                Account account = db.Accounts.Where(a => a.Email2 == email).First();
                if (account != null)
                {
                model.Id = account.Id;
                model.LastName = account.LastName;
                model.FirstName = account.FirstName;
                model.Address = account.Address;
                model.Designation = account.Designation;
                model.PhoneNumber2 = account.PhoneNumber2;
                model.Status = account.Status;
                model.Email = account.Email;
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST FUNCTIONS

        // this method can be used by any user to register to the system. 
        // Enterprise and wholesaler user roles needs to be confirmed by an Administrator
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(AddAccountViewModel model)
        {
            ApplicationDbContext appDb = new ApplicationDbContext();

            if (ModelState.IsValid)
            {
                // create a Enterprise table entry and a user table entry
                var Enterprise = new Account { UserName = model.Email, Email = model.Email, Status = "registered", PhoneNumber2 = model.PhoneNumber2, Designation = model.Designation, Address = model.Address, FirstName = model.FirstName };
                var result = await UManager.CreateAsync(Enterprise, model.Password);
                if (result.Succeeded)
                {
                    var addedToRole = await UManager.AddToRoleAsync(Enterprise.Id, "Administrator");
                    if (addedToRole.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                }
                AddErrors(result);
            }
            return View(model);
        }

        //creates an account if doesn't exist
        private async Task<bool> CreateCustomerAccountIfNotExist(LoginViewModel model){
            if (db.Accounts.Where(a => a.Email == model.Email).Count() > 0)
            {
                return true;
            }
            else
            {
                var Enterprise = new Account { UserName = model.Email, Email = model.Email , Email2 = model.Email};
                var result = await UManager.CreateAsync(Enterprise, model.Password);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         This method authenticates the user using the IS as the remote authentication server
         */
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
        if (ModelState.IsValid)
            {
                Integrator integrator = new Integrator();

                JObject jsonBody = JObject.FromObject(model);

                HttpWebResponse response = integrator.sendJsonObject(jsonBody, "/api/Accounts/Authenticate");
                if (response == null)
                {
                    Session["Authenticated"]=null;
                    Session["UserEmail"] = null;
                    ModelState.AddModelError("", "Invalid Account.");
                    return View(model);
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    // user is authentic....
                    // redirect user to my account page...
                    // set the session storage authenticated flag
                    // check if we have this account, if not.. create it
                    bool accExist = await CreateCustomerAccountIfNotExist(model);
                    if (accExist)
                    {
                    Session["Authenticated"] = "Authenticated";
                    Session["UserEmail"] = model.Email;
                    return RedirectToAction("MyAccount");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error in creating Account");
                        Session["Authenticated"]=null;
                        Session["UserEmail"] = null;
                        return View(model);
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    // user name and password mismatch
                    // redirect the user to the login page..
                    // remove the session storage authenticated flag
                    Session["Authenticated"]=null;
                    Session["UserEmail"] = null;
                    ModelState.AddModelError("", "Invalid Account.");
                    return View(model);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    // there is a problem with the data model
                    // you need to remove the authenticated flag... but wait until above two cases
                    Session["Authenticated"]=null;
                    Session["UserEmail"] = null;
                    ModelState.AddModelError("", "Bad request");
                    return View(model);
                }
            }
            ModelState.AddModelError("", "Unknown response recieved from Integration System");
            return View(model);
        }



        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add more custom claims here if you want. Eg HomeTown can be a claim for the User
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        // Process edit request for profile
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Edit(ManageAccountViewModel model)
        {
            ApplicationDbContext appDb = new ApplicationDbContext();

            if (ModelState.IsValid)
            {
                Account Enterprise = db.Accounts.Find(User.Identity.GetUserId());

                if (Enterprise!= null)
                {
                  // set Enterprise object values
                    // create a new Enterprise object
                    db.Entry(Enterprise).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
            }
            return View("EditProfile",model);
        }


        public bool isAuthenticated()
        {
            if (Session["Authenticated"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void authenticateUser()
        {
            Session["Authenticate"] = "Authenticate";
        }

        public void deAuthenticate()
        {
            Session["Authenticate"] = null;
            Session["UserEmail"] = null;
        }
        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // POST: /Account/LogOff
        [AllowAnonymous]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["Authenticated"]=null;
            Session["UserEmail"] = null;
            return RedirectToAction("Login", "Account", new { area = "Accounts" });
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UManager != null)
                {
                    UManager.Dispose();
                    UManager = null;
                }

                if (SInManager != null)
                {
                    SInManager.Dispose();
                    SInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Login", "Account", new { area = "Accounts" });
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}