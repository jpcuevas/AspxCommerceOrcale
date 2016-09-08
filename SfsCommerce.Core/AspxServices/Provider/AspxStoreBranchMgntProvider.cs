using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxStoreBranchMgntProvider
    {
       public AspxStoreBranchMgntProvider()
       {
       }
  
       public static bool CheckBranchNameUniqueness(AspxCommonInfo aspxCommonObj, int storeBranchId, string storeBranchName)
       {
           try
           {
               OracleHandler sqlH = new OracleHandler();
               List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               Parameter.Add(new KeyValuePair<string, object>("StoreBranchID", storeBranchId));
               Parameter.Add(new KeyValuePair<string, object>("StoreBranchName", storeBranchName));
               bool isUnique= sqlH.ExecuteNonQueryAsBool("usp_Aspx_CheckBranchNameUniquness", Parameter, "IsUnique");
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
  
       public static void SaveAndUpdateStorebranch(string branchName, string branchImage, AspxCommonInfo aspxCommonObj, int storeBranchId)
       {
           try
           {
               List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               Parameter.Add(new KeyValuePair<string, object>("BranchName", branchName));
               Parameter.Add(new KeyValuePair<string, object>("BranchImage", branchImage));
               Parameter.Add(new KeyValuePair<string, object>("StoreBranchID", storeBranchId));
               OracleHandler sqlH = new OracleHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateStoreBranch", Parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    
       public static List<BranchDetailsInfo> GetStoreBranchList(int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("offset", offset));
               parameter.Add(new KeyValuePair<string, object>("limit", limit));
               OracleHandler sqlH = new OracleHandler();
               List<BranchDetailsInfo> lstBrDetail= sqlH.ExecuteAsList<BranchDetailsInfo>("usp_Aspx_GetStoreBranches", parameter);
               return lstBrDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteStoreBranches(string storeBranchIds, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               Parameter.Add(new KeyValuePair<string, object>("StoreBranchIDs", storeBranchIds));
               OracleHandler sqlH = new OracleHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteStoreBranches", Parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
