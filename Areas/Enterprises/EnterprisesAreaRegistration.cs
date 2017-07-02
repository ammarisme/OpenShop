using System.Web.Mvc;

namespace RetailTradingPortal.Areas.Enterprises
{
    public class EnterprisesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Enterprises";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Enterprises_default",
                "Enterprises/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}