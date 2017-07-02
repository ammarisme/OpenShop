namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productfk : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ProductsInQuotations", "ProductId");
            AddForeignKey("dbo.ProductsInQuotations", "ProductId", "dbo.Product", "ProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsInQuotations", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductsInQuotations", new[] { "ProductId" });
        }
    }
}
