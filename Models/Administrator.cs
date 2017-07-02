using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RetailShop.Models
{
    [Table("Administrators")]
    public class Administrator
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public string Designation { get; set; }
    }
}