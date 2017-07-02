using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailTradingPortal.Areas.Services.Models;
using RetailTradingPortal.DAL;
using RetailTradingPortal.Models;

namespace RetailTradingPortal.Areas.Services.Controllers
{
    
    public class ServiceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        

        public ActionResult ManageServices()
        {
            return View();
        }

        public ActionResult ViewAllServices()
        {
            return View();
        }

        public ActionResult ManageServiceUsers()
        {
            return View();
        }

        public ActionResult ManageServiceAccounts()
        {
            return View();
        }

        public ActionResult AddRemoveAccountServices()
        {
            AddServiceToAccountViewModel model = new AddServiceToAccountViewModel();
            model.ServiceId = db.Services;

            return View(model);
        }

    }
}