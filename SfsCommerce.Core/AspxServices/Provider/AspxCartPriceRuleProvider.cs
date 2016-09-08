using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
using Oracle.DataAccess.Client;
using SageFrame.Web.Utilities;
using SageFrame.Web;

namespace AspxCommerce.Core
{
   public class AspxCartPriceRuleProvider
    {
       public AspxCartPriceRuleProvider()
       {
       }

       public static string ConnectionString
       {
           get { return SystemSetting.SageFrameConnectionString; }
       }

       public static List<ShippingMethodInfo> GetShippingMethods(System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("IsActive", isActive));
               OracleHandler Sq = new OracleHandler();
               List<ShippingMethodInfo> lstShipMethod= Sq.ExecuteAsList<ShippingMethodInfo>("usp_Aspx_GetShippingMethods", parameter);
               return lstShipMethod;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<CartPricingRuleAttributeInfo> GetCartPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
       {
           OracleHandler sqlHandler = new OracleHandler();
           List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           List<CartPricingRuleAttributeInfo> lst = sqlHandler.ExecuteAsList<CartPricingRuleAttributeInfo>("usp_Aspx_GetCartPricingRuleAttr", paramList);
           return lst;
       }

       public static int CartPriceRuleAdd(CartPriceRule cartPriceRule, OracleTransaction tran, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter =CommonParmBuilder.GetParamPUC(aspxCommonObj);
           parameter.Add(new KeyValuePair<string, object>("CartPriceRuleName", cartPriceRule.CartPriceRuleName));
           parameter.Add(new KeyValuePair<string, object>("CartPriceRuleDescription", cartPriceRule.CartPriceRuleDescription));
           parameter.Add(new KeyValuePair<string, object>("Apply", cartPriceRule.Apply));
           parameter.Add(new KeyValuePair<string, object>("Value", cartPriceRule.Value));
           parameter.Add(new KeyValuePair<string, object>("ApplytoShippingAmount", cartPriceRule.ApplytoShippingAmount));
           parameter.Add(new KeyValuePair<string, object>("DiscountQuantity", cartPriceRule.DiscountQuantity));
           parameter.Add(new KeyValuePair<string, object>("DiscountStep", cartPriceRule.DiscountStep));
           parameter.Add(new KeyValuePair<string, object>("FreeShipping", cartPriceRule.FreeShipping));
           parameter.Add(new KeyValuePair<string, object>("IsFurtherProcessing", cartPriceRule.IsFurtherProcessing));
           parameter.Add(new KeyValuePair<string, object>("FromDate", cartPriceRule.FromDate));
           parameter.Add(new KeyValuePair<string, object>("ToDate", cartPriceRule.ToDate));
           parameter.Add(new KeyValuePair<string, object>("Priority", cartPriceRule.Priority));
           parameter.Add(new KeyValuePair<string, object>("IsActive", cartPriceRule.IsActive));
           try
           {
               OracleHandler sqlH = new OracleHandler();

               if (cartPriceRule.CartPriceRuleID > 0)
                   DeleteCartPricingRuleForEdit(tran, cartPriceRule.CartPriceRuleID, aspxCommonObj.PortalID);

               int cartPRID= sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_CartPriceRuleAdd", parameter, "CartPriceRuleID");
               return cartPRID;

           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteCartPricingRuleForEdit(OracleTransaction tran, Int32 cartPriceRuleID, Int32 portalID)
       {
           OracleHandler sqlHandler = new OracleHandler();
           List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
           paramList.Add(new KeyValuePair<string, object>("CartPriceRuleID", cartPriceRuleID));
           paramList.Add(new KeyValuePair<string, object>("PortalID", portalID));
           sqlHandler.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_DeleteCartPriceForEdit", paramList);
       }

       public static int RuleConditionAdd(List<RuleCondition> lstRuleCondition, int cartPriceRuleID, List<int> parentID, OracleTransaction tran, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               int ruleConditionID = 0;
               int count = 0;
               int rcID = 0;
               List<int> lstParent = parentID; // new JavaScriptSerializer().ConvertToType<List<object>>(parentID);

               foreach (RuleCondition objRuleCondition in lstRuleCondition)
               {
                   if (count == 1 && ruleConditionID > 1)
                       rcID = ruleConditionID - 1;

                   List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                   parameter.Add(new KeyValuePair<string, object>("RuleConditionType", objRuleCondition.RuleConditionType));
                   parameter.Add(new KeyValuePair<string, object>("CartPriceRuleID", cartPriceRuleID));
                   parameter.Add(new KeyValuePair<string, object>("ParentID",
                                                                  rcID + int.Parse(lstParent[count].ToString())));
                   parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                   parameter.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));

                   OracleHandler sqlH = new OracleHandler();
                   ruleConditionID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_RuleConditionAdd", parameter, "RuleConditionID");

                   if (objRuleCondition.RuleConditionType == "CC" && objRuleCondition.LstCartPriceRuleConditions != null && objRuleCondition.LstCartPriceRuleConditions.Count > 0)
                   {
                       CartPriceRuleConditionAdd(objRuleCondition.LstCartPriceRuleConditions, cartPriceRuleID, ruleConditionID, tran, aspxCommonObj);
                   }
                   else if (objRuleCondition.RuleConditionType == "PAC" && objRuleCondition.LstProductAttributeRuleConditions != null && objRuleCondition.LstProductAttributeRuleConditions.Count > 0)
                   {
                       ProductAttributeRuleConditionAdd(objRuleCondition.LstProductAttributeRuleConditions, cartPriceRuleID, ruleConditionID, tran, aspxCommonObj);
                   }
                   else if (objRuleCondition.RuleConditionType == "PS" && objRuleCondition.LstProductSublectionRuleConditions != null && objRuleCondition.LstProductSublectionRuleConditions.Count > 0)
                   {
                       SubselectionRuleConditionAdd(objRuleCondition.LstProductSublectionRuleConditions, cartPriceRuleID, ruleConditionID, tran, aspxCommonObj);
                   }

                   count++;
               }
               return ruleConditionID;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public static void CartPriceRuleConditionAdd(List<CartPriceRuleCondition> lstCartPriceRuleCondition, int cartPriceRuleID, int ruleConditionID, OracleTransaction tran, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("RuleConditionID", ruleConditionID));
           parameter.Add(new KeyValuePair<string, object>("IsAll", lstCartPriceRuleCondition[0].IsAll));
           parameter.Add(new KeyValuePair<string, object>("IsTrue", lstCartPriceRuleCondition[0].IsTrue));
           parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));

           OracleHandler sqlH = new OracleHandler();
           try
           {
               sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_CartPriceRuleConditionAdd", parameter);
               if (lstCartPriceRuleCondition[0].LstCartConditionDetails != null && lstCartPriceRuleCondition[0].LstCartConditionDetails.Count > 0)
               {
                   CartConditionDetailAdd(lstCartPriceRuleCondition[0].LstCartConditionDetails,
                                          ruleConditionID, cartPriceRuleID, tran, aspxCommonObj);
               }
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void ProductAttributeRuleConditionAdd(List<ProductAttributeRuleCondition> lstPACRuleCondition, int cartPriceRuleID, int ruleConditionID, OracleTransaction tran, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("RuleConditionID", ruleConditionID));
           parameter.Add(new KeyValuePair<string, object>("IsAll", lstPACRuleCondition[0].IsAll));
           parameter.Add(new KeyValuePair<string, object>("IsFound", lstPACRuleCondition[0].IsFound));
           parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));

           OracleHandler sqlH = new OracleHandler();
           try
           {
               sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_ProductAttributeRuleConditionAdd", parameter);
               if (lstPACRuleCondition[0].LstCartConditionDetails != null && lstPACRuleCondition[0].LstCartConditionDetails.Count > 0)
               {
                   CartConditionDetailAdd(lstPACRuleCondition[0].LstCartConditionDetails,
                                          ruleConditionID, cartPriceRuleID, tran, aspxCommonObj);
               }
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void SubselectionRuleConditionAdd(List<ProductSubSelectionRuleCondition> lstPSRuleCondition, int cartPriceRuleID, int ruleConditionID, OracleTransaction tran, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("RuleConditionID", ruleConditionID));
           parameter.Add(new KeyValuePair<string, object>("IsAll", lstPSRuleCondition[0].IsAll));
           parameter.Add(new KeyValuePair<string, object>("IsQuantity", lstPSRuleCondition[0].IsQuantity));
           parameter.Add(new KeyValuePair<string, object>("Value", lstPSRuleCondition[0].Value));
           parameter.Add(new KeyValuePair<string, object>("RuleOperatorID", lstPSRuleCondition[0].RuleOperatorID));
           parameter.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));

           OracleHandler sqlH = new OracleHandler();
           try
           {
               sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_ProductSubSelectionRuleConditionAdd", parameter);
               if (lstPSRuleCondition[0].LstCartConditionDetails != null && lstPSRuleCondition[0].LstCartConditionDetails.Count > 0)
               {
                   CartConditionDetailAdd(lstPSRuleCondition[0].LstCartConditionDetails,
                                          ruleConditionID, cartPriceRuleID, tran, aspxCommonObj);
               }
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void CartConditionDetailAdd(List<CartConditionDetail> lstCartConditionDetail, int? ruleConditionID, int cartPriceRuleID, OracleTransaction tran, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                foreach (CartConditionDetail objCartConditionDetail in lstCartConditionDetail)
                {
                    if (objCartConditionDetail != null)
                    {
                        List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamPUC(aspxCommonObj);
                        parameter.Add(new KeyValuePair<string, object>("RuleConditionID", ruleConditionID));
                        parameter.Add(new KeyValuePair<string, object>("CartPriceRuleID", cartPriceRuleID));
                        parameter.Add(new KeyValuePair<string, object>("AttributeID",
                                                                       objCartConditionDetail.AttributeID));
                        parameter.Add(new KeyValuePair<string, object>("RuleOperatorID",
                                                                       objCartConditionDetail.RuleOperatorID));
                        parameter.Add(new KeyValuePair<string, object>("Value", objCartConditionDetail.Value));
                        parameter.Add(new KeyValuePair<string, object>("Priority", objCartConditionDetail.Priority));
                        parameter.Add(new KeyValuePair<string, object>("IsActive", true));
                        OracleHandler sqlH = new OracleHandler();
                        sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,
                                             "usp_Aspx_CartConditionDetailAdd", parameter);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void CartPriceRuleRoleAdd(CartPriceRuleRole cartPriceRuleRole,OracleTransaction tran,AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamPUC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("CartPriceRuleID", cartPriceRuleRole.CartPriceRuleID));
            parameter.Add(new KeyValuePair<string, object>("RoleID", cartPriceRuleRole.RoleID));
            parameter.Add(new KeyValuePair<string, object>("IsActive", true));
            OracleHandler sqlH = new OracleHandler();
            try
            {
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_CartPriceRuleRoleAdd", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void CartPriceRuleStoreAdd(CartPriceRuleStore cartPriceRuleStore, OracleTransaction tran, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamPUC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("CartPriceRuleID", cartPriceRuleStore.CartPriceRuleID));
            parameter.Add(new KeyValuePair<string, object>("StoreID", cartPriceRuleStore.StoreID));
            parameter.Add(new KeyValuePair<string, object>("IsActive", true));
            try
            {
                OracleHandler sqlH = new OracleHandler();
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_CartPriceRuleStoreAdd", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int CartPriceRuleDelete(int cartPriceRuleID,AspxCommonInfo aspxCommonObj)
        {
            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Parameters.Add(new OracleParameter("CartPriceRuleID", cartPriceRuleID));
            sqlCommand.Parameters.Add(new OracleParameter("StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new OracleParameter("PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new OracleParameter("UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new OracleParameter("CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CartPriceRuleDelete";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            OracleConnection sqlConnection = new OracleConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static int CartPriceRulesMultipleDelete(string cartRulesIds, AspxCommonInfo aspxCommonObj)
        {
            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Parameters.Add(new OracleParameter("CartPriceRulesIDs", cartRulesIds));
            sqlCommand.Parameters.Add(new OracleParameter("StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new OracleParameter("PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new OracleParameter("UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new OracleParameter("CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CartPriceRulesDeleteMultiple";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            OracleConnection sqlConnection = new OracleConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static List<CartPriceRulePaging> GetCartPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            paramList.Add(new KeyValuePair<string, object>("offset", offset));
            paramList.Add(new KeyValuePair<string, object>("limit", limit));
            paramList.Add(new KeyValuePair<string, object>("RuleName", ruleName));
            paramList.Add(new KeyValuePair<string, object>("StartDate", startDate));
            paramList.Add(new KeyValuePair<string, object>("EndDate", endDate));
            paramList.Add(new KeyValuePair<string, object>("IsActive", isActive));
            List<CartPriceRulePaging> lstCartPriceRule = sqlHandler.ExecuteAsList<CartPriceRulePaging>("usp_Aspx_GetCartPrincingRules", paramList);
            return lstCartPriceRule;
        }

        public static DataSet GetCartPriceRule(Int32 cartPriceRuleID, AspxCommonInfo aspxCommonObj)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamPUC(aspxCommonObj);
            paramList.Add(new KeyValuePair<string, object>("CartPriceRuleID", cartPriceRuleID));
            DataSet ds = sqlHandler.ExecuteAsDataSet("usp_Aspx_GetCartPricingRule", paramList);
            return ds;
        }

        public static List<ProductAttributeRuleCondition> GetCartPriceProductAttributeConditions(Int32? ruleConditionID, Int32 portalID)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("RuleConditionID", ruleConditionID));
            paramList.Add(new KeyValuePair<string, object>("PortalID", portalID));
            List<ProductAttributeRuleCondition> lstPAttrRule= sqlHandler.ExecuteAsList<ProductAttributeRuleCondition>("usp_Aspx_GetProductAttributeCombinations", paramList);
            return lstPAttrRule;
        }

        public static List<CartConditionDetail> GetCartPriceRuleConditionDetails(Int32 cartPriceRuleID, Int32? ruleConditionID, Int32 portalID, string userName)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("CartPriceRuleID", cartPriceRuleID));
            paramList.Add(new KeyValuePair<string, object>("RuleConditionID", ruleConditionID));
            paramList.Add(new KeyValuePair<string, object>("PortalID", portalID));
            paramList.Add(new KeyValuePair<string, object>("UserName", userName));
            List<CartConditionDetail> lstCCDetail= sqlHandler.ExecuteAsList<CartConditionDetail>("usp_Aspx_CartPriceRuleConditionDetails", paramList);
            return lstCCDetail;
        }

        public static List<ProductSubSelectionRuleCondition> GetCartPriceSubSelections(Int32? ruleConditionID, Int32 portalID)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("RuleConditionID", ruleConditionID));
            paramList.Add(new KeyValuePair<string, object>("PortalID", portalID));
            List<ProductSubSelectionRuleCondition> lstPSSC= sqlHandler.ExecuteAsList<ProductSubSelectionRuleCondition>("usp_Aspx_GetProductSubSelections", paramList);
            return lstPSSC;
        }
        public static List<CartPriceRuleCondition> GetCartPriceRuleConditions(Int32? ruleConditionID, Int32 portalID)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("RuleConditionID", ruleConditionID));
            paramList.Add(new KeyValuePair<string, object>("PortalID", portalID));
            List<CartPriceRuleCondition> lstCPRC= sqlHandler.ExecuteAsList<CartPriceRuleCondition>("usp_Aspx_GetCartPriceConditions", paramList);
            return lstCPRC;
        }

        public static bool CheckCartPricePriorityUniqueness(int cartPriceRuleID, int priority, int portalID)
        {
            try
            {
                OracleHandler Sq = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("CartPriceRuleID", cartPriceRuleID));
                parameterCollection.Add(new KeyValuePair<string, object>("Priority", priority));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                bool isUnique= Sq.ExecuteNonQueryAsBool("usp_Aspx_CartPricePriorityUniquenessCheck", parameterCollection, "IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
