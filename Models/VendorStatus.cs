using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETrading.Models
{
    public class VendorStatus
    {
        [Key]
        public int VendorStatusId { get; set; }

        public string StatusName { get; set; }
    }
}