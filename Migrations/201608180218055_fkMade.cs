namespace ETrading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkMade : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ProductInRetailOrders", "ProductId");
            CreateIndex("dbo.ProductInWholesaleOrders", "ProductId");
            AddForeignKey("dbo.ProductInRetailOrders", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.ProductInWholesaleOrders", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductInWholesaleOrders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductInRetailOrders", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductInWholesaleOrders", new[] { "ProductId" });
            DropIndex("dbo.ProductInRetailOrders", new[] { "ProductId" });
        }
    }
}
