namespace ETrading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PO3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PurchaseOrders", name: "VendorId", newName: "Vendors_VendorId");
            RenameIndex(table: "dbo.PurchaseOrders", name: "IX_VendorId", newName: "IX_Vendors_VendorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PurchaseOrders", name: "IX_Vendors_VendorId", newName: "IX_VendorId");
            RenameColumn(table: "dbo.PurchaseOrders", name: "Vendors_VendorId", newName: "VendorId");
        }
    }
}
