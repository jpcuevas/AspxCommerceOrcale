using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.Web
{
    public class SageFrameEnums
    {
        public SageFrameEnums()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum ECTDataTypes
        {
            Integer = 1,
            Decimal = 2,
            String = 3
        }

        public enum ErrorType
        {
            Unknown,
            CustomerError,
            MailError,
            OrderError,
            AdministrationArea,
            CommonError,
            ShippingErrror,
            TaxError,
            WCF,
            WebService,
            PageMethod
        }

        public enum ViewPermissionType
        {
            View = 0,
            Edit = 1
        }



        public enum ControlType
        {
            View = 1,
            Edit = 2,
            Setting = 3
        }


    }

    public enum SageMessageType
    {
        Success,
        Error,
        Alert
    }

    public enum SageMessageTitle
    {
        Information,
        Notification,
        Exception
    }

}

namespace SageFrame.Modules.Admin.PortalSettings
{
    public enum SettingType
    {
        SiteAdmin,
        SuperUser
    }
}