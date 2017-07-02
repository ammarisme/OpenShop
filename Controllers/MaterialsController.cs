using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETrading.Models;
using ETrading.DAL;

namespace ETrading.Controllers
{
    public class MaterialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); // the Dal handler

        /*Creation of Material with its specifications
         * form datas are loaded at the server
         * a form where the use can declare specifications is binded with a table
         * user fills the material information in another table
         * on click of create material button the material is created over ajax api
         */
        public ActionResult CreateMaterial()
        {
            
            return View("_create_material");
        }
        
        /*
         Generates a view that shows all the materials table,
         * on choice a user selects a material
         *  another table shows the specifications of the material
         *  also the material specifications are shown in a division
         * on choice of specification, there is form where it updates the specification and value
         * on hit of the update button, the updated material is updated over ajax api
         */
        public ActionResult ViewEditMaterials()
        {
            return View("_view_edit_materials");
        }


        public ActionResult ViewMaterialsRecieved()
        {
            return View("_view_materials_recieved");
        }

        
        public ActionResult AddMaterialsRecieved()
        {
            ViewBag.materials = db.Materials.Select(b => new {MaterialId = b.MaterialId , MaterialName = b.MaterialName }).ToList();
            ViewBag.purchaseOrders =  db.PurchaseOrder.Select(p => new { PurchaseOrderId = p.PurchaseOrderId});

            return View("_add_material_recieved");
        }

        //// GET: Materials
        //public ActionResult Index()
        //{
        //    return View(db.Materials.ToList());
        //}
        //// GET: Materials/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Material material = db.Materials.Find(id);
        //    if (material == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(material);
        //}


        //// POST: Materials/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaterialId,MaterialName,Cost")] Material material)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Materials.Add(material);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(material);
        //}

        //// GET: Materials/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Material material = db.Materials.Find(id);
        //    if (material == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(material);
        //}

        //// POST: Materials/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaterialId,MaterialName,Cost")] Material material)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(material).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(material);
        //}

        //// GET: Materials/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Material material = db.Materials.Find(id);
        //    if (material == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(material);
        //}

        //// POST: Materials/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Material material = db.Materials.Find(id);
        //    db.Materials.Remove(material);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
