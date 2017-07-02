using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WholesaleTradingPortal.Controllers.API
{

    public class SystemController : ApiController
    {
        // GET: api/System
        public HttpResponseMessage GetStatus()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }

        // GET: api/System/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/System
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/System/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/System/5
        public void Delete(int id)
        {
        }
    }
}
