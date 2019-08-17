using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularNewsFeed.Controllers
{
    public class CatgeoryController : ApiController
    {
        // GET: api/Catgeory
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Catgeory
        public void Post([FromBody]string value)
        {
        }
    }
}
