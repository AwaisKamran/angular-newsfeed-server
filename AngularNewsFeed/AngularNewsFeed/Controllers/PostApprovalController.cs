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
    public class PostApprovalController : ApiController
    {
        // GET: api/PostApproval
        public ResponseModel Get()
        {
            IEnumerable<Object> data;
            ResponseModel response = new ResponseModel();
            if ((data = PostManager.getUnApprovedPosts()) != null)
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

        // PUT: api/PostApproval/5
        public ResponseModel Put(int id)
        {
            ResponseModel response = new ResponseModel();
            if (PostManager.approvePost(id))
            {
                response.data = null;
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
