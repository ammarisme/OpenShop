using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETrading.Models
{
    public class PurchaseOrderStatus
    {
        [Key]
        public int PurchaseOrderStatusId { get; set; }

        public string StatusName { get; set; }

        // navigational properties
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}