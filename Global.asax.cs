using WholesaleTradingPortal.Models;
using WholesaleTradingPortal.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WholesaleTradingPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            ViewEngines.Engines.Clear();
            var razorEngine = new RazorViewEngine();
            
            razorEngine.PartialViewLocationFormats = razorEngine.PartialViewLocationFormats
                  .Concat(new[] { 
                  "~/Areas/Accounts/Views/Shared/{0}.cshtml",
                  "~/Areas/Default/Views/Shared/{0}.cshtml",
                  }).ToArray();
            

            ViewEngines.Engines.Add(razorEngine);
        }
    }
}