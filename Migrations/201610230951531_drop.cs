namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EnterpriseServices", "Service_ServiceId", "dbo.Services");
            DropIndex("dbo.EnterpriseServices", new[] { "Service_ServiceId" });
            DropTable("dbo.EnterpriseServices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EnterpriseServices",
                c => new
                    {
                        EnterpriseServiceId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Service_ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.EnterpriseServiceId);
            
            CreateIndex("dbo.EnterpriseServices", "Service_ServiceId");
            AddForeignKey("dbo.EnterpriseServices", "Service_ServiceId", "dbo.Services", "ServiceId");
        }
    }
}
