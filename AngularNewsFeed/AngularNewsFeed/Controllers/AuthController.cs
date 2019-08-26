using AngularNewsFeed.Manager;
using AngularNewsFeed.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularNewsFeed.Controllers
{
    public class AuthController : ApiController
    {
        // POST: api/Auth
        public ResponseModel Post([FromBody]User user)
        {
            string userType;
            ResponseModel response = new ResponseModel();
            if ((userType = UserManager.loginUser(user)) != null)
            {
                response.data = userType;
                response.success = true;
            }
            else
            {
                response.success = false;
                response.error = "Internal Server Error";
                response.data = null;
            }
            return response;
        }
    }
}
