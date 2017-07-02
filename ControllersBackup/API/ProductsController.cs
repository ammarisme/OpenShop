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
using ETrading.Models;
using ETrading.DAL;
using Newtonsoft.Json.Linq;

namespace ETrading.Controllers.API
{
    public class ProductsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        //public List<ProductSpecification> GetProductSpecifications()
        //{
        //    List<ProductSpecification> specifications = new List<ProductSpecification>() { 
        //    new ProductSpecification { Specification="Cut Size", SpecificationValue="1"},
        //    new ProductSpecification{ Specification ="Fin.Size", SpecificationValue="2"},
        //    new ProductSpecification{ Specification ="Filling", SpecificationValue="3"},
        //    new ProductSpecification{ Specification ="Padding Layer", SpecificationValue="4"},
        //    new ProductSpecification{ Specification ="Cover Width", SpecificationValue="5"},
        //    new ProductSpecification{ Specification ="Cross Width", SpecificationValue="6"},
        //    new ProductSpecification{ Specification ="Packing Width", SpecificationValue="7"},
        //    new ProductSpecification{ Specification ="Overlock", SpecificationValue="8"}
            
        //    };
        //    return specifications;
        //}

        public IQueryable<SpecificationInProduct> GetSpecificationsInProduct(int id)
        {
            IQueryable<SpecificationInProduct> productSpecifications = db.SpecificationInProduct.Where(m => m.ProductId== id);
            return productSpecifications;
        }

        public IQueryable<MaterialInProduct> GetMaterialsInProduct(int id)
        {
            IQueryable<MaterialInProduct> materialsInProduct = db.MaterialInProducts.Where(p => p.ProductId == id);
            return materialsInProduct;
        }

        [HttpPost]
        public IHttpActionResult AddProduct(JObject jsonBody)
        {
            JObject specifications = (JObject)jsonBody["SpecificationsInProduct"]; // this variable must be present in the javascript
            JObject materials = (JObject)jsonBody["MaterialsInProduct"]; // this variable must be present in the javascript

            jsonBody.Remove("SpecificationsInProduct");
            jsonBody.Remove("MaterialsInProduct");

            Product product= jsonBody.ToObject<Product>(); // the job card object

            db.Products.Add(product);

            db.SaveChanges(); // save the shit

            int productId = product.ProductId; // the foregin key to be used for the -> proudcts

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)specifications.Children<JToken>();
            
            foreach (JToken token in tokens)
            {
                    JToken specificationJson = token.Children().First();
                    SpecificationInProduct specificationInstance = specificationJson.ToObject<SpecificationInProduct>();
                    specificationInstance.ProductId = productId;
                    db.SpecificationInProduct.Add(specificationInstance);

            }
            JEnumerable<JToken> materialsTokens = (JEnumerable<JToken>)materials.Children<JToken>();
            foreach (JToken token in materialsTokens)
            {
                JToken materialJson = token.Children().First();
                MaterialInProduct materialInstance = materialJson.ToObject<MaterialInProduct>();
                materialInstance.ProductId = productId;
                db.MaterialInProducts.Add(materialInstance);

            }

            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        //GetSpecificationsInProduct
        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}