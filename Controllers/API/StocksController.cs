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
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;

namespace WholesaleTradingPortal.Controllers.API
{
    public class StocksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Stocks
        public IQueryable<Product> GetStocks()
        {
            return db.Products ;
        }

        

        // GET: api/Stocks/5
        [ResponseType(typeof(ProductStocks))]
        public IHttpActionResult GetProductStocks(int id)
        {
            ProductStocks productStocks = db.Stocks.Find(id);
            if (productStocks == null)
            {
                return NotFound();
            }

            return Ok(productStocks);
        }


        /*
         * @purpose - Add Stocks recieved to the database
         * Products, ProductsInStock and ProductStocks tables are manipulated by this function.
         * This function employs NewtonJsoft library to process json data.
         * @parameters
         * -jsonBody    - json Object
         * 
         * @returns - 
         * Http status 201 - if successfully added
         * Http status 304 - if not successful
         */
        [HttpPost]
        public IHttpActionResult AddStocks(JObject jsonBody)
        {

            JObject products = (JObject)jsonBody["ProductsInStocks"]; // this variable must be present in the javascript
            jsonBody.Remove("ProductsInStocks");

            ProductStocks productStocks = jsonBody.ToObject<ProductStocks>(); // the job card object\
            
            

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

            foreach (JToken token in tokens)
            {
                JToken productJson = token.Children().First();

                ProductInProductStocks productInstance = productJson.ToObject<ProductInProductStocks>();
                // get the product and update it
                Product product = db.Products.Find(productInstance.ProductId);
                if (product == null)
                {
                    return NotFound();
                }

                product.StocksQuantity = productInstance.QuantityRecieved + product.StocksQuantity;
                db.Entry(product).State = EntityState.Modified;
            }

            db.SaveChanges();
        
            return StatusCode(HttpStatusCode.Created);
        }

        /*
         * @purpose - Deduct the level of stocks
         * Products, ProductsInStockWasteds and ProductStockWasteds tables are manipulated by this function.
         * This function employs NewtonJsoft library to process json data.
         * @parameters
         * -jsonBody    - json Object
         * 
         * @returns - 
         * Http status 201 - if successfully added
         * Http status 406 - if not successful
         */
        [HttpPost]
        public IHttpActionResult DeductStock(JObject jsonBody)
        {

            JObject products = (JObject)jsonBody["ProductInProductStockWasted"]; // this variable must be present in the javascript

            jsonBody.Remove("ProductInProductStockWasted"); // get rid of extra data

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

            // update each product in the stock
            foreach (JToken token in tokens)
            {
                JToken productJson = token.Children().First();
                ProductInProductStockWasted productInstance = productJson.ToObject<ProductInProductStockWasted>();
                // add a product in wasted stock to the db
                db.ProductInProductStockWasteds.Add(productInstance);

                // decrease quantity in the products table 
                Product product = db.Products.Find(productInstance.ProductId);
                if(product.StocksQuantity - productInstance.Quantity < 0){
                    // as a business rule.. if the enterprise tried to deduct product's quantity to a negative value
                    // let the product stock quantity be zero
                    product.StocksQuantity = 0;
                    db.Entry(product).State = EntityState.Modified;
                }
                else
                {
                    // reduce the stock from products table
                    product.StocksQuantity = product.StocksQuantity - productInstance.Quantity;
                    db.Entry(product).State = EntityState.Modified;
                }
            }

            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductStocksExists(int id)
        {
            return db.Stocks.Count(e => e.ProductStocksId == id) > 0;
        }
    }
}