namespace ETrading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PO2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PurchaseOrders", name: "Supplier", newName: "VendorId");
            RenameIndex(table: "dbo.PurchaseOrders", name: "IX_Supplier", newName: "IX_VendorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PurchaseOrders", name: "IX_VendorId", newName: "IX_Supplier");
            RenameColumn(table: "dbo.PurchaseOrders", name: "VendorId", newName: "Supplier");
        }
    }
}
