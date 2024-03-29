﻿using AngularNewsFeed.Manager;
using AngularNewsFeed.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularNewsFeed.Controllers
{
    public class CategoryPostsController : ApiController
    {
        // GET: api/CategoryPosts
        public ResponseModel Get()
        {
            IEnumerable<Object> data;
            ResponseModel response = new ResponseModel();
            if ((data = PostManager.getCategoryPosts()) != null)
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

        public ResponseModel Post([FromBody]Category category)
        {
            IEnumerable<Object> data;
           ResponseModel response = new ResponseModel();
            if ((data = PostManager.getCategoryByName(category.categoryName)) != null)
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
