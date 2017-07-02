using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WholesaleTradingPortal.Models
{
    // Base class for all the orders {RetailSales, Wholesale Sales}
    public class Order
    {
            [Key]
            public int OrderId { get; set; }

            public DateTime? OrderDate { get; set; }
            //foreign key to Customers
            [ForeignKey("Accounts")]
            public string AccountId { get; set; }


            public DateTime? OrderDueDate { get; set; }

            public string OrderStatus { get; set; }
            [NotMapped]
            public string OrderStatusId { get; set; }

            public DateTime? DeliveredDate { get; set; }

            public string DeliveryStatus { get; set; }
            [NotMapped]
            public string DeliverStatusId { get; set; }

            public string PaymentMethod { get; set; }
            [NotMapped]
            public string PaymentMethodId { get; set; }

            public int PaymentDuration { get; set; }

            public string DeliveryMode { get; set; }
            [NotMapped]
            public string DeliveryModeId { get; set; }
            

            public string Remark { get; set; }

    }

    // Base class for Product in orders / product in returns
    public class ProductInOrder
    {
        [ForeignKey("Products")]
        public int ProductId { get; set; }

        public float Quantity { get; set; }

        public string Remark { get; set; }

        public Product Products { get; set; }
    }

    //[Table("PurchaseOrders")]
    //public class PurchaseOrder : Order
    //{
    //    [ForeignKey("Suppliers")]
    //    public string SupplierId { get; set; }

    //    // navigational properties
    //    public ICollection<ProductInPurchaseOrder> ProductsInPurchaseOrders { get; set; }

    //    public Account Accounts { get; set; }

    //    public Enterprise Suppliers { get; set; }
    //}

    //[Table("ProductsInPurchaseOrders")]
    //public class ProductInPurchaseOrder : ProductInOrder
    //{
    //    [Key]
    //    public int ProductInPurchaseOrderId { get; set; }

    //    public float Cost { get; set; }

    //    [ForeignKey("PurchaseOrders")]
    //    public int PurchaseOrderId { get; set; }

    //    public PurchaseOrder PurchaseOrders { get; set; }
    //}

    //// Sales tables and Models

    //[Table("RetailSales")]
    //public class RetailSale : Order
    //{
    //    //foreign key to Customers
    //    [ForeignKey("Customers")]
    //    public int CustomerId { get; set; }

    //    public ICollection<ProductInRetailSale> ProductsInRetailOrder { get; set; }

    //    public Customer Customers { get; set; }

    //    public Account Accounts { get; set; }
    //}

    //[Table("ProductsInRetailSales")]
    //public class ProductInRetailSale : ProductInOrder
    //{
    //    [Key]
    //    public int ProductInRetailSaleId { get; set; }

    //    public float UnitPrice { get; set; }

    //    [ForeignKey("RetailSales")]
    //    public int RetailSaleId { get; set; }

    //    public RetailSale RetailSales { get; set; }
    //}

    //[Table("ProductsInRetailSaleReturns")]
    //public class ProductInRetailSaleReturn 
    //{
    //    [Key]
    //    public int ProductInRetailSaleReturnId { get; set; }
        
    //    [ForeignKey("RetailSales")]
    //    public int RetailSaleId { get; set; }

    //    public RetailSale RetailSales { get; set; }
    //}


    //// Wholesale sales tables
    //[Table("WholesaleSales")]
    //public class WholesaleSale : Order
    //{
    //    //foreign key to Enterprises
    //    [ForeignKey("Enterprises")]
    //    public int EnterpriseId { get; set; }

    //    public ICollection<ProductInWholesaleSale> ProductsInWholesaleSale { get; set; }

    //    public Enterprise Enterprises { get; set; }

    //    public Account Accounts { get; set; }
    //}

    //[Table("ProductsInWholesaleSales")]
    //public class ProductInWholesaleSale : ProductInOrder
    //{
    //    [Key]
    //    public int ProductInWholesaleSaleId { get; set; }

    //    public float UnitPrice { get; set; }

    //    [ForeignKey("WholesaleSales")]
    //    public int WholesaleSaleId { get; set; }

    //    public WholesaleSale WholesaleSales { get; set; }
    //}

    //[Table("ProductsInWholesaleSaleReturns")]
    //public class ProductInWholesaleSaleReturn
    //{
    //    [Key]
    //    public int ProductInWholesaleSaleReturnId { get; set; }

    //    [ForeignKey("WholesaleSales")]
    //    public int WholesaleSaleId { get; set; }

    //    public WholesaleSale WholesaleSales { get; set; }
    //}

    [Table("OnlineOrders")]
    public class OnlineOrder {

        [Key]
        public int OnlineOrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? OrderDueDate { get; set; }

        public string OrderStatus { get; set; }
        [NotMapped]
        public string OrderStatusId { get; set; }

        public DateTime? DeliveredDate { get; set; }

        public string DeliveryStatus { get; set; }
        [NotMapped]
        public string DeliverStatusId { get; set; }

        public string PaymentMethod { get; set; }
        [NotMapped]
        public string PaymentMethodId { get; set; }

        public int PaymentDuration { get; set; }

        public string DeliveryMode { get; set; }
        [NotMapped]
        public string DeliveryModeId { get; set; }

        public string Remark { get; set; }

        [ForeignKey("Accounts")]
        public string Id { get; set; }
        //[ForeignKey("Customers")]
        //public int CustomerId { get; set; }

        //foreign key to Customers
        [ForeignKey("Enterprises")]
        public int EnterpriseId { get; set; }

        public Enterprise Enterprises { get; set; }

        public Account Accounts { get; set; }
        //public Customer Customers { get; set; }
    }

    [Table("ProductsInOnlineOrders")]
    public class ProductInOnlineOrder : ProductInOrder
    {
        [Key]
        public int ProductInWholesaleSaleId { get; set; }

        public float UnitPrice { get; set; }

        [ForeignKey("OnlineOrders")]
        public int OnlineOrderId { get; set; }

        public OnlineOrder OnlineOrders { get; set; }
    }
}
