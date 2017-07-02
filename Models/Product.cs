using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WholesaleTradingPortal.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Unit { get; set; }

        public float? Price { get; set; }

       public float? StocksQuantity { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Category { get; set; }

        [ForeignKey("Enterprises")]
        public int EnterpriseId { get; set; }

        //nav property
        public Enterprise Enterprises { get; set; }

        public ICollection<SpecificationInProduct> SpecificationInProduct { get; set; }

    }
    public class SpecificationInProduct
    {
        public int SpecificationInProductId { get; set; }

        // foreign key in product
        public int ProductId { get; set; }

        public string Specification { get; set; }

        public string Value { get; set; }
    }
}