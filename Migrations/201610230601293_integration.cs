namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class integration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enterprises",
                c => new
                    {
                        EnterpriseId = c.Int(nullable: false, identity: true),
                        EnterpriseTypeId = c.Int(nullable: false),
                        AccountId = c.String(),
                        EnterpriseType_EnterpriseTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.EnterpriseId)
                .ForeignKey("dbo.EnterpriseTypes", t => t.EnterpriseType_EnterpriseTypeId)
                .Index(t => t.EnterpriseType_EnterpriseTypeId);
            
            CreateTable(
                "dbo.EnterpriseServices",
                c => new
                    {
                        EnterpriseServiceId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Service_ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.EnterpriseServiceId)
                .ForeignKey("dbo.Services", t => t.Service_ServiceId)
                .Index(t => t.Service_ServiceId);
            
            CreateTable(
                "dbo.EnterpriseTypes",
                c => new
                    {
                        EnterpriseTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.EnterpriseTypeId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnterpriseServices", "Service_ServiceId", "dbo.Services");
            DropForeignKey("dbo.Enterprises", "EnterpriseType_EnterpriseTypeId", "dbo.EnterpriseTypes");
            DropIndex("dbo.EnterpriseServices", new[] { "Service_ServiceId" });
            DropIndex("dbo.Enterprises", new[] { "EnterpriseType_EnterpriseTypeId" });
            DropTable("dbo.Services");
            DropTable("dbo.EnterpriseTypes");
            DropTable("dbo.EnterpriseServices");
            DropTable("dbo.Enterprises");
        }
    }
}
