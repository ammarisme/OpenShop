using Newtonsoft.Json.Linq;
using WholesaleTradingPortal.DAL;
using WholesaleTradingPortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace RetaiEnterprise.Controllers.API
{
    public class Integrator
    {
        public string IntegrationSystemUrl = "http://localhost:51980";

        private ApplicationDbContext db { get; set; }

        public string SystemIdNumber { get; set; }

        public Integrator()
        {
            db = new ApplicationDbContext();

            Setting setting = db.Settings.Find(1);
            SystemIdNumber = setting.SystemIdNumber;
        }

        // send a json object to the route in apiRoute
        // apiRoute must be like /api/{controller}/{action}

        public HttpWebResponse sendJsonObject(JObject jsonBody, string apiRoute)
        {
            // creating the http request
            var http = (HttpWebRequest)WebRequest.Create(new Uri(IntegrationSystemUrl + apiRoute));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            if (jsonBody["ServiceId"] == null)
            {
                jsonBody.Add("ServiceId", SystemIdNumber);
            }

            // parsing the json object
            string parsedContent = jsonBody.ToString();
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent);

            // opeing a new stream and gettin data
            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)http.GetResponse();
            }
            catch (WebException ex)
            {
                Console.Write(ex);
            }
            return response;
        }

        public string getResponseMessage(string apiRoute)
        {
            // creating the http request
            HttpWebRequest request = WebRequest.Create(new Uri(IntegrationSystemUrl + apiRoute)) as HttpWebRequest;

            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Method = "POST";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            WebHeaderCollection header = response.Headers;
            string responseText;

            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();

            }
            return responseText;
        }
        

    }
}