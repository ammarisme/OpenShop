using System.Web.Mvc;

namespace RetailShop.Areas.Administrators
{
    public class AdministratorsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administrators";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administrators_default",
                "Administrators/{controller}/{action}/{id}",
                defaults: new { controller = "Administrator", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}