namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wholesaler", "Currency", c => c.String());
            AddColumn("dbo.Wholesaler", "Country", c => c.String());
            AddColumn("dbo.Wholesaler", "Region", c => c.String());
            AddColumn("dbo.Product", "ShortDescription", c => c.String());
            AddColumn("dbo.Product", "LongDescription", c => c.String());
            AddColumn("dbo.ProductsInQuotations", "UnitPrice", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductsInQuotations", "UnitPrice");
            DropColumn("dbo.Product", "LongDescription");
            DropColumn("dbo.Product", "ShortDescription");
            DropColumn("dbo.Wholesaler", "Region");
            DropColumn("dbo.Wholesaler", "Country");
            DropColumn("dbo.Wholesaler", "Currency");
        }
    }
}
