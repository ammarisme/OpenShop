namespace ETrading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ke : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductInSales", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductInPurchaseOrders", new[] { "Product_ProductId" });
            DropColumn("dbo.ProductInSales", "ProductId");
            DropColumn("dbo.ProductInPurchaseOrders", "ProductId");
            RenameColumn(table: "dbo.ProductInPurchaseOrders", name: "WholesaleOrderId", newName: "PurchaseOrder");
            RenameColumn(table: "dbo.ProductInSales", name: "Product_ProductId", newName: "ProductId");
            RenameColumn(table: "dbo.ProductInPurchaseOrders", name: "Product_ProductId", newName: "ProductId");
            RenameIndex(table: "dbo.ProductInPurchaseOrders", name: "IX_WholesaleOrderId", newName: "IX_PurchaseOrder");
            DropPrimaryKey("dbo.ProductInPurchaseOrders");
            AddColumn("dbo.ProductInPurchaseOrders", "ProductInPurchaseOrderId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ProductInSales", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductInPurchaseOrders", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductInPurchaseOrders", "ProductInPurchaseOrderId");
            CreateIndex("dbo.ProductInPurchaseOrders", "ProductId");
            CreateIndex("dbo.ProductInSales", "ProductId");
            DropColumn("dbo.ProductInPurchaseOrders", "ProductInWholesaleOrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductInPurchaseOrders", "ProductInWholesaleOrderId", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.ProductInSales", new[] { "ProductId" });
            DropIndex("dbo.ProductInPurchaseOrders", new[] { "ProductId" });
            DropPrimaryKey("dbo.ProductInPurchaseOrders");
            AlterColumn("dbo.ProductInPurchaseOrders", "ProductId", c => c.Int());
            AlterColumn("dbo.ProductInSales", "ProductId", c => c.Int());
            DropColumn("dbo.ProductInPurchaseOrders", "ProductInPurchaseOrderId");
            AddPrimaryKey("dbo.ProductInPurchaseOrders", "ProductInWholesaleOrderId");
            RenameIndex(table: "dbo.ProductInPurchaseOrders", name: "IX_PurchaseOrder", newName: "IX_WholesaleOrderId");
            RenameColumn(table: "dbo.ProductInPurchaseOrders", name: "ProductId", newName: "Product_ProductId");
            RenameColumn(table: "dbo.ProductInSales", name: "ProductId", newName: "Product_ProductId");
            RenameColumn(table: "dbo.ProductInPurchaseOrders", name: "PurchaseOrder", newName: "WholesaleOrderId");
            AddColumn("dbo.ProductInPurchaseOrders", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.ProductInSales", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductInPurchaseOrders", "Product_ProductId");
            CreateIndex("dbo.ProductInSales", "Product_ProductId");
        }
    }
}
