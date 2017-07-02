using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETrading.Models
{
    public class MaterialUnit
    {
        [Key]
        public int MaterialUnitId { get; set; }

        public string UnitName { get; set; }

        public List<MaterialInPurchaseOrder> MaterialsInPurchaseOrders { get; set; }

    }
}