using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetailShop.Models;

namespace RetailShop.Areas.WholesalePortal.Models
{
    public class ShopViewModel
    {
        public Wholesaler Wholesaler { get; set; }
        public IEnumerable<Product> ProductId { get; set; }
    }

    public class HomeViewModel
    {
        public IEnumerable<Wholesaler> Wholesalers { get; set; }
    }

    public class RequestQuotationViewModel 
    {
        public Wholesaler Wholesaler { get; set; }

        public Retailer Retailer { get; set; }
    }
}