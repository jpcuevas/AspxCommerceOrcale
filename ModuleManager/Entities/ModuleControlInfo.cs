using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.ModuleControls
{
    public class ModuleControlInfo
    {
        public string UserModuleID { get; set; }
        public string ModuleDefID { get; set; }
        public string ControlTitle { get; set; }
        public string ControlType { get; set; }
        public string ControlSrc { get; set; }

        public ModuleControlInfo() { }
    }
}