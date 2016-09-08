using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Dashboard
{
    public class CountUserInfo
    {

        public int AnonymousUser { get; set; }
        public int LoginUser { get; set; }
        public int PageCount { get; set; }
        public int UserCount { get; set; }
        public CountUserInfo() { }
    }
}