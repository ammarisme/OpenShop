namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Quotation : DbMigration
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
                "dbo.Product",
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
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.MaterialInProduct",
                c => new
                    {
                        MaterialInProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Product_ProductId = c.Int(),
                        Material_MaterialId = c.Int(),
                    })
                .PrimaryKey(t => t.MaterialInProductId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .ForeignKey("dbo.Material", t => t.Material_MaterialId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Material_MaterialId);
            
            CreateTable(
                "dbo.ProductInCustomerOrder",
                c => new
                    {
                        ProductsInCustomerOrderId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CustomerOrderId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Description = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductsInCustomerOrderId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.ProductInJob",
                c => new
                    {
                        ProductInJobId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Remark = c.String(),
                        Product_ProductId = c.Int(),
                        Job_JobId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInJobId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .ForeignKey("dbo.Job", t => t.Job_JobId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Job_JobId);
            
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
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInRetailOrderId)
                .ForeignKey("dbo.RetailOrders", t => t.RetailOrderId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .Index(t => t.RetailOrderId)
                .Index(t => t.Product_ProductId);
            
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
                "dbo.ProductStock",
                c => new
                    {
                        ProductStockId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(),
                        RecievedDate = c.DateTime(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductStockId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.ProductInProductStock",
                c => new
                    {
                        ProductInProductStockId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductStockId = c.Int(nullable: false),
                        QuantityRecieved = c.Int(),
                        QuantityDispatched = c.Int(),
                        Cost = c.Int(),
                        Remarks = c.String(),
                        ProductStock_ProductStockId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInProductStockId)
                .ForeignKey("dbo.ProductStock", t => t.ProductStock_ProductStockId)
                .Index(t => t.ProductStock_ProductStockId);
            
            CreateTable(
                "dbo.Quotations",
                c => new
                    {
                        QuotationId = c.Int(nullable: false, identity: true),
                        RetailerId = c.String(maxLength: 128),
                        WholesalerId = c.String(maxLength: 128),
                        Status = c.String(),
                        PaymentMethod = c.String(),
                    })
                .PrimaryKey(t => t.QuotationId)
                .ForeignKey("dbo.Retailers", t => t.RetailerId)
                .ForeignKey("dbo.Wholesaler", t => t.WholesalerId)
                .Index(t => t.RetailerId)
                .Index(t => t.WholesalerId);
            
            CreateTable(
                "dbo.ProductsInQuotations",
                c => new
                    {
                        ProductInQuotationId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        QuotationId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Quotation_QuotationId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInQuotationId)
                .ForeignKey("dbo.Quotations", t => t.Quotation_QuotationId)
                .Index(t => t.Quotation_QuotationId);
            
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
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductInWholesaleOrderId)
                .ForeignKey("dbo.WholesaleOrders", t => t.WholesaleOrderId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .Index(t => t.WholesaleOrderId)
                .Index(t => t.Product_ProductId);
            
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
                "dbo.Employee",
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
                "dbo.Job",
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
                        Employee_EmployeeId = c.Int(),
                        Material_MaterialId = c.Int(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Employee", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.Material", t => t.Material_MaterialId)
                .Index(t => t.Employee_EmployeeId)
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
                .ForeignKey("dbo.Job", t => t.Job_JobId)
                .Index(t => t.Job_JobId);
            
            CreateTable(
                "dbo.MaterialInJob",
                c => new
                    {
                        MaterialInJobId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Remark = c.String(),
                        Damaged = c.Boolean(),
                        Job_JobId = c.Int(),
                    })
                .PrimaryKey(t => t.MaterialInJobId)
                .ForeignKey("dbo.Job", t => t.Job_JobId)
                .Index(t => t.Job_JobId);
            
            CreateTable(
                "dbo.MaterialInMaterialsRecieved",
                c => new
                    {
                        MaterialInMaterialsRecievedId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        MaterialsRecievedId = c.Int(nullable: false),
                        QuantityRecieved = c.Int(nullable: false),
                        QuantityDispatched = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        Remarks = c.String(),
                        Material_MaterialId = c.Int(),
                        MaterialsRecieved_MaterialsRecievedId = c.Int(),
                    })
                .PrimaryKey(t => t.MaterialInMaterialsRecievedId)
                .ForeignKey("dbo.Material", t => t.Material_MaterialId)
                .ForeignKey("dbo.MaterialsRecieved", t => t.MaterialsRecieved_MaterialsRecievedId)
                .Index(t => t.Material_MaterialId)
                .Index(t => t.MaterialsRecieved_MaterialsRecievedId);
            
            CreateTable(
                "dbo.Material",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(),
                        Cost = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId);
            
            CreateTable(
                "dbo.MaterialInPurchaseOrder",
                c => new
                    {
                        MaterialInPurchaseOrderId = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        UnitCost = c.Single(nullable: false),
                        Description = c.String(),
                        Material_MaterialId = c.Int(),
                        MaterialUnit_MaterialUnitId = c.Int(),
                        PurchaseOrder_PurchaseOrderId = c.Int(),
                    })
                .PrimaryKey(t => t.MaterialInPurchaseOrderId)
                .ForeignKey("dbo.Material", t => t.Material_MaterialId)
                .ForeignKey("dbo.MaterialUnit", t => t.MaterialUnit_MaterialUnitId)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrder_PurchaseOrderId)
                .Index(t => t.Material_MaterialId)
                .Index(t => t.MaterialUnit_MaterialUnitId)
                .Index(t => t.PurchaseOrder_PurchaseOrderId);
            
            CreateTable(
                "dbo.MaterialSpecification",
                c => new
                    {
                        MaterialSpecificationId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        Specification = c.String(),
                        Value = c.String(),
                        Material_MaterialId = c.Int(),
                    })
                .PrimaryKey(t => t.MaterialSpecificationId)
                .ForeignKey("dbo.Material", t => t.Material_MaterialId)
                .Index(t => t.Material_MaterialId);
            
            CreateTable(
                "dbo.MaterialsRecieved",
                c => new
                    {
                        MaterialsRecievedId = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(),
                        RecievedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MaterialsRecievedId);
            
            CreateTable(
                "dbo.MaterialInStore",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialUnit",
                c => new
                    {
                        MaterialUnitId = c.Int(nullable: false, identity: true),
                        UnitName = c.String(),
                    })
                .PrimaryKey(t => t.MaterialUnitId);
            
            CreateTable(
                "dbo.Payment",
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
                        PurchaseOrder_PurchaseOrderId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrder_PurchaseOrderId)
                .Index(t => t.PurchaseOrder_PurchaseOrderId);
            
            CreateTable(
                "dbo.PaymentMode",
                c => new
                    {
                        PaymentModeId = c.Int(nullable: false, identity: true),
                        PaymentModeName = c.String(),
                        Payment_PaymentId = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentModeId)
                .ForeignKey("dbo.Payment", t => t.Payment_PaymentId)
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
                .ForeignKey("dbo.Payment", t => t.Payment_PaymentId)
                .Index(t => t.Payment_PaymentId);
            
            CreateTable(
                "dbo.ProductInStore",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(),
                        Status = c.String(),
                        PurchaseOrderStatus_PurchaseOrderStatusId = c.Int(),
                        Vendor_VendorId = c.Int(),
                    })
                .PrimaryKey(t => t.PurchaseOrderId)
                .ForeignKey("dbo.PurchaseOrderStatus", t => t.PurchaseOrderStatus_PurchaseOrderStatusId)
                .ForeignKey("dbo.Vendor", t => t.Vendor_VendorId)
                .Index(t => t.PurchaseOrderStatus_PurchaseOrderStatusId)
                .Index(t => t.Vendor_VendorId);
            
            CreateTable(
                "dbo.PurchaseOrderStatus",
                c => new
                    {
                        PurchaseOrderStatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.PurchaseOrderStatusId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SalaryPayment",
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
                "dbo.Vendor",
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
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
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
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrder_PurchaseOrderId)
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
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
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
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wholesaler", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Retailers", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Customers", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.Customers", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Administrators", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.PurchaseOrder", "Vendor_VendorId", "dbo.Vendor");
            DropForeignKey("dbo.ProductStock", "ApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Product", "ApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.PurchaseOrder", "PurchaseOrderStatus_PurchaseOrderStatusId", "dbo.PurchaseOrderStatus");
            DropForeignKey("dbo.Payment", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.MaterialInPurchaseOrder", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PaymentStatus", "Payment_PaymentId", "dbo.Payment");
            DropForeignKey("dbo.PaymentMode", "Payment_PaymentId", "dbo.Payment");
            DropForeignKey("dbo.MaterialInPurchaseOrder", "MaterialUnit_MaterialUnitId", "dbo.MaterialUnit");
            DropForeignKey("dbo.MaterialInMaterialsRecieved", "MaterialsRecieved_MaterialsRecievedId", "dbo.MaterialsRecieved");
            DropForeignKey("dbo.MaterialSpecification", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.MaterialInPurchaseOrder", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.MaterialInProduct", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.MaterialInMaterialsRecieved", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.Job", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.Job", "Employee_EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.ProductInJob", "Job_JobId", "dbo.Job");
            DropForeignKey("dbo.MaterialInJob", "Job_JobId", "dbo.Job");
            DropForeignKey("dbo.JobStatus", "Job_JobId", "dbo.Job");
            DropForeignKey("dbo.SpecificationInProduct", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductInWholesaleOrders", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductInRetailOrders", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductInRetailOrders", "RetailOrderId", "dbo.RetailOrders");
            DropForeignKey("dbo.RetailOrders", "RetailerId", "dbo.Retailers");
            DropForeignKey("dbo.Quotations", "WholesalerId", "dbo.Wholesaler");
            DropForeignKey("dbo.WholesaleOrders", "WholesalerId", "dbo.Wholesaler");
            DropForeignKey("dbo.WholesaleOrders", "RetailerId", "dbo.Retailers");
            DropForeignKey("dbo.ProductInWholesaleOrders", "WholesaleOrderId", "dbo.WholesaleOrders");
            DropForeignKey("dbo.Quotations", "RetailerId", "dbo.Retailers");
            DropForeignKey("dbo.ProductsInQuotations", "Quotation_QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.RetailOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ProductInProductStock", "ProductStock_ProductStockId", "dbo.ProductStock");
            DropForeignKey("dbo.ProductInJob", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductInCustomerOrder", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.MaterialInProduct", "Product_ProductId", "dbo.Product");
            DropIndex("dbo.Wholesaler", new[] { "Id" });
            DropIndex("dbo.Retailers", new[] { "Id" });
            DropIndex("dbo.Customers", new[] { "PurchaseOrder_PurchaseOrderId" });
            DropIndex("dbo.Customers", new[] { "Id" });
            DropIndex("dbo.Administrators", new[] { "Id" });
            DropIndex("dbo.PurchaseOrder", new[] { "Vendor_VendorId" });
            DropIndex("dbo.PurchaseOrder", new[] { "PurchaseOrderStatus_PurchaseOrderStatusId" });
            DropIndex("dbo.PaymentStatus", new[] { "Payment_PaymentId" });
            DropIndex("dbo.PaymentMode", new[] { "Payment_PaymentId" });
            DropIndex("dbo.Payment", new[] { "PurchaseOrder_PurchaseOrderId" });
            DropIndex("dbo.MaterialSpecification", new[] { "Material_MaterialId" });
            DropIndex("dbo.MaterialInPurchaseOrder", new[] { "PurchaseOrder_PurchaseOrderId" });
            DropIndex("dbo.MaterialInPurchaseOrder", new[] { "MaterialUnit_MaterialUnitId" });
            DropIndex("dbo.MaterialInPurchaseOrder", new[] { "Material_MaterialId" });
            DropIndex("dbo.MaterialInMaterialsRecieved", new[] { "MaterialsRecieved_MaterialsRecievedId" });
            DropIndex("dbo.MaterialInMaterialsRecieved", new[] { "Material_MaterialId" });
            DropIndex("dbo.MaterialInJob", new[] { "Job_JobId" });
            DropIndex("dbo.JobStatus", new[] { "Job_JobId" });
            DropIndex("dbo.Job", new[] { "Material_MaterialId" });
            DropIndex("dbo.Job", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.SpecificationInProduct", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductInWholesaleOrders", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductInWholesaleOrders", new[] { "WholesaleOrderId" });
            DropIndex("dbo.WholesaleOrders", new[] { "WholesalerId" });
            DropIndex("dbo.WholesaleOrders", new[] { "RetailerId" });
            DropIndex("dbo.ProductsInQuotations", new[] { "Quotation_QuotationId" });
            DropIndex("dbo.Quotations", new[] { "WholesalerId" });
            DropIndex("dbo.Quotations", new[] { "RetailerId" });
            DropIndex("dbo.ProductInProductStock", new[] { "ProductStock_ProductStockId" });
            DropIndex("dbo.ProductStock", new[] { "ApplicationUserId" });
            DropIndex("dbo.RetailOrders", new[] { "RetailerId" });
            DropIndex("dbo.RetailOrders", new[] { "CustomerId" });
            DropIndex("dbo.ProductInRetailOrders", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductInRetailOrders", new[] { "RetailOrderId" });
            DropIndex("dbo.ProductInJob", new[] { "Job_JobId" });
            DropIndex("dbo.ProductInJob", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductInCustomerOrder", new[] { "Product_ProductId" });
            DropIndex("dbo.MaterialInProduct", new[] { "Material_MaterialId" });
            DropIndex("dbo.MaterialInProduct", new[] { "Product_ProductId" });
            DropIndex("dbo.Product", new[] { "ApplicationUserId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Wholesaler");
            DropTable("dbo.Retailers");
            DropTable("dbo.Customers");
            DropTable("dbo.Administrators");
            DropTable("dbo.VendorStatus");
            DropTable("dbo.Vendor");
            DropTable("dbo.SalaryPayment");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.PurchaseOrderStatus");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.ProductInStore");
            DropTable("dbo.PaymentStatus");
            DropTable("dbo.PaymentMode");
            DropTable("dbo.Payment");
            DropTable("dbo.MaterialUnit");
            DropTable("dbo.MaterialInStore");
            DropTable("dbo.MaterialsRecieved");
            DropTable("dbo.MaterialSpecification");
            DropTable("dbo.MaterialInPurchaseOrder");
            DropTable("dbo.Material");
            DropTable("dbo.MaterialInMaterialsRecieved");
            DropTable("dbo.MaterialInJob");
            DropTable("dbo.JobStatus");
            DropTable("dbo.Job");
            DropTable("dbo.Employee");
            DropTable("dbo.SpecificationInProduct");
            DropTable("dbo.ProductInWholesaleOrders");
            DropTable("dbo.WholesaleOrders");
            DropTable("dbo.ProductsInQuotations");
            DropTable("dbo.Quotations");
            DropTable("dbo.ProductInProductStock");
            DropTable("dbo.ProductStock");
            DropTable("dbo.RetailOrders");
            DropTable("dbo.ProductInRetailOrders");
            DropTable("dbo.ProductInJob");
            DropTable("dbo.ProductInCustomerOrder");
            DropTable("dbo.MaterialInProduct");
            DropTable("dbo.Product");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
        }
    }
}
