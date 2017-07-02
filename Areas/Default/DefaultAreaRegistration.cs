using System.Web.Mvc;

namespace WholesaleTradingPortal.Areas.Default
{
    public class DefaultAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Default";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Default_default",
                "Default/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}