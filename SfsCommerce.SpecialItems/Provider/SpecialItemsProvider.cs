using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using AspxCommerce.SpecialItems;
using SageFrame.Web.Utilities;

namespace AspxCommerce.SpecialItems
{
    public class SpecialItemsProvider
    {
        public List<SpecialItemsInfo> GetSpecialItems(AspxCommonInfo aspxCommonObj, int count)
        {
            List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            paramCol.Add(new KeyValuePair<string, object>("vcount", count));
            OracleHandler sageSQL = new OracleHandler();
            List<SpecialItemsInfo> slInfo = sageSQL.ExecuteAsList<SpecialItemsInfo>("usp_Aspx_GetSpecialItems", paramCol);
            return slInfo;
        }

        public SpecialItemsSettingInfo GetSpecialItemsSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                SpecialItemsSettingInfo objHDSetting = new SpecialItemsSettingInfo();
                objHDSetting = sqlH.ExecuteAsObject<SpecialItemsSettingInfo>("usp_Aspx_SpecialItemsSettingsG", parameterCollection);
                return objHDSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void SaveAndUpdateSpecialItemsSetting(AspxCommonInfo aspxCommonObj, SpecialItemsSettingKeyPairInfo specialObj)
        {
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("SettingKeys", specialObj.SettingKey));
            parameterCollection.Add(new KeyValuePair<string, object>("SettingValues", specialObj.SettingValue));
            OracleHandler sqlhandle = new OracleHandler();
            sqlhandle.ExecuteNonQuery("usp_Aspx_SpecialItemsSettingsUpdate", parameterCollection);
        }

        public List<RssFeedItemInfo> GetItemRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                var rssFeedContent = new List<RssFeedItemInfo>();
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("Count", count));
                OracleHandler SQLH = new OracleHandler();
                switch (rssOption)
                {
                    case "specialitems":
                        rssFeedContent = SQLH.ExecuteAsList<RssFeedItemInfo>("usp_Aspx_GetRssFeedSpecialItem", Parameter);
                        break;
                }
                return rssFeedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SpecialItemsInfo> GetAllSpecialItems(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
        {
            string spName = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("RowTotal", rowTotal));
                OracleHandler sqlH = new OracleHandler();
                if (sortBy == 1)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByItemIDDesc";
                }
                else if (sortBy == 2)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByItemIDAsc";
                }
                else if (sortBy == 3)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByPriceDesc";
                }
                else if (sortBy == 4)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByPriceAsc";
                }
                else if (sortBy == 5)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByName";
                }
                else if (sortBy == 6)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByViewCount";
                }
                else if (sortBy == 7)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByIsFeatured";
                }
                else if (sortBy == 8)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByIsSpecial";
                }
                else if (sortBy == 9)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortBySoldItem";
                }
                else if (sortBy == 10)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByDiscount";
                }
                else if (sortBy == 11)
                {
                    spName = "usp_Aspx_GetSpecialItemDetailsSortByRatedValue";
                }
                List<SpecialItemsInfo> lstCatDetail = sqlH.ExecuteAsList<SpecialItemsInfo>(spName, parameterCollection);
                return lstCatDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
