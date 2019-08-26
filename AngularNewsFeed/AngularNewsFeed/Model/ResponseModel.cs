using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularNewsFeed.Model
{
    public class ResponseModel
    {

        public bool success { get; set; }
        public string error { get; set; }
        public Object data { get; set; }
    }
}