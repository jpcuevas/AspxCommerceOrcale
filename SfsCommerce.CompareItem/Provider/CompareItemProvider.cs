using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.CompareItem
{
    public class CompareItemProvider
    {

        public int SaveCompareItems(SaveCompareItemInfo saveCompareItemObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUS(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ItemID", saveCompareItemObj.ItemID));
                parameter.Add(new KeyValuePair<string, object>("CompareItemID", 0));
                parameter.Add(new KeyValuePair<string, object>("IP", saveCompareItemObj.IP));
                parameter.Add(new KeyValuePair<string, object>("CountryName", saveCompareItemObj.CountryName));
                parameter.Add(new KeyValuePair<string, object>("CostVariantValueIDs", saveCompareItemObj.CostVariantIDs));
                OracleHandler sqlH = new OracleHandler();
                int compareID = sqlH.ExecuteNonQuery("usp_Aspx_AddItemsToCompare", parameter, "CompareAddedItemID");
                return compareID;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ItemsCompareInfo> GetItemCompareList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamNoCID(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                List<ItemsCompareInfo> lstItemCompare = sqlH.ExecuteAsList<ItemsCompareInfo>("usp_Aspx_GetCompareItemsList", parameter);
                return lstItemCompare;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteCompareItem(int compareItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUS(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("CompareItemID", compareItemID));
                OracleHandler sqlH = new OracleHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteCompareItem", parameter);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ClearAll(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUS(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_ClearCompareItems", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckCompareItems(int ID, AspxCommonInfo aspxCommonObj, string costVariantValueIDs)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUS(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ItemID", ID));
                parameter.Add(new KeyValuePair<string, object>("CostVariantValueIDs", costVariantValueIDs));
                OracleHandler sqlH = new OracleHandler();
                string isExist = sqlH.ExecuteNonQueryAsGivenType<string>("usp_Aspx_CheckCompareItems", parameter, "IsExist");
                return Convert.ToBoolean(isExist);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ItemBasicDetailsInfo> GetCompareListImage(string itemIDs, string CostVariantValueIDs, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("ItemIDs", itemIDs));
            parameter.Add(new KeyValuePair<string, object>("CostVariantaValueIDS", CostVariantValueIDs));
            OracleHandler sqlH = new OracleHandler();
            List<ItemBasicDetailsInfo> lstItemBasic = sqlH.ExecuteAsList<ItemBasicDetailsInfo>("usp_Aspx_GetCompareList", parameter);
            return lstItemBasic;
        }

        public ItemsCompareInfo GetItemDetailsForCompare(int ItemID, AspxCommonInfo aspxCommonObj, string costVariantValueIDs)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ItemID", ItemID));
                parameter.Add(new KeyValuePair<string, object>("CostVariantValueIDs", costVariantValueIDs));
                OracleHandler sqlH = new OracleHandler();
                ItemsCompareInfo objItemDetails = sqlH.ExecuteAsObject<ItemsCompareInfo>("usp_Aspx_GetItemDetailsForComp", parameter);
                //ItemsCompareInfo objItemDetails = sqlH.ExecuteAsObject<ItemsCompareInfo>("usp_Aspx_GetItemDetailsForCompare", parameter);
                return objItemDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CompareItemListInfo> GetCompareList(string itemIDs, string CostVariantValueIDs, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("ItemIDs", itemIDs));
            parameter.Add(new KeyValuePair<string, object>("CostVariantValueIDs", CostVariantValueIDs));
            OracleHandler sqlH = new OracleHandler();
            List<CompareItemListInfo> lstCompItem = sqlH.ExecuteAsList<CompareItemListInfo>("usp_Aspx_GetItemCompareList", parameter);
            return lstCompItem;
        }

        #region RecentlyComparedProducts

        public void AddComparedItems(string IDs, string CostVarinatIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ItemIDs", IDs));
                parameter.Add(new KeyValuePair<string, object>("CostVarinatIds", CostVarinatIds));
                OracleHandler sqlH = new OracleHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddComparedItems", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ItemsCompareInfo> GetRecentlyComparedItemList(int count, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("Count", count));
                OracleHandler sqlH = new OracleHandler();
                List<ItemsCompareInfo> lstCompItem = sqlH.ExecuteAsList<ItemsCompareInfo>("usp_Aspx_GetRecentlyComparedItemList", parameter);
                return lstCompItem;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        public CompareItemsSettingInfo GetCompareItemsSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                CompareItemsSettingInfo objItemCompareSetting = new CompareItemsSettingInfo();
                objItemCompareSetting = sqlH.ExecuteAsObject<CompareItemsSettingInfo>("usp_Aspx_CompareItemSettingGet", parameterCollection);
                return objItemCompareSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveAndUpdateCompareItemsSetting(AspxCommonInfo aspxCommonObj, CompareItemsSettingKeyPairInfo compareItems)
        {
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("SettingKeys", compareItems.SettingKey));
            parameterCollection.Add(new KeyValuePair<string, object>("SettingValues", compareItems.SettingValue));
            OracleHandler sqlhandle = new OracleHandler();
            sqlhandle.ExecuteNonQuery("usp_Aspx_CompareItemSettingsUpdate", parameterCollection);
        }
    }
}