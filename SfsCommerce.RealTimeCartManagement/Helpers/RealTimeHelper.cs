using AspxCommerce.AdminNotification;
using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AspxCommerce.RealTimeCartManagement
{
    public class RealTimeHelper
    {
        public RealTimeHelper()
        {
        }

        public static void UpdateAdminNotifications(int StoreID, int PortalID)
        {
            try
            {
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.StoreID = StoreID;
                aspxCommonObj.PortalID = PortalID;
                NotificationGetAllInfo listInfo = AdminNotificationController.NotificationGetAll(aspxCommonObj.StoreID, aspxCommonObj.PortalID);
                IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<RealTimeHub>();
                hubContext.Clients.All.NotificationGetAllSuccess(listInfo);

            }
            catch (Exception)
            {
                //TODO
            }
        }

    }
}