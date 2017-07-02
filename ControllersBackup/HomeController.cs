using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETrading.Controllers
{
    public class HomeController : Controller
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
                return View("Index_authenticated");
            }
            else
            {
                return View("Index_unauthenticated");
            }
        }

	        public ActionResult Dashboard()
        {
            ViewBag.Title = "Sleepytime";
            ViewBag.UserName = User.Identity.Name;

            return View();
        }
		
     
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}