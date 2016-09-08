using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.ServiceItem
{
    public class ServiceAvailableTime
    {
        public int ServiceDateID
        {
            get;
            set;
        }

        public int ServiceID
        {
            get;
            set;
        }

        public string ServiceTimeFrom
        {
            get;
            set;
        }

        public int ServiceTimeID
        {
            get;
            set;
        }

        public string ServiceTimeTo
        {
            get;
            set;
        }

        public ServiceAvailableTime()
        {
        }
    }
}