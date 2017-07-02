namespace WholesaleTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Enterprises",
                c => new
                    {
                        EnterpriseId = c.Int(nullable: false, identity: true),
                        EnterpriseName = c.String(),
                        EnterpriseAddress = c.String(),
                        Rating = c.Single(),
                        BusinessPhoneNumber = c.String(),
                        Status = c.String(),
                        BRCNumber = c.String(),
                        Category = c.String(),
                        Currency = c.String(),
                        Country = c.String(),
                        Region = c.String(),
                    })
                .PrimaryKey(t => t.EnterpriseId);
            
            CreateTable(
                "dbo.Quotations",
                c => new
                    {
                        QuotationId = c.Int(nullable: false, identity: true),
                        EnterpriseId = c.Int(nullable: false),
                        Status = c.String(),
                        PaymentMethod = c.String(),
                        PaymentDuration = c.Int(nullable: false),
                        DeliveryMethod = c.String(),
                    })
                .PrimaryKey(t => t.QuotationId)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId)
                .Index(t => t.EnterpriseId);
            
            CreateTable(
                "dbo.ProductsInQuotations",
                c => new
                    {
                        ProductInQuotationId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        QuotationId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ProductInQuotationId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Quotations", t => t.QuotationId)
                .Index(t => t.ProductId)
                .Index(t => t.QuotationId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Unit = c.String(),
                        Price = c.Single(),
                        StocksQuantity = c.Single(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        Category = c.String(),
                        EnterpriseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId)
                .Index(t => t.EnterpriseId);
            
            CreateTable(
                "dbo.SpecificationInProduct",
                c => new
                    {
                        SpecificationInProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Specification = c.String(),
                        Value = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.SpecificationInProductId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.OnlineOrders",
                c => new
                    {
                        OnlineOrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(),
                        OrderDueDate = c.DateTime(),
                        OrderStatus = c.String(),
                        DeliveredDate = c.DateTime(),
                        DeliveryStatus = c.String(),
                        PaymentMethod = c.String(),
                        PaymentDuration = c.Int(nullable: false),
                        DeliveryMode = c.String(),
                        Remark = c.String(),
                        Id = c.String(maxLength: 128),
                        EnterpriseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OnlineOrderId)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId)
                .Index(t => t.Id)
                .Index(t => t.EnterpriseId);
            
            CreateTable(
                "dbo.ProductInProductStockWasted",
                c => new
                    {
                        ProductInProductStockWastedId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        Remarks = c.String(),
                        ProductStockWasted_ProductStockWastedId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInProductStockWastedId)
                .ForeignKey("dbo.ProductStockWasted", t => t.ProductStockWasted_ProductStockWastedId)
                .Index(t => t.ProductStockWasted_ProductStockWastedId);
            
            CreateTable(
                "dbo.ProductsInOnlineOrders",
                c => new
                    {
                        ProductInWholesaleSaleId = c.Int(nullable: false, identity: true),
                        UnitPrice = c.Single(nullable: false),
                        OnlineOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ProductInWholesaleSaleId)
                .ForeignKey("dbo.OnlineOrders", t => t.OnlineOrderId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.OnlineOrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductInProductStocks",
                c => new
                    {
                        ProductInProductStocksId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        QuantityRecieved = c.Int(),
                        QuantityDispatched = c.Int(),
                        Cost = c.Single(),
                        Remarks = c.String(),
                        ProductStocks_ProductStocksId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInProductStocksId)
                .ForeignKey("dbo.ProductStocks", t => t.ProductStocks_ProductStocksId)
                .Index(t => t.ProductStocks_ProductStocksId);
            
            CreateTable(
                "dbo.ProductStockWasted",
                c => new
                    {
                        ProductStockWastedId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductStockWastedId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemIdNumber = c.String(),
                        Name = c.String(),
                        ReportHead = c.String(),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductStocks",
                c => new
                    {
                        ProductStocksId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(),
                        Enterprises_EnterpriseId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductStocksId)
                .ForeignKey("dbo.Enterprises", t => t.Enterprises_EnterpriseId)
                .Index(t => t.Enterprises_EnterpriseId);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        PhoneNumber2 = c.String(),
                        Status = c.String(),
                        Designation = c.String(),
                        Email2 = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ProductInProductStocks", "ProductStocks_ProductStocksId", "dbo.ProductStocks");
            DropForeignKey("dbo.ProductStocks", "Enterprises_EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.ProductInProductStockWasted", "ProductStockWasted_ProductStockWastedId", "dbo.ProductStockWasted");
            DropForeignKey("dbo.ProductsInOnlineOrders", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductsInOnlineOrders", "OnlineOrderId", "dbo.OnlineOrders");
            DropForeignKey("dbo.OnlineOrders", "EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.OnlineOrders", "Id", "dbo.Accounts");
            DropForeignKey("dbo.ProductsInQuotations", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.ProductsInQuotations", "ProductId", "dbo.Product");
            DropForeignKey("dbo.SpecificationInProduct", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.Quotations", "EnterpriseId", "dbo.Enterprises");
            DropIndex("dbo.Accounts", new[] { "Id" });
            DropIndex("dbo.ProductStocks", new[] { "Enterprises_EnterpriseId" });
            DropIndex("dbo.ProductInProductStocks", new[] { "ProductStocks_ProductStocksId" });
            DropIndex("dbo.ProductsInOnlineOrders", new[] { "ProductId" });
            DropIndex("dbo.ProductsInOnlineOrders", new[] { "OnlineOrderId" });
            DropIndex("dbo.ProductInProductStockWasted", new[] { "ProductStockWasted_ProductStockWastedId" });
            DropIndex("dbo.OnlineOrders", new[] { "EnterpriseId" });
            DropIndex("dbo.OnlineOrders", new[] { "Id" });
            DropIndex("dbo.SpecificationInProduct", new[] { "Product_ProductId" });
            DropIndex("dbo.Product", new[] { "EnterpriseId" });
            DropIndex("dbo.ProductsInQuotations", new[] { "QuotationId" });
            DropIndex("dbo.ProductsInQuotations", new[] { "ProductId" });
            DropIndex("dbo.Quotations", new[] { "EnterpriseId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Accounts");
            DropTable("dbo.ProductStocks");
            DropTable("dbo.Settings");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.ProductStockWasted");
            DropTable("dbo.ProductInProductStocks");
            DropTable("dbo.ProductsInOnlineOrders");
            DropTable("dbo.ProductInProductStockWasted");
            DropTable("dbo.OnlineOrders");
            DropTable("dbo.SpecificationInProduct");
            DropTable("dbo.Product");
            DropTable("dbo.ProductsInQuotations");
            DropTable("dbo.Quotations");
            DropTable("dbo.Enterprises");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
        }
    }
}
