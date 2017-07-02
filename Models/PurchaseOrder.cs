using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ETrading;

namespace ETrading.Models
{
    public enum Status{
    Pending, Active, Closed
    };
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }

        // foreign key in Vendors
        public int VendorId { get; set; }

        public DateTime? CreatedTime { get; set; }
        
        public string Status { get; set; }

        // navigational properties
        public ICollection<MaterialInPurchaseOrder> MaterialsInPurchaseOrder { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Customer> Customers{ get; set; }
    }

}
