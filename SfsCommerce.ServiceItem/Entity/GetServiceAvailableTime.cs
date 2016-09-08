using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.ServiceItem
{
    public class GetServiceAvailableTime
    {
        public int BranchID
        {
            get;
            set;
        }

        public int CategoryID
        {
            get;
            set;
        }

        public int EmployeeID
        {
            get;
            set;
        }

        public int ItemID
        {
            get;
            set;
        }

        public string ServiceDate
        {
            get;
            set;
        }

        public string ServiceDateID
        {
            get;
            set;
        }

        public GetServiceAvailableTime()
        {
        }
    }
}