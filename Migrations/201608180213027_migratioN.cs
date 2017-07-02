namespace ETrading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratioN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Unit = c.String(),
                        RetailPrice = c.Single(),
                        WholesalePrice = c.Single(),
                        StockQuantity = c.Single(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.MaterialInProducts",
                c => new
                    {
                        MaterialInProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialInProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.ProductInCustomerOrders",
                c => new
                    {
                        ProductsInCustomerOrderId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CustomerOrderId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductsInCustomerOrderId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductInJobs",
                c => new
                    {
                        ProductInJobId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ProductInJobId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.SpecificationInProducts",
                c => new
                    {
                        SpecificationInProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Specification = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.SpecificationInProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductStocks",
                c => new
                    {
                        ProductStockId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(),
                        RecievedDate = c.DateTime(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductStockId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.ProductInProductStocks",
                c => new
                    {
                        ProductInProductStockId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductStockId = c.Int(nullable: false),
                        QuantityRecieved = c.Int(),
                        QuantityDispatched = c.Int(),
                        Cost = c.Int(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ProductInProductStockId)
                .ForeignKey("dbo.ProductStocks", t => t.ProductStockId, cascadeDelete: true)
                .Index(t => t.ProductStockId);
            
            CreateTable(
                "dbo.RetailOrders",
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
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Retailers", t => t.RetailerId)
                .Index(t => t.CustomerId)
                .Index(t => t.RetailerId);
            
            CreateTable(
                "dbo.ProductInRetailOrders",
                c => new
                    {
                        ProductInRetailOrderId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        RetailOrderId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductInRetailOrderId)
                .ForeignKey("dbo.RetailOrders", t => t.RetailOrderId, cascadeDelete: true)
                .Index(t => t.RetailOrderId);
            
            CreateTable(
                "dbo.WholesaleOrders",
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
                .ForeignKey("dbo.Retailers", t => t.RetailerId)
                .ForeignKey("dbo.Wholesaler", t => t.WholesalerId)
                .Index(t => t.RetailerId)
                .Index(t => t.WholesalerId);
            
            CreateTable(
                "dbo.ProductInWholesaleOrders",
                c => new
                    {
                        ProductInWholesaleOrderId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        WholesaleOrderId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductInWholesaleOrderId)
                .ForeignKey("dbo.WholesaleOrders", t => t.WholesaleOrderId, cascadeDelete: true)
                .Index(t => t.WholesaleOrderId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Designation = c.String(),
                        Status = c.String(),
                        Remark = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        IssuedDate = c.DateTime(),
                        Status = c.String(),
                        DueDate = c.DateTime(),
                        CompletedDate = c.DateTime(),
                        CustomerOrderId = c.Int(),
                        EmployeeId = c.Int(),
                        Remark = c.String(),
                        Material_MaterialId = c.Int(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Materials", t => t.Material_MaterialId)
                .Index(t => t.EmployeeId)
                .Index(t => t.Material_MaterialId);
            
            CreateTable(
                "dbo.JobStatus",
                c => new
                    {
                        JobStatusId = c.Int(nullable: false, identity: true),
                        JobStatusName = c.String(),
                        Job_JobId = c.Int(),
                    })
                .PrimaryKey(t => t.JobStatusId)
                .ForeignKey("dbo.Jobs", t => t.Job_JobId)
                .Index(t => t.Job_JobId);
            
            CreateTable(
                "dbo.MaterialInJobs",
                c => new
                    {
                        MaterialInJobId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Remark = c.String(),
                        Damaged = c.Boolean(),
                    })
                .PrimaryKey(t => t.MaterialInJobId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.MaterialInMaterialsRecieveds",
                c => new
                    {
                        MaterialInMaterialsRecievedId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        MaterialsRecievedId = c.Int(nullable: false),
                        QuantityRecieved = c.Int(nullable: false),
                        QuantityDispatched = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.MaterialInMaterialsRecievedId)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.MaterialsRecieveds", t => t.MaterialsRecievedId, cascadeDelete: true)
                .Index(t => t.MaterialId)
                .Index(t => t.MaterialsRecievedId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(),
                        Cost = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId);
            
            CreateTable(
                "dbo.MaterialInPurchaseOrders",
                c => new
                    {
                        MaterialInPurchaseOrderId = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitCost = c.Single(nullable: false),
                        Description = c.String(),
                        MaterialUnit_MaterialUnitId = c.Int(),
                    })
                .PrimaryKey(t => t.MaterialInPurchaseOrderId)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.MaterialUnits", t => t.MaterialUnit_MaterialUnitId)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.MaterialId)
                .Index(t => t.MaterialUnit_MaterialUnitId);
            
            CreateTable(
                "dbo.MaterialSpecifications",
                c => new
                    {
                        MaterialSpecificationId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        Specification = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.MaterialSpecificationId)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.MaterialsRecieveds",
                c => new
                    {
                        MaterialsRecievedId = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(),
                        RecievedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MaterialsRecievedId);
            
            CreateTable(
                "dbo.MaterialInStores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialUnits",
                c => new
                    {
                        MaterialUnitId = c.Int(nullable: false, identity: true),
                        UnitName = c.String(),
                    })
                .PrimaryKey(t => t.MaterialUnitId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        IssuedDate = c.DateTime(),
                        RealizationDate = c.DateTime(),
                        PaymentMode = c.String(),
                        PurchaseOrderId = c.Int(),
                        Status = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId)
                .Index(t => t.PurchaseOrderId);
            
            CreateTable(
                "dbo.PaymentModes",
                c => new
                    {
                        PaymentModeId = c.Int(nullable: false, identity: true),
                        PaymentModeName = c.String(),
                        Payment_PaymentId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentModeId)
                .ForeignKey("dbo.Payments", t => t.Payment_PaymentId)
                .Index(t => t.Payment_PaymentId);
            
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        PaymentStatusId = c.Int(nullable: false, identity: true),
                        PaymentStatusName = c.String(),
                        Payment_PaymentId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentStatusId)
                .ForeignKey("dbo.Payments", t => t.Payment_PaymentId)
                .Index(t => t.Payment_PaymentId);
            
            CreateTable(
                "dbo.ProductInStores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(),
                        Status = c.String(),
                        PurchaseOrderStatus_PurchaseOrderStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.PurchaseOrderId)
                .ForeignKey("dbo.PurchaseOrderStatus", t => t.PurchaseOrderStatus_PurchaseOrderStatusId)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.PurchaseOrderStatus_PurchaseOrderStatusId);
            
            CreateTable(
                "dbo.PurchaseOrderStatus",
                c => new
                    {
                        PurchaseOrderStatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.PurchaseOrderStatusId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SalaryPayments",
                c => new
                    {
                        SalaryPaymentId = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        PaidDate = c.DateTime(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.SalaryPaymentId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Telephone = c.String(),
                        ContactPerson = c.String(),
                        Remark = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.VendorId);
            
            CreateTable(
                "dbo.VendorStatus",
                c => new
                    {
                        VendorStatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.VendorStatusId);
            
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Address = c.String(),
                        Status = c.String(),
                        Designation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PurchaseOrder_PurchaseOrderId = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        Status = c.String(),
                        BillingAddress = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_PurchaseOrderId)
                .Index(t => t.Id)
                .Index(t => t.PurchaseOrder_PurchaseOrderId);
            
            CreateTable(
                "dbo.Retailers",
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Wholesaler",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        WholesalerName = c.String(),
                        WholesalerAddress = c.String(),
                        Rating = c.Single(nullable: false),
                        BusinessPhoneNumber = c.String(),
                        Status = c.String(),
                        BRCNumber = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wholesaler", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Retailers", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.Customers", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Administrators", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseOrders", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.ProductStocks", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PurchaseOrders", "PurchaseOrderStatus_PurchaseOrderStatusId", "dbo.PurchaseOrderStatus");
            DropForeignKey("dbo.Payments", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.MaterialInPurchaseOrders", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PaymentStatus", "Payment_PaymentId", "dbo.Payments");
            DropForeignKey("dbo.PaymentModes", "Payment_PaymentId", "dbo.Payments");
            DropForeignKey("dbo.MaterialInPurchaseOrders", "MaterialUnit_MaterialUnitId", "dbo.MaterialUnits");
            DropForeignKey("dbo.MaterialInMaterialsRecieveds", "MaterialsRecievedId", "dbo.MaterialsRecieveds");
            DropForeignKey("dbo.MaterialSpecifications", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.MaterialInPurchaseOrders", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.MaterialInProducts", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.MaterialInMaterialsRecieveds", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Jobs", "Material_MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Jobs", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ProductInJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.MaterialInJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.JobStatus", "Job_JobId", "dbo.Jobs");
            DropForeignKey("dbo.RetailOrders", "RetailerId", "dbo.Retailers");
            DropForeignKey("dbo.WholesaleOrders", "WholesalerId", "dbo.Wholesaler");
            DropForeignKey("dbo.WholesaleOrders", "RetailerId", "dbo.Retailers");
            DropForeignKey("dbo.ProductInWholesaleOrders", "WholesaleOrderId", "dbo.WholesaleOrders");
            DropForeignKey("dbo.ProductInRetailOrders", "RetailOrderId", "dbo.RetailOrders");
            DropForeignKey("dbo.RetailOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ProductInProductStocks", "ProductStockId", "dbo.ProductStocks");
            DropForeignKey("dbo.SpecificationInProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductInJobs", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductInCustomerOrders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.MaterialInProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.Wholesaler", new[] { "Id" });
            DropIndex("dbo.Retailers", new[] { "Id" });
            DropIndex("dbo.Customers", new[] { "PurchaseOrder_PurchaseOrderId" });
            DropIndex("dbo.Customers", new[] { "Id" });
            DropIndex("dbo.Administrators", new[] { "Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PurchaseOrders", new[] { "PurchaseOrderStatus_PurchaseOrderStatusId" });
            DropIndex("dbo.PurchaseOrders", new[] { "VendorId" });
            DropIndex("dbo.PaymentStatus", new[] { "Payment_PaymentId" });
            DropIndex("dbo.PaymentModes", new[] { "Payment_PaymentId" });
            DropIndex("dbo.Payments", new[] { "PurchaseOrderId" });
            DropIndex("dbo.MaterialSpecifications", new[] { "MaterialId" });
            DropIndex("dbo.MaterialInPurchaseOrders", new[] { "MaterialUnit_MaterialUnitId" });
            DropIndex("dbo.MaterialInPurchaseOrders", new[] { "MaterialId" });
            DropIndex("dbo.MaterialInPurchaseOrders", new[] { "PurchaseOrderId" });
            DropIndex("dbo.MaterialInMaterialsRecieveds", new[] { "MaterialsRecievedId" });
            DropIndex("dbo.MaterialInMaterialsRecieveds", new[] { "MaterialId" });
            DropIndex("dbo.MaterialInJobs", new[] { "JobId" });
            DropIndex("dbo.JobStatus", new[] { "Job_JobId" });
            DropIndex("dbo.Jobs", new[] { "Material_MaterialId" });
            DropIndex("dbo.Jobs", new[] { "EmployeeId" });
            DropIndex("dbo.ProductInWholesaleOrders", new[] { "WholesaleOrderId" });
            DropIndex("dbo.WholesaleOrders", new[] { "WholesalerId" });
            DropIndex("dbo.WholesaleOrders", new[] { "RetailerId" });
            DropIndex("dbo.ProductInRetailOrders", new[] { "RetailOrderId" });
            DropIndex("dbo.RetailOrders", new[] { "RetailerId" });
            DropIndex("dbo.RetailOrders", new[] { "CustomerId" });
            DropIndex("dbo.ProductInProductStocks", new[] { "ProductStockId" });
            DropIndex("dbo.ProductStocks", new[] { "ApplicationUserId" });
            DropIndex("dbo.SpecificationInProducts", new[] { "ProductId" });
            DropIndex("dbo.ProductInJobs", new[] { "JobId" });
            DropIndex("dbo.ProductInJobs", new[] { "ProductId" });
            DropIndex("dbo.ProductInCustomerOrders", new[] { "ProductId" });
            DropIndex("dbo.MaterialInProducts", new[] { "MaterialId" });
            DropIndex("dbo.MaterialInProducts", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.Wholesaler");
            DropTable("dbo.Retailers");
            DropTable("dbo.Customers");
            DropTable("dbo.Administrators");
            DropTable("dbo.VendorStatus");
            DropTable("dbo.Vendors");
            DropTable("dbo.SalaryPayments");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PurchaseOrderStatus");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.ProductInStores");
            DropTable("dbo.PaymentStatus");
            DropTable("dbo.PaymentModes");
            DropTable("dbo.Payments");
            DropTable("dbo.MaterialUnits");
            DropTable("dbo.MaterialInStores");
            DropTable("dbo.MaterialsRecieveds");
            DropTable("dbo.MaterialSpecifications");
            DropTable("dbo.MaterialInPurchaseOrders");
            DropTable("dbo.Materials");
            DropTable("dbo.MaterialInMaterialsRecieveds");
            DropTable("dbo.MaterialInJobs");
            DropTable("dbo.JobStatus");
            DropTable("dbo.Jobs");
            DropTable("dbo.Employees");
            DropTable("dbo.ProductInWholesaleOrders");
            DropTable("dbo.WholesaleOrders");
            DropTable("dbo.ProductInRetailOrders");
            DropTable("dbo.RetailOrders");
            DropTable("dbo.ProductInProductStocks");
            DropTable("dbo.ProductStocks");
            DropTable("dbo.SpecificationInProducts");
            DropTable("dbo.ProductInJobs");
            DropTable("dbo.ProductInCustomerOrders");
            DropTable("dbo.MaterialInProducts");
            DropTable("dbo.Products");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
        }
    }
}
