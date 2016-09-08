using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.ServiceItem
{
    public class ServiceItemRss
    {
        public string AddedOn
        {
            get;
            set;
        }

        public string ImagePath
        {
            get;
            set;
        }

        public int ServiceCategoryID
        {
            get;
            set;
        }

        public string ServiceName
        {
            get;
            set;
        }

        public string ShortDescription
        {
            get;
            set;
        }

        public ServiceItemRss()
        {
        }
    }
}