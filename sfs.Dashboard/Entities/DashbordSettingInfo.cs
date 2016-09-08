using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Dashboard
{
    public class DashbordSettingInfo
    {
        public int DashboardSettingKeyID { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string UserName { get; set; }
        public int PortalID { get; set; }

        public DashbordSettingInfo() { }

        public DashbordSettingInfo(string _SettingKey, string _UserName, int _PortalID)
        {
            this.SettingKey = _SettingKey;
            this.UserName = _UserName;
            this.PortalID = _PortalID;
        }
    }
}