namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enterprise_services : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnterpriseServices",
                c => new
                    {
                        EnterpriseServiceId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        EnterpriseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnterpriseServiceId)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId)
                .Index(t => t.EnterpriseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnterpriseServices", "EnterpriseId", "dbo.Enterprises");
            DropIndex("dbo.EnterpriseServices", new[] { "EnterpriseId" });
            DropTable("dbo.EnterpriseServices");
        }
    }
}
