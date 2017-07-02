namespace ETrading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PO4 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Vendors");
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.String(nullable: false, maxLength: 128),
                        SupplierName = c.String(),
                        SupplierAddress = c.String(),
                        Rating = c.Single(),
                        BusinessPhoneNumber = c.String(),
                        Status = c.String(),
                        BRCNumber = c.String(),
                        Category = c.String(),
                        Currency = c.String(),
                        Country = c.String(),
                        Region = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            
        }
    }
}
