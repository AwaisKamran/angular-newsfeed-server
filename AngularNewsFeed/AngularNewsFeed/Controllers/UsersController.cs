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
    public class UsersController : ApiController
    {
        //Register User -  POST: api/Users
        public ResponseModel Post()
        {
            int data;
            ResponseModel response = new ResponseModel();

            User user = new User();
            user.name = HttpContext.Current.Request.Params["name"];
            user.email = HttpContext.Current.Request.Params["email"];
            user.password = HttpContext.Current.Request.Params["password"];
            user.ipAddress = HttpContext.Current.Request.Params["ipAddress"];

            if ((data = UserManager.registerUser(user)) != -1)
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var filePath = HttpContext.Current.Server.MapPath("~/Images/User/" + data + ".jpg");
                    try
                    {
                        var httpPostedFile = HttpContext.Current.Request.Files["image"];
                        httpPostedFile.SaveAs(filePath);

                        UserModel model = new UserModel();
                        model.userId = data;
                        model.userImage = HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port) + "/Images/User/" + data + ".jpg";
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
    }
}
