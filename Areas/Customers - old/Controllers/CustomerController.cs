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
using Microsoft.Owin.Security;
using RetailShop.Models;
using RetailShop.Areas.Customers.Models;
using RetailShop.DAL;

namespace RetailShop.Areas.Customers.Controllers
{
    

    [Authorize]
    public class CustomerController : Controller
    {
        private ApplicationDbContext applicationDb= new ApplicationDbContext();


        private UserManager<ApplicationUser> UManager;
        private UserStore<ApplicationUser> UStore;

        private RoleManager<IdentityRole> RManager;
        private RoleStore<IdentityRole> RStore;

        private ApplicationDbContext UserDbContext;

        private ApplicationSignInManager SInManager;

        
        public CustomerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UManager = userManager;
            SInManager = signInManager;
        }

        public CustomerController()
        {
            UserDbContext = new ApplicationDbContext();

            UStore = new UserStore<ApplicationUser>(UserDbContext);
            UManager = new UserManager<ApplicationUser>(UStore);

            RStore = new RoleStore<IdentityRole>(UserDbContext);
            RManager = new RoleManager<IdentityRole>(RStore);

            //SInManager = new SignInManager<ApplicationUser>;
        }
        /*
         * View the Dahsboard if logged in.
         * View the login page if not
         */
        //[AllowAnonymous]
        //public ActionResult Index()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Dashboard");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}

        //// GET : a view to enter reg information
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    return View();
        //}
        //// this method can be used by any user to register to the system. 
        //// retailer and wholesaler user roles needs to be confirmed by an Administrator
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> Register(RegisterCustomerViewModel model)
        //{
        //    ApplicationDbContext appDb = new ApplicationDbContext();

        //    if (ModelState.IsValid)
        //    {
        //        // create a customer table entry and a user table entry
        //        var customer = new Customer { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, BillingAddress = model.BillingAddress, City = model.City, Status = "registered" };
        //        var result = await UManager.CreateAsync(customer, model.Password);
        //        if (result.Succeeded)
        //        {
        //            var addedToRole = await UManager.AddToRoleAsync(customer.Id, "Customer");
        //            if (addedToRole.Succeeded)
        //            {
        //                return RedirectToAction("Login");
        //            }
        //        }
        //        AddErrors(result);
        //    }
        //    return View(model);
        //}

        //// show the login view
        //[AllowAnonymous]
        //public ActionResult LogIn(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UManager.FindAsync(model.Email, model.Password);
        //        if (user != null)
        //        {
        //            var isCustomer = await UManager.IsInRoleAsync(user.Id, "Customer");
        //            if (isCustomer)
        //            {
        //            await SignInAsync(user, model.RememberMe);
        //            return RedirectToAction("Dashboard");
        //            }
        //            //return RedirectToLocal(returnUrl);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid Customer account.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    var identity = await UManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add more custom claims here if you want. Eg HomeTown can be a claim for the User
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        //}

        //[Authorize]
        //public ActionResult Dashboard() {
        //    return View();
        //}
        
        //// show a view where customer can add , update profile
        //[Authorize]
        //public ActionResult Manage()
        //{
        //    var customer = applicationDb.Customers.Find(User.Identity.GetUserId());
        //    ManageCustomerViewModel model = new ManageCustomerViewModel {Id=User.Identity.GetUserId(), Email = customer.Email, BillingAddress = customer.BillingAddress, City = customer.City, FirstName = customer.FirstName, LastName = customer.LastName , PhoneNumber = customer.PhoneNumber};

        //    return View(model);
        //}

        
        //// Process edit request for profile
        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult> Edit(ManageCustomerViewModel model)
        //{
        //    ApplicationDbContext appDb = new ApplicationDbContext();

        //    if (ModelState.IsValid)
        //    {   
        //        Customer customer = applicationDb.Customers.Find(User.Identity.GetUserId());
                
        //        if (customer != null)
        //        {
        //            customer.Email = model.Email;
        //            customer.FirstName = model.FirstName;
        //            customer.LastName = model.LastName;
        //            customer.PhoneNumber = model.PhoneNumber;
        //            customer.Status = "changed";
        //            customer.City = model.City;
        //            customer.BillingAddress = model.BillingAddress;

        //            // create a new customer object
        //            applicationDb.Entry(customer).State = System.Data.Entity.EntityState.Modified;
        //            applicationDb.SaveChanges();

        //        }
        //    }
        //    return RedirectToAction("Manage");
        //}

        //[AllowAnonymous]
        //public ActionResult RegisteredCustomers()
        //{
        //    RegisteredCustomersViewModel model = new RegisteredCustomersViewModel();
        //    model.RegisteredCustomers = applicationDb.Customers.Where(r => r.Status == "Registered");
        //    return View(model);
        //}
        ///// Whatever below is still not tested and integrated

        ////public ApplicationSignInManager SignInManager
        ////{
        ////    get
        ////    {
        ////        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        ////    }
        ////    private set
        ////    {
        ////        _signInManager = value;
        ////    }
        ////}

        ////public ApplicationUserManager UserManager
        ////{
        ////    get
        ////    {
        ////        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        ////    }
        ////    private set
        ////    {
        ////        _userManager = value;
        ////    }
        ////}
        ////-------------------------------------------------------------------------

        ////
        //// GET: /Account/VerifyCode
        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    if (!await SInManager.HasBeenVerifiedAsync())
        //    {
        //        return View("Error");
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ////
        //// POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes. 
        //    // If a user enters incorrect codes for a specified amount of time then the user account 
        //    // will be locked out for a specified amount of time. 
        //    // You can configure the account lockout settings in IdentityConfig
        //    var result = await SInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(model.ReturnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid code.");
        //            return View(model);
        //    }
        //}

        ////
        //// GET: /Account/ConfirmEmail
        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return View("Error");
        //    }
        //    var result = await UManager.ConfirmEmailAsync(userId, code);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        ////
        //// GET: /Account/ForgotPassword
        //[AllowAnonymous]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UManager.FindByNameAsync(model.Email);
        //        if (user == null || !(await UManager.IsEmailConfirmedAsync(user.Id)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //        // Send an email with this link
        //        // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //        // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
        //        // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //        // return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        ////
        //// GET: /Account/ForgotPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ForgotPasswordConfirmation()
        //{
        //    return View();
        //}

        ////
        //// GET: /Account/ResetPassword
        //[AllowAnonymous]
        //public ActionResult ResetPassword(string code)
        //{
        //    return code == null ? View("Error") : View();
        //}

        ////
        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

        ////
        //// GET: /Account/ResetPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        ////
        //// GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //    var userId = await SInManager.GetVerifiedUserIdAsync();
        //    if (userId == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await UManager.GetValidTwoFactorProvidersAsync(userId);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ////
        //// POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    // Generate the token and send it
        //    if (!await SInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //    {
        //        return View("Error");
        //    }
        //    return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        ////
        //// POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return RedirectToAction("Index", "Home");
        //}

        ////
        //// GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (UManager != null)
        //        {
        //            UManager.Dispose();
        //            UManager = null;
        //        }

        //        if (SInManager != null)
        //        {
        //            SInManager.Dispose();
        //            SInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

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
            return RedirectToAction("Dashboard", "Home");
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