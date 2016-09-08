using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.UserAgent
{
    public class UserAgentInfo
    {
        public int PortalID { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; }
        public bool IsActive { get; set; }
        public UserAgentInfo() { }
    }
}