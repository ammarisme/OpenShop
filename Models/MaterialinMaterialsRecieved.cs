using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class MaterialInMaterialsRecieved
    {
        [Key]
        public int MaterialInMaterialsRecievedId { get; set; }
        
       // foreign key to Materials Table
        public int MaterialId { get; set; }

        // foreign key to Goods Recieved Table
        public int MaterialsRecievedId { get; set; }
        
        public int QuantityRecieved { get; set; }

        public int QuantityDispatched { get; set; }

        public int Cost { get; set; }

        public string Remarks { get; set; }

        // navigational properies
        public Material Material { get; set; }

        public MaterialsRecieved MaterialsRecieved { get; set; }



    }
}
