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
    public class PostController : ApiController
    {
        // GET: api/Post
        public ResponseModel Get()
        {
            IEnumerable<Object> data;
            ResponseModel response = new ResponseModel();
            if ((data = PostManager.getPosts()) != null)
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

        // GET: api/Post/5
        public ResponseModel Get(int id)
        {
            Object data;
            ResponseModel response = new ResponseModel();
            if ((data = PostManager.getPost(id)) != null)
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

        // POST: api/Post
        public ResponseModel Post([FromBody]Post post)
        {
            int data;
            ResponseModel response = new ResponseModel();
            if ((data = PostManager.createPost(post)) != -1)
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

        // POST: api/Put
        public ResponseModel Put([FromUri]int id, [FromBody]Post post)
        {
            ResponseModel response = new ResponseModel();
            if (PostManager.updatePost(id,post))
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
