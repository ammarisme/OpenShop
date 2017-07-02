namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Category");
        }
    }
}
