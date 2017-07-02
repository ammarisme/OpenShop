
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }

        public string MaterialName { get; set; }
        
        public float Cost { get; set; }

        // dependent tables
        public ICollection<MaterialInMaterialsRecieved> MaterialsInMaterialsRecieved { get; set; }

        public ICollection<Job> JobCards { get; set; }

        public ICollection<MaterialInPurchaseOrder> MaterialsInPurchaseOrder { get; set; }

        public ICollection<MaterialInProduct> MaterialsInProduct { get; set; }

        public ICollection<MaterialSpecification> MaterialSpecifications { get; set; }

        
    }
}
