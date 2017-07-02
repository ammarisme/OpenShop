using System.Web.Mvc;

namespace RetailTradingPortal.Areas.PurchaseOrders
{
    public class PurchaseOrdersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PurchaseOrders";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PurchaseOrders_default",
                "PurchaseOrders/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}