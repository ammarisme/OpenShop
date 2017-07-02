namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class account_services : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountsServices",
                c => new
                    {
                        AccountServiceId = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountServiceId)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.Id)
                .Index(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountsServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.AccountsServices", "Id", "dbo.Accounts");
            DropIndex("dbo.AccountsServices", new[] { "ServiceId" });
            DropIndex("dbo.AccountsServices", new[] { "Id" });
            DropTable("dbo.AccountsServices");
        }
    }
}
