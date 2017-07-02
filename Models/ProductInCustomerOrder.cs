using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class ProductInCustomerOrder
    {
        [Key]
        public int ProductsInCustomerOrderId { get; set; }

        // foreign key to products table
        public int ProductId { get; set; }
 
        // foreign key to CustomerOrder
        public int CustomerOrderId { get; set; }

        public float Quantity { get; set; }

        public float UnitPrice { get; set; }

        public string Description { get; set; }
    }
}
