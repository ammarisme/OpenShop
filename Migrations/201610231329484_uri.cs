namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uri : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enterprises", "Uri", c => c.String());
            AddColumn("dbo.Services", "Uri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "Uri");
            DropColumn("dbo.Enterprises", "Uri");
        }
    }
}
