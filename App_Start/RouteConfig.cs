using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WholesaleTradingPortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { area = "Accounts" , controller = "Account", action = "Login", id = UrlParameter.Optional }
            //);

            routes.MapRoute("redirect all other requests", "{*url}",
            new
            {
                controller = "Home",
                action = "Index"
            }).DataTokens = new RouteValueDictionary(new { area = "Default" });

            //routes.MapRoute(
            //name: "Default",
            //url: "{controller}/{action}/{id}",
            //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //namespaces: new[] { "WholesaleTradingPortal.Areas.Default.Controllers" }
            //);
        }
    }
}
