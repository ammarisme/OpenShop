using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WholesaleTradingPortal.Models;
using System.Net;
using WholesaleTradingPortal.DAL ;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System.Data.Entity;
using RetaiEnterprise.Controllers.API;


namespace WholesaleTradingPortal.Controllers.API
{
    public class QuotationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Get individual quotation information.
        /// TODO : Authorization
        /// </summary>
        /// <param name="id">th PK</param>
        /// <returns>200, 204</returns>
        public IHttpActionResult GetQuotation(int id)
        {
            Quotation quotation = db.Quotations.Where(q => q.QuotationId == id).Single<Quotation>();
            if (quotation != null)
            {
            return Ok(quotation);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        /// <summary>
        /// GET all the products in a quotation
        /// TODO : Authorization
        /// </summary>
        /// <param name="id">the Quotation Id</param>
        /// <returns>200, 204</returns>
        public IHttpActionResult GetProductsInQuotation(int id)
        {
            var productsInQuote = db.ProductsInQuotations.Where(p => p.QuotationId == id);
            var result = (
                from pq in productsInQuote
                join po in db.Products on pq.ProductId equals po.ProductId
                where po.ProductId == pq.ProductId
                select new { 
                    ProductName = po.ProductName,
                    Quantity = pq.Quantity,
                    UnitPrice = pq.UnitPrice
                }
                );
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
        

        /// <summary>
        /// Requesting a Quotation. 
        /// Save Quotation information in the local database.
        /// Send the object to the IS to route to the Enterprise
        /// TODO : Authorization
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public IHttpActionResult RequestQuotation(JObject jsonBody)
        {
            using (var dbTransaction = db.Database.BeginTransaction()){
                try
                {
                    // Deserializing the json and gettting Quotation object
                    JObject products = (JObject)jsonBody["ProductsInQuotation"]; // this variable must be present in the javascript
                    jsonBody.Remove("ProductsInQuotation");
                    Quotation quotation = jsonBody.ToObject<Quotation>();
                    quotation.Status = "Request";

                    db.Quotations.Add(quotation);
                    db.SaveChanges();
                    int quotationId = quotation.QuotationId; // saving this in a seperate variable to make the code look simple


                    //Deserializing the object and getting Produts in Quotation
                    JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

                    foreach (JToken token in tokens)
                    {
                        JToken productJson = token.Children().First();
                        ProductInQuotation productInstance = productJson.ToObject<ProductInQuotation>();
                        productInstance.QuotationId = quotationId;
                        db.ProductsInQuotations.Add(productInstance);
                    }

                    db.SaveChanges();
                    
                    // lets send this to the IS
                    Integrator integrator = new Integrator();
                    Setting setting = db.Settings.Find(1);

                    //reconstructing the jsonBody to send to the IS
                    jsonBody.Add("ProductsInQuotation", products);
                    // adding routing information. Some of these might be usefull at the Enterprise system too.
                    jsonBody.Add("ServiceId", setting.SystemIdNumber);
                    jsonBody.Add("SellingEnterpriseId", jsonBody["SellingEnterpriseId"]);
                    jsonBody.Add("BuyingEnterpriseId", jsonBody["BuyingEnterpriseId"]);


                    HttpWebResponse response = integrator.sendJsonObject(jsonBody, "/api/Enterprises/RequestQuotation");

                    if (response != null && response.StatusCode != HttpStatusCode.Conflict)
                    {
                        dbTransaction.Commit();
                        return StatusCode(response.StatusCode);
                    }
                    else
                    {
                        dbTransaction.Rollback();
                        return StatusCode(HttpStatusCode.Conflict);
                    }

                }catch (Exception ex){
                    System.Diagnostics.Trace.WriteLine(ex);
                    dbTransaction.Rollback();
                    return StatusCode(HttpStatusCode.Conflict);
                }
            }
        }

        public IHttpActionResult SendQuotation(Quotation quotation)
        {
            // set quotation status to sent
            // update quotation details 
            // update products in quotation

            return StatusCode(HttpStatusCode.OK);
        }

        // user must have logged in
        //public IHttpActionResult GetSentQuotations()
        //{

        //    var quotations = db.Quotations.Where(q => q.Status == "Sent");
        //    var result = (
        //        from quotes in quotations
        //        join suppliers in db.Enterprises on quotes.SupplierId equals suppliers.EnterpriseId
        //        where quotes.SupplierId == suppliers.EnterpriseId
        //        select new
        //        {
        //            QuotationId = quotes.QuotationId,
        //            Supplier = suppliers.EnterpriseName,
        //            Status = quotes.Status,
        //            PaymentMethod = quotes.PaymentMethod,
        //            PaymentDuration = quotes.PaymentDuration,
        //            DeliveryMethod = quotes.DeliveryMethod
        //        }
        //        );

        //    return Ok(result);
        //}

        // user must have logged in
        // TODO : user validation
        //[Authorize]
        //public IHttpActionResult GetRequestedQuotations()
        //{
        //    //string wholesalerId = User.Identity.GetUserId();
        //    //IQueryable<Quotation> quotations = db.Quotations;
        //    //return quotations;
        //    var quotations = db.Quotations.Where(q => q.Status == "Requested");
        //    var result = (
        //        from quotes in quotations
        //        join suppliers in db.Enterprises on quotes.SupplierId equals suppliers.EnterpriseId
        //        where quotes.SupplierId == suppliers.EnterpriseId
        //        select new {
        //            QuotationId = quotes.QuotationId,
        //            Supplier = suppliers.EnterpriseName,
        //            Status = quotes.Status,
        //            PaymentMethod = quotes.PaymentMethod,
        //            PaymentDuration = quotes.PaymentDuration,
        //            DeliveryMethod = quotes.DeliveryMethod
        //        }
        //        );

        //    return Ok(result);
        //}

        //[Authorize]
        //public IHttpActionResult GetReceivedQuotations()
        //{
        //    //string wholesalerId = User.Identity.GetUserId();
        //    //IQueryable<Quotation> quotations = db.Quotations;
        //    //return quotations;
        //    var quotations = db.Quotations.Where(q => q.Status == "Received" || q.Status == "Accepted" || q.Status == "Rejected");
        //    var result = (
        //        from quotes in quotations
        //        join suppliers in db.Enterprises on quotes.SupplierId equals suppliers.EnterpriseId
        //        where quotes.SupplierId == suppliers.EnterpriseId
        //        select new
        //        {
        //            QuotationId = quotes.QuotationId,
        //            Supplier = suppliers.EnterpriseName,
        //            Status = quotes.Status,
        //            PaymentMethod = quotes.PaymentMethod,
        //            PaymentDuration = quotes.PaymentDuration,
        //            DeliveryMethod = quotes.DeliveryMethod
        //        }
        //        );

        //    return Ok(result);
        //}

        
        /*
         * @purpose - this function status of a quotation
         * 
         */
        public class QuotationStatus
        {
            public int QuotationId { get; set; }

            public string Status { get; set; }
        }
        public IHttpActionResult ChangeQuotationStatus(JObject jsonBody)
        {
            var statusCode=HttpStatusCode.OK;

            using (DbContextTransaction scope = db.Database.BeginTransaction())
            {
                QuotationStatus quote = jsonBody.ToObject<QuotationStatus>();
                if (db.Quotations.Count(s => s.QuotationId == quote.QuotationId) > 0)
                {

                    db.Database.ExecuteSqlCommand("SELECT * FROM Quotations WITH (TABLOCKX)");
                    Quotation originalQuotation = db.Quotations.Find(quote.QuotationId);
                    originalQuotation.Status = quote.Status;
                    db.Entry(originalQuotation).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    statusCode = HttpStatusCode.NotModified;
                }    
                scope.Commit();
            }

            return StatusCode(statusCode);
            
        }
                
                    
    }

}