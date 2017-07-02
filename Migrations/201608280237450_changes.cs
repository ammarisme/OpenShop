namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotations", "PaymentDuration", c => c.Int(nullable: false));
            AddColumn("dbo.Quotations", "DeliveryMethod", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotations", "DeliveryMethod");
            DropColumn("dbo.Quotations", "PaymentDuration");
        }
    }
}
