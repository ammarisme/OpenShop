namespace RetailEnterprise.DAL
{
    using RetailEnterprise.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseAlways<DatabaseContext>
    {
        // this method is not being invoked . Dont know why.>?????????????????

        protected override void Seed(DatabaseContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var admin = new ApplicationUser {UserName ="admin", Email="ammar@myorder.lk"};
            manager.Create(admin, "123");

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole("admin"));

            manager.AddToRole(admin.Id,"admin");
            //var materials = new List<Material>
            //{
            //    new Material {Cost=120 , MaterialName ="Silk" },
            //    new Material {Cost=123 , MaterialName ="Stick" },
            //    new Material {Cost=123 , MaterialName ="Glue" },
            //    new Material {Cost=14 , MaterialName ="Cotton" },
            //    new Material {Cost=12 , MaterialName ="Charcol" }
            //};
            //materials.ForEach(m => context.Materials.AddOrUpdate(m));

            // adding entries to the database
            //var students = new List<Student>
            //{
            //    new Student { FirstMidName = "Carson",   LastName = "Alexander", 
            //        EnrollmentDate = DateTime.Parse("2010-09-01") },
            //    new Student { FirstMidName = "Meredith", LastName = "Alonso",    
            //        EnrollmentDate = DateTime.Parse("2012-09-01") },
            //    new Student { FirstMidName = "Arturo",   LastName = "Anand",     
            //        EnrollmentDate = DateTime.Parse("2013-09-01") },
            //    new Student { FirstMidName = "Gytis",    LastName = "Barzdukas", 
            //        EnrollmentDate = DateTime.Parse("2012-09-01") },
            //    new Student { FirstMidName = "Yan",      LastName = "Li",        
            //        EnrollmentDate = DateTime.Parse("2012-09-01") },
            //    new Student { FirstMidName = "Peggy",    LastName = "Justice",   
            //        EnrollmentDate = DateTime.Parse("2011-09-01") },
            //    new Student { FirstMidName = "Laura",    LastName = "Norman",    
            //        EnrollmentDate = DateTime.Parse("2013-09-01") },
            //    new Student { FirstMidName = "Nino",     LastName = "Olivetto",  
            //        EnrollmentDate = DateTime.Parse("2005-09-01") }
            //};

            // adding the to the database and saving
            //students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            //context.SaveChanges();

            
            // adding an entry to a database table that was created on model building
            //AddOrUpdateInstructor(context, "Calculus", "Fakhouri");
    // put this method outside
            //void AddOrUpdateInstructor(DatabaseContext context, string courseTitle, string instructorName)
    //    {
    //        var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
    //        var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
    //        if (inst == null)
    //            crs.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
    //    }        
            //context.SaveChanges();

            
            //foreach (Enrollment e in enrollments)
            //{
            //    var enrollmentInDataBase = context.Enrollments.Where(
            //        s =>
            //             s.Student.ID == e.StudentID &&
            //             s.Course.CourseID == e.CourseID).SingleOrDefault();
            //    if (enrollmentInDataBase == null)
            //    {
            //        context.Enrollments.Add(e);
            //    }
            //}
            //context.SaveChanges();
        }
    }
}