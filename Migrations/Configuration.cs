namespace WholesaleTradingPortal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WholesaleTradingPortal.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WholesaleTradingPortal.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WholesaleTradingPortal.DAL.ApplicationDbContext db)
        {
            
            db.SaveChanges();
        }
    }
}
