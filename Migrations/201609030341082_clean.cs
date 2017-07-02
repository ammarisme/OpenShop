namespace RetailShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clean : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobStatus", "Job_JobId", "dbo.Job");
            DropForeignKey("dbo.MaterialInJob", "Job_JobId", "dbo.Job");
            DropForeignKey("dbo.ProductInJob", "Job_JobId", "dbo.Job");
            DropForeignKey("dbo.Job", "Employee_EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Job", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.MaterialInMaterialsRecieved", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.MaterialInProduct", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.MaterialInPurchaseOrder", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.MaterialSpecification", "Material_MaterialId", "dbo.Material");
            DropForeignKey("dbo.MaterialInMaterialsRecieved", "MaterialsRecieved_MaterialsRecievedId", "dbo.MaterialsRecieved");
            DropForeignKey("dbo.MaterialInPurchaseOrder", "MaterialUnit_MaterialUnitId", "dbo.MaterialUnit");
            DropForeignKey("dbo.PurchaseOrder", "Vendor_VendorId", "dbo.Vendor");
            DropIndex("dbo.MaterialInProduct", new[] { "Material_MaterialId" });
            DropIndex("dbo.ProductInJob", new[] { "Job_JobId" });
            DropIndex("dbo.Job", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Job", new[] { "Material_MaterialId" });
            DropIndex("dbo.JobStatus", new[] { "Job_JobId" });
            DropIndex("dbo.MaterialInJob", new[] { "Job_JobId" });
            DropIndex("dbo.MaterialInMaterialsRecieved", new[] { "Material_MaterialId" });
            DropIndex("dbo.MaterialInMaterialsRecieved", new[] { "MaterialsRecieved_MaterialsRecievedId" });
            DropIndex("dbo.MaterialInPurchaseOrder", new[] { "Material_MaterialId" });
            DropIndex("dbo.MaterialInPurchaseOrder", new[] { "MaterialUnit_MaterialUnitId" });
            DropIndex("dbo.MaterialSpecification", new[] { "Material_MaterialId" });
            DropIndex("dbo.PurchaseOrder", new[] { "Vendor_VendorId" });
            DropColumn("dbo.MaterialInProduct", "Material_MaterialId");
            DropColumn("dbo.ProductInJob", "Job_JobId");
            DropColumn("dbo.MaterialInPurchaseOrder", "Material_MaterialId");
            DropColumn("dbo.MaterialInPurchaseOrder", "MaterialUnit_MaterialUnitId");
            DropColumn("dbo.PurchaseOrder", "Vendor_VendorId");
            DropTable("dbo.Employee");
            DropTable("dbo.Job");
            DropTable("dbo.JobStatus");
            DropTable("dbo.MaterialInJob");
            DropTable("dbo.MaterialInMaterialsRecieved");
            DropTable("dbo.Material");
            DropTable("dbo.MaterialSpecification");
            DropTable("dbo.MaterialsRecieved");
            DropTable("dbo.MaterialInStore");
            DropTable("dbo.MaterialUnit");
            DropTable("dbo.ProductInStore");
            DropTable("dbo.SalaryPayment");
            DropTable("dbo.Vendor");
            DropTable("dbo.VendorStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VendorStatus",
                c => new
                    {
                        VendorStatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.VendorStatusId);
            
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
                "dbo.ProductInStore",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
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
                "dbo.MaterialInStore",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.MaterialSpecification",
                c => new
                    {
                        MaterialSpecificationId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        Specification = c.String(),
                        Value = c.String(),
                        Material_MaterialId = c.Int(),
                    })
                .PrimaryKey(t => t.MaterialSpecificationId);
            
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
                .PrimaryKey(t => t.MaterialInMaterialsRecievedId);
            
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
                .PrimaryKey(t => t.MaterialInJobId);
            
            CreateTable(
                "dbo.JobStatus",
                c => new
                    {
                        JobStatusId = c.Int(nullable: false, identity: true),
                        JobStatusName = c.String(),
                        Job_JobId = c.Int(),
                    })
                .PrimaryKey(t => t.JobStatusId);
            
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
                .PrimaryKey(t => t.JobId);
            
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
            
            AddColumn("dbo.PurchaseOrder", "Vendor_VendorId", c => c.Int());
            AddColumn("dbo.MaterialInPurchaseOrder", "MaterialUnit_MaterialUnitId", c => c.Int());
            AddColumn("dbo.MaterialInPurchaseOrder", "Material_MaterialId", c => c.Int());
            AddColumn("dbo.ProductInJob", "Job_JobId", c => c.Int());
            AddColumn("dbo.MaterialInProduct", "Material_MaterialId", c => c.Int());
            CreateIndex("dbo.PurchaseOrder", "Vendor_VendorId");
            CreateIndex("dbo.MaterialSpecification", "Material_MaterialId");
            CreateIndex("dbo.MaterialInPurchaseOrder", "MaterialUnit_MaterialUnitId");
            CreateIndex("dbo.MaterialInPurchaseOrder", "Material_MaterialId");
            CreateIndex("dbo.MaterialInMaterialsRecieved", "MaterialsRecieved_MaterialsRecievedId");
            CreateIndex("dbo.MaterialInMaterialsRecieved", "Material_MaterialId");
            CreateIndex("dbo.MaterialInJob", "Job_JobId");
            CreateIndex("dbo.JobStatus", "Job_JobId");
            CreateIndex("dbo.Job", "Material_MaterialId");
            CreateIndex("dbo.Job", "Employee_EmployeeId");
            CreateIndex("dbo.ProductInJob", "Job_JobId");
            CreateIndex("dbo.MaterialInProduct", "Material_MaterialId");
            AddForeignKey("dbo.PurchaseOrder", "Vendor_VendorId", "dbo.Vendor", "VendorId");
            AddForeignKey("dbo.MaterialInPurchaseOrder", "MaterialUnit_MaterialUnitId", "dbo.MaterialUnit", "MaterialUnitId");
            AddForeignKey("dbo.MaterialInMaterialsRecieved", "MaterialsRecieved_MaterialsRecievedId", "dbo.MaterialsRecieved", "MaterialsRecievedId");
            AddForeignKey("dbo.MaterialSpecification", "Material_MaterialId", "dbo.Material", "MaterialId");
            AddForeignKey("dbo.MaterialInPurchaseOrder", "Material_MaterialId", "dbo.Material", "MaterialId");
            AddForeignKey("dbo.MaterialInProduct", "Material_MaterialId", "dbo.Material", "MaterialId");
            AddForeignKey("dbo.MaterialInMaterialsRecieved", "Material_MaterialId", "dbo.Material", "MaterialId");
            AddForeignKey("dbo.Job", "Material_MaterialId", "dbo.Material", "MaterialId");
            AddForeignKey("dbo.Job", "Employee_EmployeeId", "dbo.Employee", "EmployeeId");
            AddForeignKey("dbo.ProductInJob", "Job_JobId", "dbo.Job", "JobId");
            AddForeignKey("dbo.MaterialInJob", "Job_JobId", "dbo.Job", "JobId");
            AddForeignKey("dbo.JobStatus", "Job_JobId", "dbo.Job", "JobId");
        }
    }
}
