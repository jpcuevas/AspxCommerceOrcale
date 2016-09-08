using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;

namespace AspxCommerce.LatestItems
{
    public class AspxLatestItemsProvider
    {
        public AspxLatestItemsProvider()
        {
        }

        public List<LatestItemsInfo> GetLatestItemsByCount(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("vCount", count));
                OracleHandler sqlH = new OracleHandler();
                return sqlH.ExecuteAsList<LatestItemsInfo>("usp_Aspx_LatestItemsGetByCount", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LatestItemSettingInfo GetLatestItemSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                LatestItemSettingInfo objLatestSetting = new LatestItemSettingInfo();
                objLatestSetting = sqlH.ExecuteAsObject<LatestItemSettingInfo>("usp_Aspx_LatestItemSettingGet", parameterCollection);
                return objLatestSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void LatestItemSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("SettingKeys", SettingKeys));
                parameterCollection.Add(new KeyValuePair<string, object>("SettingValues", SettingValues));
                OracleHandler sqLH = new OracleHandler();
                sqLH.ExecuteNonQuery("usp_Aspx_LatestItemSettingsUpdate", parameterCollection);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public List<LatestItemRssInfo> GetLatestItemRssContent(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                List<LatestItemRssInfo> rssFeedContent = new List<LatestItemRssInfo>();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                Parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                Parameter.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                Parameter.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
                Parameter.Add(new KeyValuePair<string, object>("Count", count));
                OracleHandler SQLH = new OracleHandler();
                rssFeedContent = SQLH.ExecuteAsList<LatestItemRssInfo>("usp_Aspx_GetRssFeedLatestItem", Parameter);
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
