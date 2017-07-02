using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailEnterprise.Controllers
{
    public class HomeOldController : Controller
    {
        /*A user comes he has not logged in yet
         *  update only the main content
         * the user have already logged in,
         *  show him an updated main-header / main-sidebar / control-sidebar
         */
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "MyAccount", new { area = "Accounts" });
            }
            else
            {
                return RedirectToAction("Login", "MyAccount", new { area = "Accounts" });
            }
        }
    }
}