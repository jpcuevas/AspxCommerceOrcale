using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.ServiceItem
{
    public class ServiceItemProductInfo
    {
        public int ItemID
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public string Price
        {
            get;
            set;
        }

        public int ServiceDuration
        {
            get;
            set;
        }

        public string SKU
        {
            get;
            set;
        }

        public ServiceItemProductInfo()
        {
        }
    }
}