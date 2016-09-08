using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;

namespace AspxCommerce.Core
{
   public class AspxRatingReviewProvider
    {
       public AspxRatingReviewProvider()
       {
       }

       #region rating/ review
       public static List<ItemRatingAverageInfo> GetItemAverageRating(string itemSKU, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               OracleHandler sqlH = new OracleHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("itemSKU", itemSKU));
               List<ItemRatingAverageInfo> avgRating = sqlH.ExecuteAsList<ItemRatingAverageInfo>("usp_Aspx_ItemRatingGetAverage", parameter);
               return avgRating;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
           
       public static List<RatingCriteriaInfo> GetItemRatingCriteriaByReviewID(AspxCommonInfo aspxCommonObj, int itemReviewID, bool isFlag)
       {
           try
           {
               OracleHandler sqlH = new OracleHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("ItemReviewID", itemReviewID));
               parameter.Add(new KeyValuePair<string, object>("IsFlag", isFlag));
               List<RatingCriteriaInfo> rating = sqlH.ExecuteAsList<RatingCriteriaInfo>("usp_Aspx_GetItemRatingCriteriaForPending", parameter);
               return rating;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void SaveItemRating(ItemReviewBasicInfo ratingSaveObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               OracleHandler sqlH = new OracleHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("RatingCriteriaValue", ratingSaveObj.ItemRatingCriteria));
               parameter.Add(new KeyValuePair<string, object>("StatusID", ratingSaveObj.StatusID));
               parameter.Add(new KeyValuePair<string, object>("ItemReviewID", 0));
               parameter.Add(new KeyValuePair<string, object>("ReviewSummary", ratingSaveObj.ReviewSummary));
               parameter.Add(new KeyValuePair<string, object>("Review", ratingSaveObj.Review));
               parameter.Add(new KeyValuePair<string, object>("ViewFromIP", ratingSaveObj.ViewFromIP));
               parameter.Add(new KeyValuePair<string, object>("ViewFromCountry", ratingSaveObj.viewFromCountry));
               parameter.Add(new KeyValuePair<string, object>("ItemID", ratingSaveObj.ItemID));
               parameter.Add(new KeyValuePair<string, object>("UserName", ratingSaveObj.UserName));
               parameter.Add(new KeyValuePair<string, object>("AddedBy", aspxCommonObj.UserName));
               sqlH.ExecuteNonQuery("usp_Aspx_SaveItemRating", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
      
       public static List<ItemRatingByUserInfo> GetItemRatingPerUser(int offset, int limit, string itemSKU, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               OracleHandler sqlH = new OracleHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("offset", offset));
               parameter.Add(new KeyValuePair<string, object>("vlimit", limit));
               parameter.Add(new KeyValuePair<string, object>("itemSKU", itemSKU));
               //List<ItemRatingByUserInfo> lstItemRating= sqlH.ExecuteAsList<ItemRatingByUserInfo>("usp_Aspx_GetItemAverageRatingByUser", parameter);
               List<ItemRatingByUserInfo> lstItemRating = sqlH.ExecuteAsList<ItemRatingByUserInfo>("usp_Aspx_GetItemAverageRatingB", parameter);
               return lstItemRating;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

   
       public static void DeleteMultipleItemRatings(string itemReviewIDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("ItemReviewIDs", itemReviewIDs));
               OracleHandler sqlH = new OracleHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleSelectionItemRating", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public static List<UserRatingInformationInfo> GetAllUserReviewsAndRatings(int offset, int limit, UserRatingBasicInfo userRatingObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("offset", offset));
               parameter.Add(new KeyValuePair<string, object>("limit", limit));
                parameter.Add(new KeyValuePair<string, object>("UserName", userRatingObj.UserName));
               parameter.Add(new KeyValuePair<string, object>("StatusName", userRatingObj.Status));
               parameter.Add(new KeyValuePair<string, object>("ItemName", userRatingObj.ItemName));
               OracleHandler sqlH = new OracleHandler();
               List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetAllReviewsAndRatings", parameter);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }



       public static List<ItemsReviewInfo> GetAllItemList(string searchText,AspxCommonInfo aspxCommonObj)
       {
           try
           {
               OracleHandler sqlH = new OracleHandler();
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("searchText", searchText));
               List<ItemsReviewInfo> items = sqlH.ExecuteAsList<ItemsReviewInfo>("usp_Aspx_GetAllItemsListReview", parameter);
               return items;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckReviewByUser(int itemID, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("ItemID", itemID));
           OracleHandler sqlH = new OracleHandler();
           //bool isReviewExist= sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckReviewAlreadyExist", parameter, "IsReviewAlreadyExist");
           string isReviewExist = sqlH.ExecuteNonQueryAsGivenType<string>("usp_Aspx_CheckReviewAlreadyExi", parameter, "IsReviewAlreadyExist");
           return Convert.ToBoolean(isReviewExist.ToString());
       }
       
       public static bool CheckReviewByIP(int itemID, AspxCommonInfo aspxCommonObj, string userIP)
       {
           List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("ItemID", itemID));
           parameter.Add(new KeyValuePair<string, object>("UserIP", userIP));
           OracleHandler sqlH = new OracleHandler();
           //bool isReviewExist= sqlH.ExecuteNonQueryAsGivenType<bool>("usp_Aspx_CheckReviewAlreadyExist", parameter, "IsReviewAlreadyExist");
           string isReviewExist = sqlH.ExecuteNonQueryAsGivenType<string>("usp_Aspx_CheckReviewAlreadyEx", parameter, "IsReviewAlreadyExist");
           return Convert.ToBoolean(isReviewExist);
           //return isReviewExist;
       }

       #endregion

       #region Item Rating Criteria Manage/Admin
       public static List<ItemRatingCriteriaInfo> ItemRatingCriteriaManage(int offset, int limit, ItemRatingCriteriaInfo itemCriteriaObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("offset", offset));
               parameter.Add(new KeyValuePair<string, object>("limit", limit));
               parameter.Add(new KeyValuePair<string, object>("RatingCriteria", itemCriteriaObj.ItemRatingCriteria));
               parameter.Add(new KeyValuePair<string, object>("IsActive", itemCriteriaObj.IsActive));
               OracleHandler sqlH = new OracleHandler();
               List<ItemRatingCriteriaInfo> lstRatingCriteria= sqlH.ExecuteAsList<ItemRatingCriteriaInfo>("usp_Aspx_GetAllItemRatingCriteria", parameter);
               return lstRatingCriteria;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
      
       public static void AddUpdateItemCriteria(ItemRatingCriteriaInfo itemCriteriaObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("ID", itemCriteriaObj.ItemRatingCriteriaID));
               parameter.Add(new KeyValuePair<string, object>("Criteria", itemCriteriaObj.ItemRatingCriteria));
               parameter.Add(new KeyValuePair<string, object>("IsActive", itemCriteriaObj.IsActive));
               OracleHandler sqlH = new OracleHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateItemRatingCriteria", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteItemRatingCriteria(string IDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("CriteriaID", IDs));
               OracleHandler sqlH = new OracleHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteItemRatingCriteria", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       #endregion

       #region Rating Reviews Reporting
      
       public static List<CustomerReviewReportsInfo> GetCustomerReviews(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("offset", offset));
               parameter.Add(new KeyValuePair<string, object>("limit", limit));
               OracleHandler sqlH = new OracleHandler();
               List<CustomerReviewReportsInfo> bind = sqlH.ExecuteAsList<CustomerReviewReportsInfo>("usp_Aspx_GetCustomerReviews", parameter);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static List<UserRatingInformationInfo> GetAllCustomerReviewsList(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj, UserRatingBasicInfo customerReviewObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("offset", offset));
               parameter.Add(new KeyValuePair<string, object>("limit", limit));
               parameter.Add(new KeyValuePair<string, object>("User", customerReviewObj.UserName));
               parameter.Add(new KeyValuePair<string, object>("StatusName", customerReviewObj.Status));
               parameter.Add(new KeyValuePair<string, object>("ItemName", customerReviewObj.ItemName));
               OracleHandler sqlH = new OracleHandler();
               List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetCustomerWiseReviewsList", parameter);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static List<UserListInfo> GetUserList(int portalID)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("PortalID", portalID));
               OracleHandler sqlH = new OracleHandler();
               List<UserListInfo> lstUser = sqlH.ExecuteAsList<UserListInfo>("usp_PortalUserListGet", parameter);
               return lstUser;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
      
       public static List<ItemReviewsInfo> GetItemReviews(int offset, System.Nullable<int> limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("offset", offset));
               parameter.Add(new KeyValuePair<string, object>("limit", limit));
               OracleHandler sqlH = new OracleHandler();
               List<ItemReviewsInfo> bind = sqlH.ExecuteAsList<ItemReviewsInfo>("usp_Aspx_GetItemReviewsList", parameter);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       
       public static List<UserRatingInformationInfo> GetAllItemReviewsList(int offset, System.Nullable<int> limit, UserRatingBasicInfo itemReviewObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("offset", offset));
               parameter.Add(new KeyValuePair<string, object>("limit", limit));
               parameter.Add(new KeyValuePair<string, object>("ItemID", itemReviewObj.ItemID));
               parameter.Add(new KeyValuePair<string, object>("UserName", itemReviewObj.UserName));
               parameter.Add(new KeyValuePair<string, object>("StatusName", itemReviewObj.Status));
               parameter.Add(new KeyValuePair<string, object>("ItemName", itemReviewObj.ItemName));
               OracleHandler sqlH = new OracleHandler();
               List<UserRatingInformationInfo> bind = sqlH.ExecuteAsList<UserRatingInformationInfo>("usp_Aspx_GetItemWiseReviewsList", parameter);
               return bind;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       #endregion
    }
}
