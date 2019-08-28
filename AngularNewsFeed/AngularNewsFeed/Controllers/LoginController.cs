using AngularNewsFeed.Manager;
using AngularNewsFeed.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AngularNewsFeed.Controllers
{
    public class LoginController : ApiController
    {
        // POST: api/Login
        public ResponseModel Post([FromBody]User user)
        {
            User userResponse;
            ResponseModel response = new ResponseModel();
            if ((userResponse = UserManager.loginUser(user)) != null)
            {
                response.data = userResponse;
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
