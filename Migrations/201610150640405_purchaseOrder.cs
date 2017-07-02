namespace RetailEnterprise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchaseOrder : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PurchaseOrders", name: "RetailerId", newName: "AccountId");
            RenameColumn(table: "dbo.PurchaseOrders", name: "WholesalerId", newName: "Seller");
            RenameIndex(table: "dbo.PurchaseOrders", name: "IX_RetailerId", newName: "IX_AccountId");
            RenameIndex(table: "dbo.PurchaseOrders", name: "IX_WholesalerId", newName: "IX_Seller");
            AddColumn("dbo.ProductInSales", "Remark", c => c.String());
            AddColumn("dbo.ProductInPurchaseOrders", "Cost", c => c.Single(nullable: false));
            AddColumn("dbo.ProductInPurchaseOrders", "Remark", c => c.String());
            DropColumn("dbo.ProductInSales", "Description");
            DropColumn("dbo.ProductInPurchaseOrders", "UnitPrice");
            DropColumn("dbo.ProductInPurchaseOrders", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductInPurchaseOrders", "Description", c => c.String());
            AddColumn("dbo.ProductInPurchaseOrders", "UnitPrice", c => c.Single(nullable: false));
            AddColumn("dbo.ProductInSales", "Description", c => c.String());
            DropColumn("dbo.ProductInPurchaseOrders", "Remark");
            DropColumn("dbo.ProductInPurchaseOrders", "Cost");
            DropColumn("dbo.ProductInSales", "Remark");
            RenameIndex(table: "dbo.PurchaseOrders", name: "IX_Seller", newName: "IX_WholesalerId");
            RenameIndex(table: "dbo.PurchaseOrders", name: "IX_AccountId", newName: "IX_RetailerId");
            RenameColumn(table: "dbo.PurchaseOrders", name: "Seller", newName: "WholesalerId");
            RenameColumn(table: "dbo.PurchaseOrders", name: "AccountId", newName: "RetailerId");
        }
    }
}
