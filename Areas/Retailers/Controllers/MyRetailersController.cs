using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailTradingPortal.Areas.Enterprises.Models;
using RetailTradingPortal.DAL;

namespace RetailTradingPortal.Areas.Enterprises.Controllers
{
    public class MyEnterprisesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AllEnterprises()
        {
            AllEnterprisesViewModels model = new AllEnterprisesViewModels();
            model.Enterprises = db.Enterprises;

            return View();
        }
    }
}