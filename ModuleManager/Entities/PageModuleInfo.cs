using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.ModuleManager
{
    public class PageModuleInfo
    {
        public int PageModuleID { get; set; }
        public int UserModuleID { get; set; }
        public string PaneName { get; set; }
        public int ModuleOrder { get; set; }
        public PageModuleInfo() { }
    }
}