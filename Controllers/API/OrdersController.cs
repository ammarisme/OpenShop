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
using System.Text;
using System.IO;
using RetaiEnterprise.Controllers.API;

namespace WholesaleTradingPortal.Controllers.API
{
    public class OrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // intergration system url
        //public string IntegrationSystemUrl = "http://localhost:51980";
            
        // GET: api/Sales
        //public IHttpActionResult GetOrders()
        //{
        //    var sales = (
        //        from sale in db.OnlineOrders
        //        join customers in db.Customers on sale.CustomerId equals customers.CustomerId
        //        where customers.CustomerId == sale.CustomerId
        //        select new { 
        //            OrderId = sale.OnlineOrderId,
        //            OrderDate = sale.OrderDate,
        //            OrderDueDate = sale.OrderDueDate,
        //            OrderStatus = sale.OrderStatus,
        //            CustomerFullName  = customers.FirstName + " " + customers.LastName ,
        //            DeliveredDate = sale.DeliveredDate ,
        //            DeliveryStatus = sale.DeliveryStatus,
        //            DeliveryMode = sale.DeliveryMode,
        //            PaymentMethod = sale.PaymentMethod,
        //            PaymentDuration = sale.PaymentDuration ,
        //            Remark = sale.Remark
        //        }
        //        );
        //    return Ok(sales);
        //}

        [HttpPost]
        public HttpResponseMessage ChangeOrderStatus(JObject jsonBody)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                SaleStatus sale = jsonBody.ToObject<SaleStatus>();
                if (db.OnlineOrders.Count(s => s.OnlineOrderId == sale.OrderId) > 0)
                {
                    OnlineOrder originalSale = db.OnlineOrders.Find(sale.OrderId);
                    originalSale.OrderStatus = sale.Status;
                    db.Entry(originalSale).State = EntityState.Modified;
                    db.SaveChanges();

                    // reaching this statement means the db transaction is success
                    dbTransaction.Commit();
                    return this.Request.CreateResponse(HttpStatusCode.OK, "RTP : Change sale status");
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.NoContent, "RTP : No sale");
                }
                }catch(Exception ex){
                    dbTransaction.Rollback();
                    System.Diagnostics.Trace.WriteLine(ex);

                    return this.Request.CreateResponse(HttpStatusCode.Conflict, ex);
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage AddOrder(JObject jsonBody)
        {
            JObject products = (JObject)jsonBody["ProductsInRetailOrder"]; // contains product in the order
            JObject account = (JObject)jsonBody["Account"]; // contains information about  the account who is placin the order
            jsonBody.Remove("ProductsRetailOrder"); // removing these information, will add the later back to send to IS
            jsonBody.Remove("Account"); // same as above
            

            OnlineOrder order= jsonBody.ToObject<OnlineOrder>(); // the online order

            HttpWebResponse response=null;// to store the response from the IS

            // startin the distributed transaction, will be commited only the distributed system transaction is success
            using (var dbTransaction = db.Database.BeginTransaction()) 
                { 
                    try 
                    {
                        // write order
                        db.OnlineOrders.Add(order);

                        db.SaveChanges();

                        // get order id to FK 
                        int OrderId = order.OnlineOrderId; // the foregin key to be used for the -> product in..

                        JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

                        foreach (JToken token in tokens)
                        {
                            JToken productJson = token.Children().First(); // I think this can be changed if changed earlier part of code
                            ProductInOnlineOrder productInstance = productJson.ToObject<ProductInOnlineOrder>();
                            productInstance.OnlineOrderId = OrderId;
                            db.ProductsInOnlineOrders.Add(productInstance);

                            // update the stock
                            Product dbProduct = db.Products.Find(productInstance.ProductId);
                            float newQuantity = (float)dbProduct.StocksQuantity;
                            newQuantity = newQuantity - productInstance.Quantity;
                            if (newQuantity < 0)
                            {
                                dbTransaction.Rollback();
                                return this.Request.CreateResponse(HttpStatusCode.Conflict, "Stock not sufficient.");
                            }
                            else
                            {
                                dbProduct.StocksQuantity = newQuantity;
                                db.Entry(dbProduct).State = EntityState.Modified;
                            }
                        }

                        Integrator integrator = new Integrator();

                        // add routing information to the data and send it to the IS
                        Setting setting = db.Settings.Find(1);
                        jsonBody.Add("ServiceId", setting.SystemIdNumber);
                        //jsonBody.Add("EnterpriseId", jsonBody["EnterpriseId"].ToString());
                        //jsonBody.Add("AccountId", jsonBody["AccountId"].ToString());

                        // add payload information as promised earlier
                        jsonBody.Add("Account", account);
                        jsonBody.Add("ServiceOrderId", OrderId);
                        //jsonBody.Add("ProductsInRetailOrder", account);

                        response = integrator.sendJsonObject(jsonBody, "/api/Enterprises/AddRetailOrder");

                        db.SaveChanges();
                        // commit if distributed transaction is success
                        if (response !=null && response.StatusCode != HttpStatusCode.Conflict)
                        {
                            dbTransaction.Commit();
                        }
                        else
                        {
                            dbTransaction.Rollback();
                        }
                    } 
                    catch (Exception ex) 
                    { 
                        dbTransaction.Rollback();
                        System.Diagnostics.Trace.WriteLine(ex);
                        return this.Request.CreateResponse(HttpStatusCode.Conflict, response);
                    } 
                } 

            return this.Request.CreateResponse(HttpStatusCode.Created, response);
        }


        [HttpPost]
        public HttpResponseMessage AddOnlineOrder(JObject jsonBody)
        {
            JObject products = (JObject)jsonBody["ProductsWholesaleOrder"]; // this variable must be present in the javascript

            jsonBody.Remove("ProductsWholesaleOrder");

            OnlineOrder wholesaleOrder = jsonBody.ToObject<OnlineOrder>(); // the job card object

            //wholesaleOrder.CustomerId = User.Identity.GetUserId();

            db.OnlineOrders.Add(wholesaleOrder);

            db.SaveChanges();

            int retailOrderId = wholesaleOrder.OnlineOrderId; // the foregin key to be used for the -> proudcts

            JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

            foreach (JToken token in tokens)
            {
                JToken productJson = token.Children().First();
                ProductInOnlineOrder productInstance = productJson.ToObject<ProductInOnlineOrder>();
                productInstance.OnlineOrderId = retailOrderId;
                db.ProductsInOnlineOrders.Add(productInstance);
            }

            db.SaveChanges();
            return this.Request.CreateResponse(HttpStatusCode.Created, retailOrderId);
        }

        // GET: api/Sales/5
        [ResponseType(typeof(OnlineOrder))]
        public IHttpActionResult GetRetailOrder(int id)
        {
            OnlineOrder retailOrder = db.OnlineOrders.Find(id);
            if (retailOrder == null)
            {
                return NotFound();
            }

            return Ok(retailOrder);
        }

        public IHttpActionResult GetProductsInRetailOrder(int id)
        {
            var products = db.ProductsInOnlineOrders.Where(p => p.OnlineOrderId == id);
            return Ok(products);
        }

        // PUT: api/Sales/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRetailOrder(int id, OnlineOrder retailOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != retailOrder.OnlineOrderId)
            {
                return BadRequest();
            }

            db.Entry(retailOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RetailOrderExists(id))
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

        // POST: api/Sales
        [ResponseType(typeof(OnlineOrder))]
        public IHttpActionResult PostRetailOrder(OnlineOrder retailOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OnlineOrders.Add(retailOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = retailOrder.OnlineOrderId }, retailOrder);
        }

        // DELETE: api/Sales/5
        [ResponseType(typeof(OnlineOrder))]
        public IHttpActionResult DeleteRetailOrder(int id)
        {
            OnlineOrder retailOrder = db.OnlineOrders.Find(id);
            if (retailOrder == null)
            {
                return NotFound();
            }

            db.OnlineOrders.Remove(retailOrder);
            db.SaveChanges();

            return Ok(retailOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RetailOrderExists(int id)
        {
            return db.OnlineOrders.Count(e => e.OnlineOrderId == id) > 0;
        }
    }

    public class SaleStatus
    {
        public int OrderId { get; set; }

        public string Status { get; set; }
    }
}