namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class online_order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OnlineOrders",
                c => new
                    {
                        OnlineOrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(),
                        OrderDueDate = c.DateTime(),
                        OrderStatus = c.String(),
                        DeliveredDate = c.DateTime(),
                        DeliveryStatus = c.String(),
                        PaymentMethod = c.String(),
                        PaymentDuration = c.Int(nullable: false),
                        DeliveryMode = c.String(),
                        Remark = c.String(),
                        CustomerId = c.Int(nullable: false),
                        EnterpriseId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OnlineOrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId)
                .Index(t => t.CustomerId)
                .Index(t => t.EnterpriseId);
            
            CreateTable(
                "dbo.ProductsInOnlineOrders",
                c => new
                    {
                        ProductInWholesaleSaleId = c.Int(nullable: false, identity: true),
                        UnitPrice = c.Single(nullable: false),
                        OnlineOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ProductInWholesaleSaleId)
                .ForeignKey("dbo.OnlineOrders", t => t.OnlineOrderId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.OnlineOrderId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsInOnlineOrders", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductsInOnlineOrders", "OnlineOrderId", "dbo.OnlineOrders");
            DropForeignKey("dbo.OnlineOrders", "EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.OnlineOrders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ProductsInOnlineOrders", new[] { "ProductId" });
            DropIndex("dbo.ProductsInOnlineOrders", new[] { "OnlineOrderId" });
            DropIndex("dbo.OnlineOrders", new[] { "EnterpriseId" });
            DropIndex("dbo.OnlineOrders", new[] { "CustomerId" });
            DropTable("dbo.ProductsInOnlineOrders");
            DropTable("dbo.OnlineOrders");
        }
    }
}
