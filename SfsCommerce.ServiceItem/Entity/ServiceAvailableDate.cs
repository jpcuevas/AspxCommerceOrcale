using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.ServiceItem
{
    public class ServiceAvailableDate
    {
        public DateTime? ServiceDateFrom
        {
            get;
            set;
        }

        public int ServiceDateID
        {
            get;
            set;
        }

        public DateTime? ServiceDateTo
        {
            get;
            set;
        }

        public int ServiceID
        {
            get;
            set;
        }

        public ServiceAvailableDate()
        {
        }
    }
}