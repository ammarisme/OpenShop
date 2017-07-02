using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETrading.Models
{
    public class SalaryPayment
    {
        [Key]
        public int SalaryPaymentId { get; set; }

        public float Amount { get; set; }

        public int EmployeeId { get; set; }

        public DateTime? PaidDate { get; set; }

        public string Remark { get; set; }

    }
}