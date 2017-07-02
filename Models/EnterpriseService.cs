using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WholesaleTradingPortal.Models
{
    [Table("EnterpriseServices")]
    public class EnterpriseService
    {
        [Key]
        public int EnterpriseServiceId { get; set; }

        public int ServiceId { get; set; }

        [ForeignKey("Enterprises")]
        public int EnterpriseId { get; set; }

        public Enterprise Enterprises { get; set; }
    }

    [Table("EnterpriseAccounts")]
    public class EnterpriseAccount
    {
        [Key]
        public int EnterpriseAccountId { get; set; }

        [ForeignKey("Accounts")]
        public string Id { get; set; }

        [ForeignKey("Enterprises")]
        public int EnterpriseId { get; set; }

        //navigational properties
        public Enterprise Enterprises { get; set; }

        public Account Accounts { get; set; }
    }

    [Table("AccountsServices")]
    public class AccountService
    {
        [Key]
        public int AccountServiceId { get; set; }

        [ForeignKey("Accounts")]
        public string Id { get; set; }

        [ForeignKey("Services")]
        public int ServiceId { get; set; }

        //navigational properties
        public Service Services { get; set; }

        public Account Accounts { get; set; }
    }
}
