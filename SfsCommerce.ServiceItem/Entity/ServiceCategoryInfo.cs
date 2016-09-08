using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.ServiceItem
{
    public class ServiceCategoryInfo
    {
        public int Count
        {
            get;
            set;
        }

        public int ServiceCategoryID
        {
            get;
            set;
        }

        public string ServiceDetail
        {
            get;
            set;
        }

        public string ServiceImagePath
        {
            get;
            set;
        }

        public string ServiceName
        {
            get;
            set;
        }

        public ServiceCategoryInfo()
        {
        }
    }
}