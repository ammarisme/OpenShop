using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetailTradingPortal.Models;

namespace RetailTradingPortal.Areas.Customers.Models
{
    public class AddCustomerViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string BillingAddress { get; set; }

        public string Remark { get; set; }


    }

    public class AllCustomersViewModel
    {
        public IEnumerable<Customer> Customers;
    }
}