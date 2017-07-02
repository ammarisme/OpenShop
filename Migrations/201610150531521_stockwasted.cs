namespace RetailEnterprise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockwasted : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductInProductStockWasted",
                c => new
                    {
                        ProductInProductStockWastedId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductStockWastedId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        Remarks = c.String(),
                        ProductStockWasted_ProductStockWastedId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInProductStockWastedId)
                .ForeignKey("dbo.ProductStockWasted", t => t.ProductStockWasted_ProductStockWastedId)
                .Index(t => t.ProductStockWasted_ProductStockWastedId);
            
            CreateTable(
                "dbo.ProductStockWasted",
                c => new
                    {
                        ProductStockWastedId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        ApplicationUserId = c.String(),
                    })
                .PrimaryKey(t => t.ProductStockWastedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductInProductStockWasted", "ProductStockWasted_ProductStockWastedId", "dbo.ProductStockWasted");
            DropIndex("dbo.ProductInProductStockWasted", new[] { "ProductStockWasted_ProductStockWastedId" });
            DropTable("dbo.ProductStockWasted");
            DropTable("dbo.ProductInProductStockWasted");
        }
    }
}
