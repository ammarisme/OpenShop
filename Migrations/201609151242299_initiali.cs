namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiali : DbMigration
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
                "dbo.Quotations",
                c => new
                    {
                        QuotationId = c.Int(nullable: false, identity: true),
                        WholesalerId = c.String(maxLength: 128),
                        Status = c.String(),
                        RetailerId = c.String(),
                        PaymentMethod = c.String(),
                        PaymentDuration = c.Int(nullable: false),
                        DeliveryMethod = c.String(),
                        Account_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.QuotationId)
                .ForeignKey("dbo.Wholesalers", t => t.WholesalerId)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.WholesalerId)
                .Index(t => t.Account_Id);
            
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
                        RetailPrice = c.Single(),
                        StocksQuantity = c.Single(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.ProductInSales",
                c => new
                    {
                        ProductInRetailOrderId = c.Int(nullable: false, identity: true),
                        RetailOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Description = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInRetailOrderId)
                .ForeignKey("dbo.Sales", t => t.RetailOrderId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .Index(t => t.RetailOrderId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        RetailerId = c.String(maxLength: 128),
                        OrderDate = c.DateTime(),
                        OrderDueDate = c.DateTime(),
                        OrderStatus = c.String(),
                        DeliveredDate = c.DateTime(),
                        DeliveryStatus = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Accounts", t => t.RetailerId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.RetailerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        Status = c.String(),
                        BillingAddress = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.ProductInPurchaseOrders",
                c => new
                    {
                        ProductInWholesaleOrderId = c.Int(nullable: false, identity: true),
                        WholesaleOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Description = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInWholesaleOrderId)
                .ForeignKey("dbo.PurchaseOrders", t => t.WholesaleOrderId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .Index(t => t.WholesaleOrderId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        RetailerId = c.String(maxLength: 128),
                        WholesalerId = c.String(maxLength: 128),
                        OrderDate = c.DateTime(),
                        OrderDueDate = c.DateTime(),
                        OrderStatus = c.String(),
                        DeliveredDate = c.DateTime(),
                        DeliveryStatus = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Accounts", t => t.RetailerId)
                .ForeignKey("dbo.Wholesalers", t => t.WholesalerId)
                .Index(t => t.RetailerId)
                .Index(t => t.WholesalerId);
            
            CreateTable(
                "dbo.Wholesalers",
                c => new
                    {
                        WholesalerId = c.String(nullable: false, maxLength: 128),
                        WholesalerName = c.String(),
                        WholesalerAddress = c.String(),
                        Rating = c.Single(nullable: false),
                        BusinessPhoneNumber = c.String(),
                        Status = c.String(),
                        BRCNumber = c.String(),
                        Category = c.String(),
                        Currency = c.String(),
                        Country = c.String(),
                        Region = c.String(),
                    })
                .PrimaryKey(t => t.WholesalerId);
            
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
                "dbo.ProductStocks",
                c => new
                    {
                        ProductStocksId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(),
                        RecievedDate = c.DateTime(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductStocksId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.ProductInProductStocks",
                c => new
                    {
                        ProductInProductStocksId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductStocksId = c.Int(nullable: false),
                        QuantityRecieved = c.Int(),
                        QuantityDispatched = c.Int(),
                        Cost = c.Int(),
                        Remarks = c.String(),
                        ProductStocks_ProductStocksId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInProductStocksId)
                .ForeignKey("dbo.ProductStocks", t => t.ProductStocks_ProductStocksId)
                .Index(t => t.ProductStocks_ProductStocksId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RetailerName = c.String(),
                        RetailerAddress = c.String(),
                        Rating = c.Single(nullable: false),
                        BusinessPhoneNumber = c.String(),
                        Status = c.String(),
                        BRCNumber = c.String(),
                        Category = c.String(),
                        Currency = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ProductStocks", "ApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Product", "ApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.ProductInProductStocks", "ProductStocks_ProductStocksId", "dbo.ProductStocks");
            DropForeignKey("dbo.Quotations", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Quotations", "WholesalerId", "dbo.Wholesalers");
            DropForeignKey("dbo.ProductsInQuotations", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.ProductsInQuotations", "ProductId", "dbo.Product");
            DropForeignKey("dbo.SpecificationInProduct", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductInPurchaseOrders", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductInPurchaseOrders", "WholesaleOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrders", "WholesalerId", "dbo.Wholesalers");
            DropForeignKey("dbo.PurchaseOrders", "RetailerId", "dbo.Accounts");
            DropForeignKey("dbo.ProductInSales", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductInSales", "RetailOrderId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sales", "RetailerId", "dbo.Accounts");
            DropIndex("dbo.Accounts", new[] { "Id" });
            DropIndex("dbo.ProductInProductStocks", new[] { "ProductStocks_ProductStocksId" });
            DropIndex("dbo.ProductStocks", new[] { "ApplicationUserId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.SpecificationInProduct", new[] { "Product_ProductId" });
            DropIndex("dbo.PurchaseOrders", new[] { "WholesalerId" });
            DropIndex("dbo.PurchaseOrders", new[] { "RetailerId" });
            DropIndex("dbo.ProductInPurchaseOrders", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductInPurchaseOrders", new[] { "WholesaleOrderId" });
            DropIndex("dbo.Sales", new[] { "RetailerId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.ProductInSales", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductInSales", new[] { "RetailOrderId" });
            DropIndex("dbo.Product", new[] { "ApplicationUserId" });
            DropIndex("dbo.ProductsInQuotations", new[] { "QuotationId" });
            DropIndex("dbo.ProductsInQuotations", new[] { "ProductId" });
            DropIndex("dbo.Quotations", new[] { "Account_Id" });
            DropIndex("dbo.Quotations", new[] { "WholesalerId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Accounts");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.ProductInProductStocks");
            DropTable("dbo.ProductStocks");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.SpecificationInProduct");
            DropTable("dbo.Wholesalers");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.ProductInPurchaseOrders");
            DropTable("dbo.Customers");
            DropTable("dbo.Sales");
            DropTable("dbo.ProductInSales");
            DropTable("dbo.Product");
            DropTable("dbo.ProductsInQuotations");
            DropTable("dbo.Quotations");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
        }
    }
}
