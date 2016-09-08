using AspxCommerce.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SageFrame.Common;

namespace AspxCommerce.MegaCategory
{
    public class MegaCategoryProvider
    {
        public MegaCategoryProvider()
        {
        }

        public List<MegaCategoryViewInfo> GetCategoryMenuList(AspxCommonInfo aspxCommonObj)
        {
            List<MegaCategoryViewInfo> megaCategoryViewInfos = new List<MegaCategoryViewInfo>();
            object[] storeID = new object[] { "CategoryInfo", aspxCommonObj.StoreID, aspxCommonObj.PortalID, "_", aspxCommonObj.CultureName };
            if (!CacheHelper.Get<List<MegaCategoryViewInfo>>(string.Concat(storeID), out megaCategoryViewInfos))
            {
                List<KeyValuePair<string, object>> paramSPC = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                megaCategoryViewInfos = (new OracleHandler()).ExecuteAsList<MegaCategoryViewInfo>("usp_Aspx_GetMegaCategoryMenuAt", paramSPC);
                //usp_Aspx_GetMegaCategoryMenuAttributes
                storeID = new object[] { "CategoryInfo", aspxCommonObj.StoreID, aspxCommonObj.PortalID, "_", aspxCommonObj.CultureName };
                CacheHelper.Add<List<MegaCategoryViewInfo>>(megaCategoryViewInfos, string.Concat(storeID));
            }
            return megaCategoryViewInfos;
        }

        public MegaCategorySettingInfo GetMegaCategorySetting(AspxCommonInfo aspxCommonObj)
        {
            MegaCategorySettingInfo megaCategorySettingInfo;
            try
            {
                List<KeyValuePair<string, object>> paramSPC = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                megaCategorySettingInfo = (new OracleHandler()).ExecuteAsObject<MegaCategorySettingInfo>("usp_Aspx_GetMegaCategorySettin", paramSPC);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return megaCategorySettingInfo;
        }

        public List<MegaCategorySettingInfo> MegaCategorySettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            List<MegaCategorySettingInfo> megaCategorySettingInfos;
            try
            {
                List<KeyValuePair<string, object>> paramSPC = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramSPC.Add(new KeyValuePair<string, object>("SettingKeys", SettingKeys));
                paramSPC.Add(new KeyValuePair<string, object>("SettingValues", SettingValues));
                megaCategorySettingInfos = (new OracleHandler()).ExecuteAsList<MegaCategorySettingInfo>("[dbo].[usp_Aspx_MegaCategorySettingUpdate]", paramSPC);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return megaCategorySettingInfos;
        }
    }
}