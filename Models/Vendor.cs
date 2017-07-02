using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }
    
        public string Name { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public string ContactPerson { get; set; }

        public string Remark { get; set; }

        public string Status { get; set; }
        // dependant key
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}