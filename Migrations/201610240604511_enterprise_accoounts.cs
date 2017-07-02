namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enterprise_accoounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnterpriseAccounts",
                c => new
                    {
                        EnterpriseAccountId = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        EnterpriseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnterpriseAccountId)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId)
                .Index(t => t.Id)
                .Index(t => t.EnterpriseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnterpriseAccounts", "EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.EnterpriseAccounts", "Id", "dbo.Accounts");
            DropIndex("dbo.EnterpriseAccounts", new[] { "EnterpriseId" });
            DropIndex("dbo.EnterpriseAccounts", new[] { "Id" });
            DropTable("dbo.EnterpriseAccounts");
        }
    }
}
