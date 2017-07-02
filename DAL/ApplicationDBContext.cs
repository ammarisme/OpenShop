using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using WholesaleTradingPortal.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WholesaleTradingPortal.DAL
    /*This is the Data Access Layer
     *Database is accessed through this class
     */
{
    /*This is the class that is used to access the database. 
     * Instantiate this object and access variables as accessing database tables.
     * 
     * Database Name - WholesaleTradingPortal1
     * Database Service - SQLExpress
     * Intergrated Security  - True
     */
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Data Source=.\\SQLEXPRESS;Initial Catalog=WholesaleTradingPortal1;Integrated Security=True", throwIfV1Schema: false)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //  Removing Entity Framework default conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PrimaryKeyNameForeignKeyDiscoveryConvention>();

            // Configuring Identity framework tables
            modelBuilder.Entity<ApplicationUser>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(e => e.UserId);
            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(e => e.UserId);
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        //These tables store System access account information.
        public DbSet<Account> Accounts { get; set; }
        
        // These tables stores Customer and Enterprises resources information
        public DbSet<Enterprise> Enterprises { get; set; }
        
        //These tables store Product and related information
        public DbSet<Product> Products { get; set; }
        public DbSet<SpecificationInProduct> SpecificationInProduct { get; set; }
        
        // These tables store Product Stocks and Products in each Stocks information.
        public DbSet<ProductStocks> Stocks { get; set; }
        public DbSet<ProductInProductStocks> ProductsInProductStocks { get; set; }
        // stock wasted tables. used for stock deduction 
        public DbSet<ProductStockWasted> ProductStockWasteds { get; set; }
        public DbSet<ProductInProductStockWasted> ProductInProductStockWasteds { get; set; }

        // These tables store Quotation Request and Recieved Quotation information
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<ProductInQuotation> ProductsInQuotations { get; set; }

        // To store online orders
        public DbSet<OnlineOrder> OnlineOrders { get; set; }
        public DbSet<ProductInOnlineOrder> ProductsInOnlineOrders { get; set; }

        public DbSet<Setting> Settings { get; set; }
    }
}

/*
         Following are required tables for the integration system
         */
//public DbSet<Enterprise> Enterprises { get; set; }
//public DbSet<EnterpriseService> EnterpriseServices { get; set; }
//public DbSet<EnterpriseAccount> EnterpriseAccounts { get; set; }
//public DbSet<AccountService> AccountServices { get; set; }
//public DbSet<EnterpriseType> EnterpriseTypes { get; set; }
//public DbSet<Service> Services { get; set; }


//// These tables store "Sales" Order information
//public DbSet<RetailSale> RetailSales { get; set; }
//public DbSet<ProductInRetailSale> ProductsInRetailSales { get; set; }
//public DbSet<ProductInRetailSaleReturn> ProductsInRetailSaleReturns { get; set; }

//public DbSet<WholesaleSale> WholesaleSales { get; set; }
//public DbSet<ProductInWholesaleSale> ProductsInWholesaleSales { get; set; }
//public DbSet<ProductInWholesaleSaleReturn> ProductsInWholesaleSaleReturns { get; set; }

//// These tables store "Purchase" Order information.
//public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
//public DbSet<ProductInPurchaseOrder> ProductsInPurchaseOrders { get; set; }
        //public DbSet<Customer> Customers { get; set; }
