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
    public class UserPostsController : ApiController
    {
        // GET: api/UserPosts/5
        public ResponseModel Get(int id)
        {
            IEnumerable<Object> data;
            ResponseModel response = new ResponseModel();
            if ((data = PostManager.getUserPost(id)) != null)
            {
                response.data = data;
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
