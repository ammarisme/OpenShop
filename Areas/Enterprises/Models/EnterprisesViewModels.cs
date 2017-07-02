using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetailTradingPortal.Models;

namespace RetailTradingPortal.Areas.Enterprises.Models
{
    public class AddEnterpriseViewModel
    {
        public int EnterpriseId { get; set; }

        public IEnumerable<EnterpriseType> EnterpriseTypeId { get; set; }

        public string EnterpriseName { get; set; }

        public string EntepriseAddress { get; set; }

        public string BusinessPhoneNumber { get; set; }

        public string Status { get; set; }

        public string BRCNumber { get; set; }

        public string Category { get; set; }

        public string Currency { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

    }

    public class EditEnterprisesViewModel : AddEnterpriseViewModel
    {
        public string EnterpriseType { get; set; }

        public string Rating { get; set; }
    }

    public class ManageServicesOfEnterprisesViewModel
    {
        public IEnumerable<Service> ServiceId { get; set; }
    }
}