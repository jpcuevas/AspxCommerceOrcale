using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;


namespace AspxCommerce.Core
{
   public class AspxBrowseCategoryProvider
    {
       public AspxBrowseCategoryProvider()
       {
       }

       public static List<CategoryDetailsInfo> BindCategoryDetails(int categoryID,int count,int level, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("v_CategoryID", categoryID));
               parameterCollection.Add(new KeyValuePair<string, object>("v_Count", count));
               parameterCollection.Add(new KeyValuePair<string, object>("v_Level", level));
               OracleHandler sqlH = new OracleHandler();
               List<CategoryDetailsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsInfo>("usp_Aspx_GetCategoryDetails", parameterCollection);
               return lstCatDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<CategoryDetailsInfo> GetBrowseByCategorySetting(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               OracleHandler sqlH = new OracleHandler();
               List<CategoryDetailsInfo> lstCatDetail = sqlH.ExecuteAsList<CategoryDetailsInfo>("usp_Aspx_GetBrowseByCategorySe", parameterCollection);
               return lstCatDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void UpdateBrowseByCategorySetting(string settingKeys,string settingValues,AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("SettingKeys", settingKeys));
               parameterCollection.Add(new KeyValuePair<string, object>("SettingValues", settingValues));
               OracleHandler sqlH = new OracleHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_BrowseBySettingUpdate", parameterCollection);

           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
