﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularNewsFeed.Model
{
    public class UserPost: Post
    {
        public User user { get; set; }
    }
}