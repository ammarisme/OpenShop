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
using WholesaleTradingPortal.Models;
using WholesaleTradingPortal.DAL;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web.Hosting;
namespace WholesaleTradingPortal.Controllers.API
{
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        public IQueryable<Product> GetProducts(string ApplicationUserId)
        {
            var Products = db.Products;
            return Products;
        }

        public ProductsController() {

        }

        public IQueryable<SpecificationInProduct> GetSpecificationsInProduct(int id)
        {
            IQueryable<SpecificationInProduct> productSpecifications = db.SpecificationInProduct.Where(m => m.ProductId== id);
            return productSpecifications;
        }

        [HttpPost]
        //[Authorize]
        public HttpResponseMessage AddProduct(JObject jsonBody)
        {

            JObject specifications = (JObject)jsonBody["SpecificationsInProduct"]; // this variable must be present in the javascript

            jsonBody.Remove("SpecificationsInProduct");

            Product product = jsonBody.ToObject<Product>(); // the job card object\

            db.Products.Add(product);

            db.SaveChanges(); // save the shit

            int productId = product.ProductId; // the foregin key to be used for the -> proudcts

            try
            {

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)specifications.Children<JToken>();

            foreach (JToken token in tokens)
            {
                JToken specificationJson = token.Children().First();
                SpecificationInProduct specificationInstance = specificationJson.ToObject<SpecificationInProduct>();
                specificationInstance.ProductId = productId;
                db.SpecificationInProduct.Add(specificationInstance);
                db.SaveChanges();

            }

            }catch(NullReferenceException ex){
                System.Diagnostics.Debug.Write(ex.Data);
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {

                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\products", HostingEnvironment.MapPath(@"\")));

                string newPath = System.IO.Path.Combine(originalDirectory.ToString(), "" + productId + "");
                string oldPath = System.IO.Path.Combine(originalDirectory.ToString(), "temporary");

                
                Directory.Move(oldPath, newPath);

            }
            catch (DirectoryNotFoundException ex)
            {
                System.Diagnostics.Debug.Write(ex.Data);
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            } 

            return this.Request.CreateResponse(HttpStatusCode.Created, "");
        }


        [HttpPost]
        //[Authorize]
        public HttpResponseMessage UpdateProduct(Product product)
        {
            if (!ProductExists(product.ProductId))
            {
                // create it
                JObject jProduct = JObject.FromObject(product);
                AddProduct(jProduct);

                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Product doesn't exist");
            }
            // else
            // get the product
            Product original = db.Products.Find(product.ProductId);

            original.ProductName = product.ProductName;
            original.Price = product.Price;
            original.Unit = product.Unit;
            original.ShortDescription = product.ShortDescription;

            db.Entry(original).State = EntityState.Modified;

            db.SaveChanges(); 


            return this.Request.CreateResponse(HttpStatusCode.OK, original);
        }
        
        /*
         Updating a selected specification in product
         */
        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateProductSpecifications(SpecificationInProduct specification)
        {
            if (!SpecificationInProductExist(specification.SpecificationInProductId))
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
            // else
            SpecificationInProduct original = db.SpecificationInProduct.Find(specification.SpecificationInProductId);

            original.Specification = specification.Specification;
            original.Value = specification.Value;
            db.Entry(original).State = EntityState.Modified;

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

        
        [HttpGet]
        public  IHttpActionResult ProductExistsByName(string name)
        {
            if (db.Products.Count(e => e.ProductName == name) > 0) {
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
                ;
        }

        [HttpGet]
        public  bool SpecificationInProductExist(int id)
        {
            return db.SpecificationInProduct.Count(e => e.SpecificationInProductId== id) > 0;
        }
    }
}