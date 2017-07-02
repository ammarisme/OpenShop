namespace RetailEnterprise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accounts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quotations", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Quotations", new[] { "Account_Id" });
            AddColumn("dbo.Accounts", "FirstName", c => c.String());
            AddColumn("dbo.Accounts", "LastName", c => c.String());
            AddColumn("dbo.Accounts", "Address", c => c.String());
            AddColumn("dbo.Accounts", "Designation", c => c.String());
            DropColumn("dbo.Accounts", "RetailerName");
            DropColumn("dbo.Accounts", "RetailerAddress");
            DropColumn("dbo.Accounts", "Rating");
            DropColumn("dbo.Accounts", "BusinessPhoneNumber");
            DropColumn("dbo.Accounts", "BRCNumber");
            DropColumn("dbo.Accounts", "Category");
            DropColumn("dbo.Accounts", "Currency");
            DropColumn("dbo.Quotations", "Account_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotations", "Account_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Accounts", "Currency", c => c.String());
            AddColumn("dbo.Accounts", "Category", c => c.String());
            AddColumn("dbo.Accounts", "BRCNumber", c => c.String());
            AddColumn("dbo.Accounts", "BusinessPhoneNumber", c => c.String());
            AddColumn("dbo.Accounts", "Rating", c => c.Single(nullable: false));
            AddColumn("dbo.Accounts", "RetailerAddress", c => c.String());
            AddColumn("dbo.Accounts", "RetailerName", c => c.String());
            DropColumn("dbo.Accounts", "Designation");
            DropColumn("dbo.Accounts", "Address");
            DropColumn("dbo.Accounts", "LastName");
            DropColumn("dbo.Accounts", "FirstName");
            CreateIndex("dbo.Quotations", "Account_Id");
            AddForeignKey("dbo.Quotations", "Account_Id", "dbo.Accounts", "Id");
        }
    }
}
