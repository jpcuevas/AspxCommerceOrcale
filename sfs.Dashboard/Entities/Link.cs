using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Dashboard
{
    public class Link
    {
        public int PageID { get; set; }
        public string PageName { get; set; }
        public string TabPath { get; set; }

        public Link() { }
    }
}