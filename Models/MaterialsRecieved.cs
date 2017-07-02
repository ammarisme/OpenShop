using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class MaterialsRecieved
    {
        [Key]
        public int MaterialsRecievedId { get; set; }

        // foreign key to purchaseorders
        public int? PurchaseOrderId { get; set; }
        
        public DateTime? RecievedDate { get; set; }


        // navigational properties
        public ICollection<MaterialInMaterialsRecieved> MaterialsInMaterialsRecieved { get; set; }
    }
}
