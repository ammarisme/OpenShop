using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WholesaleTradingPortal.Models
{
    public class ProductStocks
    {
        [Key]
        public int ProductStocksId { get; set; }

        // foreign key to purchaseorders
        public int? OrderId { get; set; }

        // navigational properties

        public Enterprise Enterprises { get; set; }
        
        public ICollection<ProductInProductStocks> ProductsInProductStocks { get; set; }


    }

    public class ProductInProductStocks
    {
        [Key]
        public int ProductInProductStocksId { get; set; }

        // foreign key to Materials Table
        public int ProductId { get; set; }

        
        public int? QuantityRecieved { get; set; }

        public int? QuantityDispatched { get; set; }

        public float? Cost { get; set; }

        public string Remarks { get; set; }

    }

    public class ProductInProductStockWasted
    {
        [Key]
        public int ProductInProductStockWastedId { get; set; }

        // foreign key to Materials Table
        public int ProductId { get; set; }

        public int? Quantity{ get; set; }

        public string Remarks { get; set; }
    }

    public class ProductStockWasted
    {
        [Key]
        public int ProductStockWastedId { get; set; }

        public DateTime? Date { get; set; }

        // navigational properties
        public ICollection<ProductInProductStockWasted> ProductsInProductStockWasted { get; set; }
    }   
}