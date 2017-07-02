using RetaiEnterprise.Controllers.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WholesaleTradingPortal.Areas.Default.Models;
using WholesaleTradingPortal.DAL;
using WholesaleTradingPortal.Models;


namespace WholesaleTradingPortal.Areas.Default.Controllers
{
    public class QuotationsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This page facilitates the user to enter information about the quotation
        /// Except product information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RequestQuotation(int id)
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


            // get all the enterprises this account has access to
            try
            {
                Integrator integrator = new Integrator();
                string response = integrator.getResponseMessage("/api/Enterprises/GetEnterpriseAccounts/"+account.Id);
                // please see the method doc
                List<Enterprise> myEnterprises = parseStringListToEnterprise(response);
                model.MyEnterprises = myEnterprises;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }

            return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "Accounts"});
            }
        }
        /// <summary>
        /// parse a string to a List<string>
        //  then parse each string in the List<string> and generate a List<Enterprise>
        // method is used to parse strings recieved from the IS
        // need to write similar methods to all types of receptions.. :/
        /// </summary>
        /// <returns></returns>
        private List<Enterprise> parseStringListToEnterprise(string enterpriseString)
        {
            return null;
        }

        /// <summary>
        /// Get a specific quotation request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult QuotaionRequest(int id){
            return View();
        }

        /// <summary>
        /// This page shows all the Quotation requests that were placed and their statuses.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {

            return View();
        }
    }
}
