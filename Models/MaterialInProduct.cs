using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class MaterialInProduct
    {
    [Key]
    public int MaterialInProductId { get; set; }

        // foreign key to products
    public int ProductId { get; set; }
        
        // foreign key to materials
    public int MaterialId { get; set; }

    public float Quantity { get; set; }

    }

}