namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductsInQuotations", new[] { "Quotation_QuotationId" });
            DropColumn("dbo.ProductsInQuotations", "QuotationId");
            RenameColumn(table: "dbo.ProductsInQuotations", name: "Quotation_QuotationId", newName: "QuotationId");
            AlterColumn("dbo.ProductsInQuotations", "QuotationId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductsInQuotations", "QuotationId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductsInQuotations", new[] { "QuotationId" });
            AlterColumn("dbo.ProductsInQuotations", "QuotationId", c => c.Int());
            RenameColumn(table: "dbo.ProductsInQuotations", name: "QuotationId", newName: "Quotation_QuotationId");
            AddColumn("dbo.ProductsInQuotations", "QuotationId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductsInQuotations", "Quotation_QuotationId");
        }
    }
}
