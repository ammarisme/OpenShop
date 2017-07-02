using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WholesaleTradingPortal.DAL;
using WholesaleTradingPortal.Models;

namespace WholesaleTradingPortal.Controllers.API
{
    public class SettingsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Settings
        public Setting GetSettings()
        {
            Setting setting = db.Settings.First(s => s.SystemIdNumber != null);
            return setting;
        }

        
        // POST: api/Settings
        [HttpPost]
        public IHttpActionResult UpdateSettings(Setting setting)
        {

            // if setting doesn't exist, create one.
            if (db.Settings.Where(s => s.Id == 1).Count() < 0)
            {
                Setting init = new Setting { SystemIdNumber ="1" , Logo="", Name="",ReportHead=""};
                db.Settings.Add(init);
                db.SaveChanges();
            }
            // now go ahead and update the existing setting
            Setting originalSettings = db.Settings.Where(s => s.Id == 1).First();
            originalSettings.SystemIdNumber = setting.SystemIdNumber;
            originalSettings.Logo = setting.Logo;
            originalSettings.Name = setting.Name;
            originalSettings.ReportHead = setting.ReportHead;

            db.Entry(originalSettings).State = EntityState.Modified;

            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}