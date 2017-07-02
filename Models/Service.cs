using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WholesaleTradingPortal.Models
{
    [Table("Services")]
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        public string Type { get; set; }

        public string Uri { get; set; }

        public bool Status { get; set; }
    }

    
}