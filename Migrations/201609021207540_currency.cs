namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Retailers", "Currency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Retailers", "Currency");
        }
    }
}
