namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "EnterpriseId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Product", "EnterpriseId");
            AddForeignKey("dbo.Product", "EnterpriseId", "dbo.Enterprises", "EnterpriseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "EnterpriseId", "dbo.Enterprises");
            DropIndex("dbo.Product", new[] { "EnterpriseId" });
            DropColumn("dbo.Product", "EnterpriseId");
        }
    }
}
