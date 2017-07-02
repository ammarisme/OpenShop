namespace RetailTradingPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        Status = c.String(),
                        BillingAddress = c.String(),
                        Remark = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.ProductInProductStockWasted",
                c => new
                    {
                        ProductInProductStockWastedId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductStockWastedId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        Remarks = c.String(),
                        ProductStockWasted_ProductStockWastedId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInProductStockWastedId)
                .ForeignKey("dbo.ProductStockWasted", t => t.ProductStockWasted_ProductStockWastedId)
                .Index(t => t.ProductStockWasted_ProductStockWastedId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Unit = c.String(),
                        RetailPrice = c.Single(),
                        WholesalePrice = c.Single(),
                        StocksQuantity = c.Single(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductsInPurchaseOrders",
                c => new
                    {
                        ProductInPurchaseOrderId = c.Int(nullable: false, identity: true),
                        Cost = c.Single(nullable: false),
                        PurchaseOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ProductInPurchaseOrderId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        SupplierId = c.String(maxLength: 128),
                        OrderDate = c.DateTime(),
                        OrderDueDate = c.DateTime(),
                        OrderStatus = c.String(),
                        DeliveredDate = c.DateTime(),
                        DeliveryStatus = c.String(),
                        PaymentMethod = c.String(),
                        PaymentDuration = c.Int(nullable: false),
                        DeliveryMode = c.String(),
                        Remark = c.String(),
                        AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Enterprises", t => t.SupplierId)
                .Index(t => t.SupplierId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Enterprises",
                c => new
                    {
                        EnterpriseId = c.String(nullable: false, maxLength: 128),
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
                        SupplierId = c.String(maxLength: 128),
                        Status = c.String(),
                        PaymentMethod = c.String(),
                        PaymentDuration = c.Int(nullable: false),
                        DeliveryMethod = c.String(),
                    })
                .PrimaryKey(t => t.QuotationId)
                .ForeignKey("dbo.Enterprises", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
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
                "dbo.ProductsInRetailSales",
                c => new
                    {
                        ProductInRetailSaleId = c.Int(nullable: false, identity: true),
                        UnitPrice = c.Single(nullable: false),
                        RetailSaleId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ProductInRetailSaleId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.RetailSales", t => t.RetailSaleId)
                .Index(t => t.RetailSaleId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.RetailSales",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(),
                        OrderDueDate = c.DateTime(),
                        OrderStatus = c.String(),
                        DeliveredDate = c.DateTime(),
                        DeliveryStatus = c.String(),
                        PaymentMethod = c.String(),
                        PaymentDuration = c.Int(nullable: false),
                        DeliveryMode = c.String(),
                        Remark = c.String(),
                        AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.AccountId);
            
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
                "dbo.ProductInProductStocks",
                c => new
                    {
                        ProductInProductStocksId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductStocksId = c.Int(nullable: false),
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
                "dbo.ProductsInRetailSaleReturns",
                c => new
                    {
                        ProductInRetailSaleReturnId = c.Int(nullable: false, identity: true),
                        RetailSaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductInRetailSaleReturnId)
                .ForeignKey("dbo.RetailSales", t => t.RetailSaleId)
                .Index(t => t.RetailSaleId);
            
            CreateTable(
                "dbo.ProductsInWholesaleSaleReturns",
                c => new
                    {
                        ProductInWholesaleSaleReturnId = c.Int(nullable: false, identity: true),
                        WholesaleSaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductInWholesaleSaleReturnId)
                .ForeignKey("dbo.WholesaleSales", t => t.WholesaleSaleId)
                .Index(t => t.WholesaleSaleId);
            
            CreateTable(
                "dbo.WholesaleSales",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        EnterpriseId = c.String(maxLength: 128),
                        OrderDate = c.DateTime(),
                        OrderDueDate = c.DateTime(),
                        OrderStatus = c.String(),
                        DeliveredDate = c.DateTime(),
                        DeliveryStatus = c.String(),
                        PaymentMethod = c.String(),
                        PaymentDuration = c.Int(nullable: false),
                        DeliveryMode = c.String(),
                        Remark = c.String(),
                        AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId)
                .Index(t => t.EnterpriseId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.ProductsInWholesaleSales",
                c => new
                    {
                        ProductInWholesaleSaleId = c.Int(nullable: false, identity: true),
                        UnitPrice = c.Single(nullable: false),
                        WholesaleSaleId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ProductInWholesaleSaleId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.WholesaleSales", t => t.WholesaleSaleId)
                .Index(t => t.WholesaleSaleId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductStockWasted",
                c => new
                    {
                        ProductStockWastedId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        ApplicationUserId = c.String(),
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
                "dbo.ProductStocks",
                c => new
                    {
                        ProductStocksId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(),
                        RecievedDate = c.DateTime(),
                        ApplicationUserId = c.String(),
                        SupplierId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductStocksId)
                .ForeignKey("dbo.Enterprises", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
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
            DropForeignKey("dbo.ProductStocks", "SupplierId", "dbo.Enterprises");
            DropForeignKey("dbo.ProductInProductStocks", "ProductStocks_ProductStocksId", "dbo.ProductStocks");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.ProductInProductStockWasted", "ProductStockWasted_ProductStockWastedId", "dbo.ProductStockWasted");
            DropForeignKey("dbo.ProductsInWholesaleSaleReturns", "WholesaleSaleId", "dbo.WholesaleSales");
            DropForeignKey("dbo.WholesaleSales", "EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.ProductsInWholesaleSales", "WholesaleSaleId", "dbo.WholesaleSales");
            DropForeignKey("dbo.ProductsInWholesaleSales", "ProductId", "dbo.Product");
            DropForeignKey("dbo.WholesaleSales", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.ProductsInRetailSaleReturns", "RetailSaleId", "dbo.RetailSales");
            DropForeignKey("dbo.SpecificationInProduct", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductsInRetailSales", "RetailSaleId", "dbo.RetailSales");
            DropForeignKey("dbo.RetailSales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.RetailSales", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.ProductsInRetailSales", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductsInPurchaseOrders", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrders", "SupplierId", "dbo.Enterprises");
            DropForeignKey("dbo.Quotations", "SupplierId", "dbo.Enterprises");
            DropForeignKey("dbo.ProductsInQuotations", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.ProductsInQuotations", "ProductId", "dbo.Product");
            DropForeignKey("dbo.PurchaseOrders", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.ProductsInPurchaseOrders", "ProductId", "dbo.Product");
            DropIndex("dbo.Accounts", new[] { "Id" });
            DropIndex("dbo.ProductStocks", new[] { "SupplierId" });
            DropIndex("dbo.ProductsInWholesaleSales", new[] { "ProductId" });
            DropIndex("dbo.ProductsInWholesaleSales", new[] { "WholesaleSaleId" });
            DropIndex("dbo.WholesaleSales", new[] { "AccountId" });
            DropIndex("dbo.WholesaleSales", new[] { "EnterpriseId" });
            DropIndex("dbo.ProductsInWholesaleSaleReturns", new[] { "WholesaleSaleId" });
            DropIndex("dbo.ProductsInRetailSaleReturns", new[] { "RetailSaleId" });
            DropIndex("dbo.ProductInProductStocks", new[] { "ProductStocks_ProductStocksId" });
            DropIndex("dbo.SpecificationInProduct", new[] { "Product_ProductId" });
            DropIndex("dbo.RetailSales", new[] { "AccountId" });
            DropIndex("dbo.RetailSales", new[] { "CustomerId" });
            DropIndex("dbo.ProductsInRetailSales", new[] { "ProductId" });
            DropIndex("dbo.ProductsInRetailSales", new[] { "RetailSaleId" });
            DropIndex("dbo.ProductsInQuotations", new[] { "QuotationId" });
            DropIndex("dbo.ProductsInQuotations", new[] { "ProductId" });
            DropIndex("dbo.Quotations", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseOrders", new[] { "AccountId" });
            DropIndex("dbo.PurchaseOrders", new[] { "SupplierId" });
            DropIndex("dbo.ProductsInPurchaseOrders", new[] { "ProductId" });
            DropIndex("dbo.ProductsInPurchaseOrders", new[] { "PurchaseOrderId" });
            DropIndex("dbo.ProductInProductStockWasted", new[] { "ProductStockWasted_ProductStockWastedId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Accounts");
            DropTable("dbo.ProductStocks");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.ProductStockWasted");
            DropTable("dbo.ProductsInWholesaleSales");
            DropTable("dbo.WholesaleSales");
            DropTable("dbo.ProductsInWholesaleSaleReturns");
            DropTable("dbo.ProductsInRetailSaleReturns");
            DropTable("dbo.ProductInProductStocks");
            DropTable("dbo.SpecificationInProduct");
            DropTable("dbo.RetailSales");
            DropTable("dbo.ProductsInRetailSales");
            DropTable("dbo.ProductsInQuotations");
            DropTable("dbo.Quotations");
            DropTable("dbo.Enterprises");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.ProductsInPurchaseOrders");
            DropTable("dbo.Product");
            DropTable("dbo.ProductInProductStockWasted");
            DropTable("dbo.Customers");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
        }
    }
}
