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
using RetailTradingPortal.DAL;
using RetailTradingPortal.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;
using System.Text;
using System.IO;

namespace RetailTradingPortal.Controllers.API
{
    //public class OrdersController : ApiController
    //{
    //    //private ApplicationDbContext db = new ApplicationDbContext();
    //    //// intergration system url
    //    //public string IntegrationSystemUrl = "http://localhost:51980";
            
    //    //// GET: api/Sales
    //    //public IHttpActionResult GetOrders()
    //    //{
    //    //    var sales = (
    //    //        from sale in db.RetailSales
    //    //        join customers in db.Customers on sale.CustomerId equals customers.CustomerId
    //    //        where customers.CustomerId == sale.CustomerId
    //    //        select new { 
    //    //            OrderId = sale.OrderId,
    //    //            OrderDate = sale.OrderDate,
    //    //            OrderDueDate = sale.OrderDueDate,
    //    //            OrderStatus = sale.OrderStatus,
    //    //            CustomerFullName  = customers.FirstName + " " + customers.LastName ,
    //    //            DeliveredDate = sale.DeliveredDate ,
    //    //            DeliveryStatus = sale.DeliveryStatus,
    //    //            DeliveryMode = sale.DeliveryMode,
    //    //            PaymentMethod = sale.PaymentMethod,
    //    //            PaymentDuration = sale.PaymentDuration ,
    //    //            Remark = sale.Remark
    //    //        }
    //    //        );
    //    //    return Ok(sales);
    //    //}

    //    //public class SaleStatus
    //    //{
    //    //    public int OrderId { get; set; }

    //    //    public string Status { get; set; }
    //    //}
    //    //public IHttpActionResult ChangeSaleStatus(JObject jsonBody)
    //    //{

    //    //    SaleStatus sale = jsonBody.ToObject<SaleStatus>();
    //    //    if (db.RetailSales.Count(s => s.OrderId == sale.OrderId) > 0)
    //    //    {
    //    //        RetailSale originalSale = db.RetailSales.Find(sale.OrderId);
    //    //        originalSale.OrderStatus = sale.Status;
    //    //        db.Entry(originalSale).State = EntityState.Modified;
    //    //        db.SaveChanges();
    //    //        return Ok();
    //    //    }
    //    //    else
    //    //    {
    //    //        return StatusCode(HttpStatusCode.NotModified);
    //    //    }
    //    //}

    //    //[HttpPost]
    //    //public HttpResponseMessage AddOrder(JObject jsonBody)
    //    //{
    //    //    JObject products = (JObject)jsonBody["ProductsInRetailOrder"]; // this variable must be present in the javascript

    //    //    jsonBody.Remove("ProductsRetailOrder");

    //    //    OnlineOrder retailOrder= jsonBody.ToObject<OnlineOrder>(); // the job card object

    //    //    retailOrder.CustomerId = 1;

    //    //    db.OnlineOrders.Add(retailOrder);

    //    //    db.SaveChanges(); 

    //    //    int retailOrderId = retailOrder.OnlineOrderId; // the foregin key to be used for the -> proudcts

    //    //    JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

    //    //    foreach (JToken token in tokens)
    //    //    {
    //    //        JToken productJson = token.Children().First();
    //    //        ProductInOnlineOrder productInstance = productJson.ToObject<ProductInOnlineOrder>();
    //    //        productInstance.OnlineOrderId = retailOrderId;
    //    //        db.ProductsInOnlineOrders.Add(productInstance);
    //    //    }

    //    //    db.SaveChanges();

    //    //    var baseAddress = IntegrationSystemUrl + "/api/Enterprises/PlaceOrderToEnterprise";

    //    //    var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
    //    //    http.Accept = "application/json";
    //    //    http.ContentType = "application/json";
    //    //    http.Method = "POST";

    //    //    string parsedContent = jsonBody.ToString();
    //    //    ASCIIEncoding encoding = new ASCIIEncoding();
    //    //    Byte[] bytes = encoding.GetBytes(parsedContent);

    //    //    Stream newStream = http.GetRequestStream();
    //    //    newStream.Write(bytes, 0, bytes.Length);
    //    //    newStream.Close();

    //    //    HttpWebResponse response = (HttpWebResponse) http.GetResponse();

    //    //    return this.Request.CreateResponse(response.StatusCode, response);

    //    //}


    //    //[HttpPost]
    //    //public HttpResponseMessage AddWholesaleSale(JObject jsonBody)
    //    //{
    //    //    JObject products = (JObject)jsonBody["ProductsWholesaleOrder"]; // this variable must be present in the javascript

    //    //    jsonBody.Remove("ProductsWholesaleOrder");

    //    //    WholesaleSale wholesaleOrder = jsonBody.ToObject<WholesaleSale>(); // the job card object

    //    //    wholesaleOrder.AccountId = User.Identity.GetUserId();

    //    //    db.WholesaleSales.Add(wholesaleOrder);

    //    //    db.SaveChanges();

    //    //    int retailOrderId = wholesaleOrder.OrderId; // the foregin key to be used for the -> proudcts

    //    //    JEnumerable<JToken> tokens = (JEnumerable<JToken>)products.Children<JToken>();

    //    //    foreach (JToken token in tokens)
    //    //    {
    //    //        JToken productJson = token.Children().First();
    //    //        ProductInRetailSale productInstance = productJson.ToObject<ProductInRetailSale>();
    //    //        productInstance.RetailSaleId = retailOrderId;
    //    //        db.ProductsInRetailSales.Add(productInstance);
    //    //    }

    //    //    db.SaveChanges();
    //    //    return this.Request.CreateResponse(HttpStatusCode.Created, retailOrderId);
    //    //}

    //    //// GET: api/Sales/5
    //    //[ResponseType(typeof(RetailSale))]
    //    //public IHttpActionResult GetRetailOrder(int id)
    //    //{
    //    //    RetailSale retailOrder = db.RetailSales.Find(id);
    //    //    if (retailOrder == null)
    //    //    {
    //    //        return NotFound();
    //    //    }

    //    //    return Ok(retailOrder);
    //    //}

    //    //public IHttpActionResult GetProductsInRetailOrder(int id)
    //    //{
    //    //    var products = db.ProductsInRetailSales.Where(p => p.RetailSaleId == id);
    //    //    return Ok(products);
    //    //}

    //    //// PUT: api/Sales/5
    //    //[ResponseType(typeof(void))]
    //    //public IHttpActionResult PutRetailOrder(int id, RetailSale retailOrder)
    //    //{
    //    //    if (!ModelState.IsValid)
    //    //    {
    //    //        return BadRequest(ModelState);
    //    //    }

    //    //    if (id != retailOrder.OrderId)
    //    //    {
    //    //        return BadRequest();
    //    //    }

    //    //    db.Entry(retailOrder).State = EntityState.Modified;

    //    //    try
    //    //    {
    //    //        db.SaveChanges();
    //    //    }
    //    //    catch (DbUpdateConcurrencyException)
    //    //    {
    //    //        if (!RetailOrderExists(id))
    //    //        {
    //    //            return NotFound();
    //    //        }
    //    //        else
    //    //        {
    //    //            throw;
    //    //        }
    //    //    }

    //    //    return StatusCode(HttpStatusCode.NoContent);
    //    //}

    //    //// POST: api/Sales
    //    //[ResponseType(typeof(RetailSale))]
    //    //public IHttpActionResult PostRetailOrder(RetailSale retailOrder)
    //    //{
    //    //    if (!ModelState.IsValid)
    //    //    {
    //    //        return BadRequest(ModelState);
    //    //    }

    //    //    db.RetailSales.Add(retailOrder);
    //    //    db.SaveChanges();

    //    //    return CreatedAtRoute("DefaultApi", new { id = retailOrder.OrderId }, retailOrder);
    //    //}

    //    //// DELETE: api/Sales/5
    //    //[ResponseType(typeof(RetailSale))]
    //    //public IHttpActionResult DeleteRetailOrder(int id)
    //    //{
    //    //    RetailSale retailOrder = db.RetailSales.Find(id);
    //    //    if (retailOrder == null)
    //    //    {
    //    //        return NotFound();
    //    //    }

    //    //    db.RetailSales.Remove(retailOrder);
    //    //    db.SaveChanges();

    //    //    return Ok(retailOrder);
    //    //}

    //    //protected override void Dispose(bool disposing)
    //    //{
    //    //    if (disposing)
    //    //    {
    //    //        db.Dispose();
    //    //    }
    //    //    base.Dispose(disposing);
    //    //}

    //    //private bool RetailOrderExists(int id)
    //    //{
    //    //    return db.RetailSales.Count(e => e.OrderId == id) > 0;
    //    //}
    //}
}