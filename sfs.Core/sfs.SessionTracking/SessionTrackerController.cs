using SageFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SageFrame.Web
{
    public class SessionTrackerController
    {
        public SessionTrackerController()
        {
        }

        public void SetSessionTrackerValues(string portalID, string userName)
        {
            SessionTracker sessionTracker = (SessionTracker)HttpContext.Current.Session[SessionKeys.Tracker];
            if (string.IsNullOrEmpty(sessionTracker.PortalID))
            {
                sessionTracker.PortalID = portalID;
                sessionTracker.Username = userName;
                SageFrameConfig sfConfig = new SageFrameConfig();
                sessionTracker.InsertSessionTrackerPages = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.InsertSessionTrackingPages);
                SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
                SLog.SessionTrackerUpdateUsername(sessionTracker, userName, portalID);
                HttpContext.Current.Session[SessionKeys.Tracker] = sessionTracker;

            }
        }
    }
}