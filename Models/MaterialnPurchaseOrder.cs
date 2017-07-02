using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class MaterialInPurchaseOrder
    {
        [Key]
        public int MaterialInPurchaseOrderId { get; set; }
        
        // foreign key purchase order
        public int PurchaseOrderId { get; set; }

        // foreign key in materials
        public int MaterialId { get; set; }

        public float Quantity { get; set; }

        public float UnitCost { get; set; }

        public string Description { get; set; }



    }
}
