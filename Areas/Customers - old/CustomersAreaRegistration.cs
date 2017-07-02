using System.Web.Mvc;

namespace RetailShop.Areas.Customers
{
    public class CustomersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Customers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Customers_default",
                "Customers/{controller}/{action}/{id}",
                defaults: new { controller = "Customer", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}