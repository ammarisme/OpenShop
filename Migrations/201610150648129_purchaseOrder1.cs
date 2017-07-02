namespace RetailEnterprise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchaseOrder1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrders", "SentDate", c => c.DateTime());
            AddColumn("dbo.PurchaseOrders", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrders", "Status");
            DropColumn("dbo.PurchaseOrders", "SentDate");
        }
    }
}
