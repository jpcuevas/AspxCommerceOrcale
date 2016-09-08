using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.RewardPoints
{
    public class RewardPointsProvider
    {
        public static void RewardPointsSaveGeneralSettings(GeneralSettingsCommonInfo generalSettingobj,
                                                           AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("RewardPoints", generalSettingobj.RewardPoints));
                parameter.Add(new KeyValuePair<string, object>("RewardAmount", generalSettingobj.RewardExchangeRate));
                parameter.Add(new KeyValuePair<string, object>("AddOrderStatusID", generalSettingobj.AddOrderStatusID));
                parameter.Add(new KeyValuePair<string, object>("SubOrderStatusID", generalSettingobj.SubOrderStatusID));
                parameter.Add(new KeyValuePair<string, object>("RewardPointsExpiresInDays",
                                                               generalSettingobj.RewardPointsExpiresInDays));
                parameter.Add(new KeyValuePair<string, object>("MinRedeemBalance", generalSettingobj.MinRedeemBalance));
                parameter.Add(new KeyValuePair<string, object>("BalanceCapped", generalSettingobj.BalanceCapped));
                parameter.Add(new KeyValuePair<string, object>("IsActive", generalSettingobj.IsActive));
                parameter.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("CulturName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
                sqlH.ExecuteNonQuery("usp_Aspx_RewardPointsSaveGeneralSettings", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static List<GeneralSettingInfo> GetGeneralSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("CustomerID", aspxCommonObj.CustomerID));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                OracleHandler sqLH = new OracleHandler();
                //usp_Aspx_RewardPointsGeneralSettingsGetAll
                List<GeneralSettingInfo> lstGeneralSet =
                    sqLH.ExecuteAsList<GeneralSettingInfo>("usp_Aspx_RewardPointsGeneralSe",
                                                           parameterCollection);
                return lstGeneralSet;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static void RewardPointsSaveUpdateNewRule(RewardPointsCommonInfo rewardPointsCommonObj,
                                                         AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("RewardPointSettingsID",
                                                               rewardPointsCommonObj.RewardPointSettingsID));
                parameter.Add(new KeyValuePair<string, object>("RewardRuleName", rewardPointsCommonObj.RewardRuleName));
                parameter.Add(new KeyValuePair<string, object>("RewardRuleID", rewardPointsCommonObj.RewardRuleID));
                parameter.Add(new KeyValuePair<string, object>("RewardRuleType", rewardPointsCommonObj.RewardRuleType));
                parameter.Add(new KeyValuePair<string, object>("RewardPoints", rewardPointsCommonObj.RewardPoints));
                parameter.Add(new KeyValuePair<string, object>("PurchaseAmount", rewardPointsCommonObj.PurchaseAmount));
                parameter.Add(new KeyValuePair<string, object>("IsActive", rewardPointsCommonObj.IsActive));
                parameter.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("CulturName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
                sqlH.ExecuteNonQuery("usp_Aspx_RewardPointsSaveUpdateNewRule", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<RewardPointsSettingInfo> RewardPointsSettingGetAll(int offset, int limit,
                                                                              AspxCommonInfo aspxCommonObj,
                                                                              RewardPointsCommonInfo
                                                                                  rewardPointsCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("Limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("RewardRuleName",
                                                                         rewardPointsCommonObj.RewardRuleName));
                parameterCollection.Add(new KeyValuePair<string, object>("IsActive", rewardPointsCommonObj.IsActive));
                OracleHandler sqLH = new OracleHandler();
                List<RewardPointsSettingInfo> lstRewardPointSet =
                    sqLH.ExecuteAsList<RewardPointsSettingInfo>("usp_Aspx_RewardPointsSettingGetAll",
                                                                parameterCollection);
                return lstRewardPointSet;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public static void RewardPointsRuleDelete(RewardPointsCommonInfo rewardPointCommonObj,
                                                  AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("RewardPointSettingsID",
                                                                         rewardPointCommonObj.RewardPointSettingsID));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));

                OracleHandler sqlH = new OracleHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_RewardPointsRuleDelete", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<RewardRuleListInfo> RewardPointsRuleListBind(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                OracleHandler sqlH = new OracleHandler();
                return sqlH.ExecuteAsList<RewardRuleListInfo>("usp_Aspx_RewardPointsRuleListBind", parameter);
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<MyRewardPointsInfo> GetMyRewardPointsHistory(int offset, int limit,
                                                                        AspxCommonInfo aspxCommonObj,
                                                                        RewardPointsHistoryCommonInfo
                                                                            RewardPointsHistoryCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("Limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("CustomerID",
                                                                         RewardPointsHistoryCommonObj.CustomerID));
                parameterCollection.Add(new KeyValuePair<string, object>("DateFrom",
                                                                        RewardPointsHistoryCommonObj.DateFrom));
                //parameterCollection.Add(new KeyValuePair<string, object>("@DateTo",
                //                                                        RewardPointsHistoryCommonObj.DateTo));
                OracleHandler sqLH = new OracleHandler();
                return sqLH.ExecuteAsList<MyRewardPointsInfo>("usp_Aspx_RewardPointsHistoryCustomer",
                                                              parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<RewardPointsHistryInfo> RewardPointsHistoryGetAll(int offset, int limit,
                                                                             AspxCommonInfo aspxCommonObj,
                                                                             RewardPointsHistoryCommonInfo
                                                                                 RewardPointsHistoryCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("Offset", offset));
                parameterCollection.Add(new KeyValuePair<string, object>("Limit", limit));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("CustomerName",
                                                                         RewardPointsHistoryCommonObj.CustomerName));
                parameterCollection.Add(new KeyValuePair<string, object>("Email", RewardPointsHistoryCommonObj.Email));
                OracleHandler sqLH = new OracleHandler();
                return sqLH.ExecuteAsList<RewardPointsHistryInfo>("usp_Aspx_RewardPointsHistory", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void RewardPointsSaveNewsLetter(RewardPointsNLCommonInfo rewardPointsInfo,
                                                      AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("RewardRuleID", rewardPointsInfo.RewardRuleID));
                parameter.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("CustomerID", aspxCommonObj.CustomerID));
                parameter.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
                parameter.Add(new KeyValuePair<string, object>("Email", rewardPointsInfo.Email));
                sqlH.ExecuteNonQuery("usp_Aspx_RewardPointsSaveNewsLetter", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RewardPointsSavePolling(RewardPointsPollCommonInfo rewardPointsInfo,
                                                      AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("RewardRuleID", rewardPointsInfo.RewardRuleID));
                parameter.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("CustomerID", aspxCommonObj.CustomerID));
                parameter.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
                parameter.Add(new KeyValuePair<string, object>("PollID", rewardPointsInfo.PollID));
                parameter.Add(new KeyValuePair<string, object>("UserModuleID", rewardPointsInfo.UserModuleID));
                sqlH.ExecuteNonQuery("usp_Aspx_RewardPointsSavePolling", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static void RewardPointsDeleteNewsLetter(RewardPointsNLCommonInfo rewardPointsInfo,
                                                        AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("RewardRuleID", rewardPointsInfo.RewardRuleID));
                parameterCollection.Add(new KeyValuePair<string, object>("CustomerID", aspxCommonObj.CustomerID));
                parameterCollection.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));

                OracleHandler sqlH = new OracleHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_RewardPointsDeleteNewsLetter", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static bool IsPurchaseActive(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                //usp_Aspx_RewardPointsIsPurchaseActive
                return sqlH.ExecuteNonQueryAsBool("usp_Aspx_RewardPointsIsPurchas", parameter, "IsActive");
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public static bool RewardPointsGeneralSettingsIsActive(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                bool IsActive = sqlH.ExecuteNonQueryAsBool("usp_Aspx_RewardPointsGeneralSettingsIsActive", parameterCollection, "IsActive");
                return IsActive;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}