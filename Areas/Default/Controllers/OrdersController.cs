using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleTradingPortal.Areas.Default.Models;
using WholesaleTradingPortal.DAL;
using WholesaleTradingPortal.Models;


namespace WholesaleTradingPortal.Areas.Default.Controllers
{
    public class OrdersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult PlaceOrder(int id)
        {
            if (Session["Authenticated"] != null)
            {
            // following information are required when gettin the order back from the client
            RequestQuotationViewModel model = new RequestQuotationViewModel();
            model.Enterprise = db.Enterprises.Find(id);
            model.PaymentMethod = new List<PaymentMethod> { new PaymentMethod { PaymentMethodName = "Cash" }, new PaymentMethod { PaymentMethodName = "Cheque" } };
            model.DeliveryMode = new List<DeliveryMethod> { new DeliveryMethod { DeliveryModeName = "Store Pickup" }, new DeliveryMethod { DeliveryModeName = "Courier" } };

            // filling out the account information
            string userEmail = Session["UserEmail"].ToString();
            Account account = db.Accounts.Where(a => a.Email2==userEmail).First();
            model.Account = account;
            return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "Accounts"});
            }
        }

        public ActionResult All() {

            return View();
        }

        public ActionResult Order(int id){
            return View();
        }

        public ActionResult Index() {

            return View();
        }
    }
}
