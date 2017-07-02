using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WholesaleTradingPortal.Models
{
    [Table("Accounts")]
    public class Account : ApplicationUser
    {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Address { get; set; }

            public string PhoneNumber2 { get; set; }

            public string Status { get; set; }

            public string Designation { get; set; }

            public string Email2 { get; set; }

   }
}


        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string EnterpriseAddress { get; set; }

        //public float  Rating { get; set; }

        //public string BusinessPhoneNumber { get; set; }

        //public string Status { get; set; }

        //public string BRCNumber { get; set; }

        //public string Category { get; set; }

        //public string Currency { get; set; }

        //public ICollection<Quotation> Quotations { get; set; }