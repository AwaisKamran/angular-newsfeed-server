using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularNewsFeed.Model
{
    public class User
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string type { get; set; }
        public bool active { get; set; }
    }
}