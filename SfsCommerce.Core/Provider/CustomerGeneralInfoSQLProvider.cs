using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class CustomerGeneralInfoSQLProvider
    {
        public static CustomerGeneralInfo CustomerIDGetByUsername(string userName, int portalID, int storeID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("UserName", userName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("StoreID", storeID));
                OracleHandler sqlH = new OracleHandler();
                CustomerGeneralInfo sageCustInfo = sqlH.ExecuteAsObject<CustomerGeneralInfo>("usp_Aspx_CustomerIDGetByUserna", ParaMeterCollection);
                return sageCustInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
