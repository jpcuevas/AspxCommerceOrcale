using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Core
{
    public partial class AppErazer : System.Web.UI.Page
    {
        public static void ClearSysHash(string key)
        {
            Hashtable sysHst = Globals.sysHst;
            if (sysHst[key] != null)
            {
                sysHst.Remove(key);
            }
        }

        public static void ClearSysCache()
        {
            foreach (System.Collections.DictionaryEntry item in HttpRuntime.Cache)
            {
                if (!item.Key.ToString().Contains("."))
                {
                    string key = item.Key.ToString();
                    HttpRuntime.Cache.Remove(key);
                }
            }
        }
    }
}