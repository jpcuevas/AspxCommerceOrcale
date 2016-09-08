using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;

namespace AspxCommerce.BrandView
{
    public class AspxBrandViewProvider
    {
        public AspxBrandViewProvider()
        { }
        public List<BrandViewInfo> GetAllBrandForSlider(AspxCommonInfo aspxCommonObj, int BrandCount)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                ParaMeter.Add(new KeyValuePair<string, object>("BrandCount", BrandCount));
                OracleHandler sqLH = new OracleHandler();
                return sqLH.ExecuteAsList<BrandViewInfo>("usp_Aspx_GetAllBrandForSlider", ParaMeter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<BrandViewInfo> GetAllBrandForItem(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                OracleHandler sqLH = new OracleHandler();
                List<BrandViewInfo> lstBrand = sqLH.ExecuteAsList<BrandViewInfo>("usp_Aspx_GetAllBrandForItem", ParaMeter);
                return lstBrand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BrandViewInfo> GetAllFeaturedBrand(AspxCommonInfo aspxCommonObj, int Count)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                ParaMeter.Add(new KeyValuePair<string, object>("Counts", Count));
                OracleHandler sqLH = new OracleHandler();
                List<BrandViewInfo> lstBrand = sqLH.ExecuteAsList<BrandViewInfo>("usp_Aspx_GetAllFeaturedBrands", ParaMeter);
                return lstBrand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BrandSettingInfo GetBrandSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                OracleHandler sqlH = new OracleHandler();
                BrandSettingInfo view =
                    sqlH.ExecuteAsObject<BrandSettingInfo>("usp_Aspx_BrandSettingGet", paramCol);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BrandSettingsUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramCol.Add(new KeyValuePair<string, object>("SettingKeys", SettingKeys));
                paramCol.Add(new KeyValuePair<string, object>("SettingValues", SettingValues));
                OracleHandler sqlH = new OracleHandler();                
                    sqlH.ExecuteNonQuery("usp_Aspx_BrandSettingsUpdate", paramCol);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BrandRssInfo> GetBrandRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {                
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("Count", count));
                Parameter.Add(new KeyValuePair<string, object>("RssOption", rssOption));
                OracleHandler SQLH = new OracleHandler();
                List<BrandRssInfo> rssFeedContent = SQLH.ExecuteAsList<BrandRssInfo>("usp_Aspx_GetRssFeedBrand", Parameter);
                return rssFeedContent;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
