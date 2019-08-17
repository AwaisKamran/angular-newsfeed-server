using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularNewsFeed.Model
{
    public class Posts
    {
        public int postId { get; set; }
        public string postTitle { get; set; }
        public string postContent { get; set; }
        public DateTime postCreated { get; set; }
        public int postCategory { get; set; }
        public bool postApproved { get; set; }
    }
}