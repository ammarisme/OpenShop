namespace RetailEnterprise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sales : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProductsInSales", name: "RetailOrderId", newName: "SaleId");
            RenameIndex(table: "dbo.ProductsInSales", name: "IX_RetailOrderId", newName: "IX_SaleId");
            CreateTable(
                "dbo.ProductsInSaleReturns",
                c => new
                    {
                        ProductInSaleReturnId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductInSaleReturnId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.SaleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsInSaleReturns", "SaleId", "dbo.Sales");
            DropIndex("dbo.ProductsInSaleReturns", new[] { "SaleId" });
            DropTable("dbo.ProductsInSaleReturns");
            RenameIndex(table: "dbo.ProductsInSales", name: "IX_SaleId", newName: "IX_RetailOrderId");
            RenameColumn(table: "dbo.ProductsInSales", name: "SaleId", newName: "RetailOrderId");
        }
    }
}
