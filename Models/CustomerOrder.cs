using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class CustomerOrder
    {
        [Key]
        public int CustomerOrderId { get; set; }

        //foreign key to Customers
        public string CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? OrderDueDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        public string Description { get; set; }

        // child keys
        public List<ProductInCustomerOrder> ProductsInCustomerOrder { get; set; }

        public List<Job> JobCards { get; set; }
       

    }
}
