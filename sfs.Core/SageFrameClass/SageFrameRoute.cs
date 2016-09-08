using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame
{
    public interface SageFrameRoute : IHttpHandler
    {
        string PagePath { get; set; }
        string PortalSEOName { get; set; }
        string UserModuleID { get; set; }
        string ControlType { get; set; }
        string ControlMode { get; set; }
        string Key { get; set; }
        string Param { get; set; }
    }
}