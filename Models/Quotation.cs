using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WholesaleTradingPortal;

namespace WholesaleTradingPortal.Models
{
    /// <summary>
    /// Storing Quotation information
    /// TODO : Validate the Buying enterprise Id
    /// </summary>
    [Table("Quotations")]
    public class Quotation
    {
        [Key]
        public int QuotationId { get; set; }

        [ForeignKey("Enterprises")] // The enterprise to whom this Q-Request is being placed
        public int EnterpriseId { get; set; }
        
        public string Status { get; set; }

        public string PaymentMethod { get; set; }

        public int PaymentDuration { get; set; }

        public string DeliveryMethod { get; set; }

        public string BuyingEnterpriseId { get; set; }
        // navigational properties
        public virtual Enterprise Enterprises { get; set; }
        
        public ICollection<ProductInQuotation> ProductsInQuotation { get; set; }

    }

    /// <summary>
    /// All the product in Quotations
    /// </summary>
    [Table("ProductsInQuotations")]
    public class ProductInQuotation
    {
        [Key]
        public int ProductInQuotationId { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }

        [ForeignKey("Quotations")]
        public int QuotationId { get; set; }

        public int Quantity { get; set; }

        public float UnitPrice { get; set; }

        public virtual Product Products { get; set; }

        public virtual Quotation Quotations { get; set; }
    }
}
