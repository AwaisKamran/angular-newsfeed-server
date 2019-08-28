using AngularNewsFeed.Manager;
using AngularNewsFeed.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        public ResponseModel Post()
        {
            int data;
            ResponseModel response = new ResponseModel();

            Post post = new Post();
            post.postTitle = HttpContext.Current.Request.Params["postTitle"];
            post.postContent = HttpContext.Current.Request.Params["postContent"];
            post.postCategory = int.Parse(HttpContext.Current.Request.Params["postCategory"]);
            post.postedBy = int.Parse(HttpContext.Current.Request.Params["postedBy"]);
            post.Tags = HttpContext.Current.Request.Params["Tags"];
            post.postSource = HttpContext.Current.Request.Params["postSource"];
            post.OwnerOfSource = HttpContext.Current.Request.Params["OwnerOfSource"];

            if ((data = PostManager.createPost(post)) != -1)
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var filePath = HttpContext.Current.Server.MapPath("~/Images/Post/" + data + ".jpg");
                    try
                    {
                        var httpPostedFile = HttpContext.Current.Request.Files["image"];
                        httpPostedFile.SaveAs(filePath);

                        UserModel model = new UserModel();
                        model.userId = data;
                        model.userImage = HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port) + "/Images/POst/" + data + ".jpg";
                        response.data = model;
                        response.success = true;
                    }
                    catch (System.Exception e)
                    {
                        response.data = null;
                        response.success = false;
                        response.error = "#InternalServerError";
                    }
                }
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
