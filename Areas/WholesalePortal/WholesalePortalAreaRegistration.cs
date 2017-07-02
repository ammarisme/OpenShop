using System.Web.Mvc;

namespace RetailShop.Areas.WholesalePortal
{
    public class WholesalePortalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WholesalePortal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WholesalePortal_default",
                "WholesalePortal/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}