namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clean1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MaterialInPurchaseOrder", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.Payment", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrder", "PurchaseOrderStatus_PurchaseOrderStatusId", "dbo.PurchaseOrderStatus");
            DropForeignKey("dbo.Customers", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrder");
            DropIndex("dbo.Payment", new[] { "PurchaseOrder_PurchaseOrderId" });
            DropIndex("dbo.PurchaseOrder", new[] { "PurchaseOrderStatus_PurchaseOrderStatusId" });
            DropIndex("dbo.MaterialInPurchaseOrder", new[] { "PurchaseOrder_PurchaseOrderId" });
            DropIndex("dbo.Customers", new[] { "PurchaseOrder_PurchaseOrderId" });
            DropColumn("dbo.Customers", "PurchaseOrder_PurchaseOrderId");
            DropColumn("dbo.Payment", "PurchaseOrder_PurchaseOrderId");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.MaterialInPurchaseOrder");
            DropTable("dbo.PurchaseOrderStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PurchaseOrderStatus",
                c => new
                    {
                        PurchaseOrderStatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.PurchaseOrderStatusId);
            
            CreateTable(
                "dbo.MaterialInPurchaseOrder",
                c => new
                    {
                        MaterialInPurchaseOrderId = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitCost = c.Single(nullable: false),
                        Description = c.String(),
                        PurchaseOrder_PurchaseOrderId = c.Int(),
                    })
                .PrimaryKey(t => t.MaterialInPurchaseOrderId);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(),
                        Status = c.String(),
                        PurchaseOrderStatus_PurchaseOrderStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.PurchaseOrderId);
            
            AddColumn("dbo.Payment", "PurchaseOrder_PurchaseOrderId", c => c.Int());
            AddColumn("dbo.Customers", "PurchaseOrder_PurchaseOrderId", c => c.Int());
            CreateIndex("dbo.Customers", "PurchaseOrder_PurchaseOrderId");
            CreateIndex("dbo.MaterialInPurchaseOrder", "PurchaseOrder_PurchaseOrderId");
            CreateIndex("dbo.PurchaseOrder", "PurchaseOrderStatus_PurchaseOrderStatusId");
            CreateIndex("dbo.Payment", "PurchaseOrder_PurchaseOrderId");
            AddForeignKey("dbo.Customers", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrder", "PurchaseOrderId");
            AddForeignKey("dbo.PurchaseOrder", "PurchaseOrderStatus_PurchaseOrderStatusId", "dbo.PurchaseOrderStatus", "PurchaseOrderStatusId");
            AddForeignKey("dbo.Payment", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrder", "PurchaseOrderId");
            AddForeignKey("dbo.MaterialInPurchaseOrder", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrder", "PurchaseOrderId");
        }
    }
}
