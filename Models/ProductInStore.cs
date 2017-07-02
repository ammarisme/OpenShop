using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class ProductInStore
    {
        [Key]
        public int Id { get; set; }

        // foreign key in products
        public int ProductId { get; set; }

        // foreign key in stores
        public int StoreId { get; set; }
    }
}
