namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clean2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MaterialInProduct", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductInCustomerOrder", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductInJob", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.PaymentMode", "Payment_PaymentId", "dbo.Payment");
            DropForeignKey("dbo.PaymentStatus", "Payment_PaymentId", "dbo.Payment");
            DropIndex("dbo.MaterialInProduct", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductInCustomerOrder", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductInJob", new[] { "Product_ProductId" });
            DropIndex("dbo.PaymentMode", new[] { "Payment_PaymentId" });
            DropIndex("dbo.PaymentStatus", new[] { "Payment_PaymentId" });
            DropTable("dbo.MaterialInProduct");
            DropTable("dbo.ProductInCustomerOrder");
            DropTable("dbo.ProductInJob");
            DropTable("dbo.Payment");
            DropTable("dbo.PaymentMode");
            DropTable("dbo.PaymentStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        PaymentStatusId = c.Int(nullable: false, identity: true),
                        PaymentStatusName = c.String(),
                        Payment_PaymentId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentStatusId);
            
            CreateTable(
                "dbo.PaymentMode",
                c => new
                    {
                        PaymentModeId = c.Int(nullable: false, identity: true),
                        PaymentModeName = c.String(),
                        Payment_PaymentId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentModeId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        IssuedDate = c.DateTime(),
                        RealizationDate = c.DateTime(),
                        PaymentMode = c.String(),
                        PurchaseOrderId = c.Int(),
                        Status = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.PaymentId);
            
            CreateTable(
                "dbo.ProductInJob",
                c => new
                    {
                        ProductInJobId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Remark = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInJobId);
            
            CreateTable(
                "dbo.ProductInCustomerOrder",
                c => new
                    {
                        ProductsInCustomerOrderId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CustomerOrderId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Description = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductsInCustomerOrderId);
            
            CreateTable(
                "dbo.MaterialInProduct",
                c => new
                    {
                        MaterialInProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.MaterialInProductId);
            
            CreateIndex("dbo.PaymentStatus", "Payment_PaymentId");
            CreateIndex("dbo.PaymentMode", "Payment_PaymentId");
            CreateIndex("dbo.ProductInJob", "Product_ProductId");
            CreateIndex("dbo.ProductInCustomerOrder", "Product_ProductId");
            CreateIndex("dbo.MaterialInProduct", "Product_ProductId");
            AddForeignKey("dbo.PaymentStatus", "Payment_PaymentId", "dbo.Payment", "PaymentId");
            AddForeignKey("dbo.PaymentMode", "Payment_PaymentId", "dbo.Payment", "PaymentId");
            AddForeignKey("dbo.ProductInJob", "Product_ProductId", "dbo.Product", "ProductId");
            AddForeignKey("dbo.ProductInCustomerOrder", "Product_ProductId", "dbo.Product", "ProductId");
            AddForeignKey("dbo.MaterialInProduct", "Product_ProductId", "dbo.Product", "ProductId");
        }
    }
}
