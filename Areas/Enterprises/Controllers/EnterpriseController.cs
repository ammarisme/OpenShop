using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailTradingPortal.DAL;
using RetailTradingPortal.Models;
using RetailTradingPortal.Areas.Enterprises.Models;

namespace RetailTradingPortal.Areas.Enterprises.Controllers
{

    public class EnterpriseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AddEnterprise()
        {
            AddEnterpriseViewModel model = new AddEnterpriseViewModel();
            model.EnterpriseTypeId = db.EnterpriseTypes;
            return View(model);
        }

        public ActionResult EditEnterprises()
        {
            return View();
        }

        public ActionResult ViewAllEnterprises()
        {
            return View();
        }

        public ActionResult ManageEnterpriseServices()
        {
            ManageServicesOfEnterprisesViewModel model = new ManageServicesOfEnterprisesViewModel();
            model.ServiceId = db.Services;
            return View(model);
        }
    }
}