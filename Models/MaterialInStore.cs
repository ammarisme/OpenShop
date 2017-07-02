using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class MaterialInStore
    {
        [Key]
        public int Id { get; set; }
        
       // [ForeignKey("Material")]
        public int MaterialId { get; set; }

       // [ForeignKey("Store")]
        public int StoreId { get; set; }
    }
}
