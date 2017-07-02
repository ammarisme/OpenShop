using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetailTradingPortal.Models;
namespace RetailTradingPortal.Areas.Sales.Models
{
    public class AddNewSaleViewModel
    {
        public IEnumerable<Product> ProductId { get; set; }

        public IEnumerable<Customer> CustomerId { get; set; }
    }
    public class ProcessSalesViewModel 
    {
        
        public IEnumerable<RetailSale> RetailSales { get; set; }
        public IEnumerable<WholesaleSale> WholesaleSales { get; set; }
    }
    
    public class CreateRetailOrder
    {
        public Account Enterprise { get; set; }

        public Customer Customer { get; set; }

        public IEnumerable<Product> ProductId { get; set; }
    }
    public class ViewAllMyRecievedOrders
    {
        public IEnumerable<Order> Orders { get; set; }
    }

    public class OrderView
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
    }
    public class PlacedOrdersView
    {
        public IEnumerable<RetailSale> Sales { get; set; }

        public Customer Customer{ get; set; }
    }
}