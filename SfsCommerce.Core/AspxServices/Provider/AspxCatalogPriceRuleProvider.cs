using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Threading;

namespace AspxCommerce.Core
{
    public class AspxCatalogPriceRuleProvider
    {

        public AspxCatalogPriceRuleProvider()
        {
        }

        public static string ConnectionString
        {
            get { return SystemSetting.SageFrameConnectionString; }
        }

        public static List<PricingRuleAttributeInfo> GetPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            List<PricingRuleAttributeInfo> lstPriceRuleAttr = sqlHandler.ExecuteAsList<PricingRuleAttributeInfo>("usp_Aspx_GetPricingRuleAttr", paramList);
            return lstPriceRuleAttr;
        }

        public static List<CatalogPriceRulePaging> GetCatalogPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            paramList.Add(new KeyValuePair<string, object>("offset", offset));
            paramList.Add(new KeyValuePair<string, object>("limit", limit));
            paramList.Add(new KeyValuePair<string, object>("RuleName", ruleName));
            paramList.Add(new KeyValuePair<string, object>("StartDate", startDate));
            paramList.Add(new KeyValuePair<string, object>("EndDate", endDate));
            paramList.Add(new KeyValuePair<string, object>("IsActive", isActive));
            List<CatalogPriceRulePaging> lstCatalogPriceRule = sqlHandler.ExecuteAsList<CatalogPriceRulePaging>("usp_Aspx_GetPricingRules", paramList);
            return lstCatalogPriceRule;
        }

        public static DataSet GetCatalogPricingRule(Int32 catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
        {
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            paramList.Add(new KeyValuePair<string, object>("CatalogPriceRuleID", catalogPriceRuleID));
            DataSet ds = sqlHandler.ExecuteAsDataSet("usp_Aspx_GetPricingRuleInfoByID", paramList);
            return ds;
        }

        public static int CatalogPriceRuleAdd(CatalogPriceRule catalogPriceRule, AspxCommonInfo aspxCommonObj)
        {
            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Parameters.Add(new OracleParameter("CatalogPriceRuleID", catalogPriceRule.CatalogPriceRuleID));
            sqlCommand.Parameters.Add(new OracleParameter("CatalogPriceRuleName", catalogPriceRule.CatalogPriceRuleName));
            sqlCommand.Parameters.Add(new OracleParameter("CatalogPriceRuleDescription", catalogPriceRule.CatalogPriceRuleDescription));
            sqlCommand.Parameters.Add(new OracleParameter("Apply", catalogPriceRule.Apply));
            sqlCommand.Parameters.Add(new OracleParameter("Value", catalogPriceRule.Value));
            sqlCommand.Parameters.Add(new OracleParameter("IsFurtherProcessing", catalogPriceRule.IsFurtherProcessing));
            sqlCommand.Parameters.Add(new OracleParameter("FromDate", catalogPriceRule.FromDate));
            sqlCommand.Parameters.Add(new OracleParameter("ToDate", catalogPriceRule.ToDate));
            sqlCommand.Parameters.Add(new OracleParameter("Priority", catalogPriceRule.Priority));
            sqlCommand.Parameters.Add(new OracleParameter("IsActive", catalogPriceRule.IsActive));
            sqlCommand.Parameters.Add(new OracleParameter("StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new OracleParameter("PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new OracleParameter("UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new OracleParameter("CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRuleAdd";
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

        public void ApplyCatalogPricingRule(AspxCommonInfo aspxCommonObj)
        {
            int CurrentPortalID = 0;
            int CurrentOffset = 0;
            int CurrentLimit = 0;
            string CurrentCulture = string.Empty;
            CurrentPortalID = aspxCommonObj.PortalID;
            CurrentCulture = aspxCommonObj.CultureName;
            bool isExistCatalogRule = CheckCatalogRuleExist(aspxCommonObj);
            if (isExistCatalogRule)
            {
                int rowTotal = GetItemID(aspxCommonObj.PortalID);
                decimal intTempLoops = Math.Ceiling(Convert.ToDecimal((double)rowTotal / 1000));
                //List<Thread> listThreads = new List<Thread>();
                for (int i = 1; i <= intTempLoops; i++)
                {

                    //bool startNewThread = true;
                    //foreach (Thread thread in listThreads)
                    //{
                    //    if (thread.IsAlive)
                    //    {
                    //        startNewThread = false;
                    //    }
                    //}

                    //if (startNewThread)
                    //{
                        CurrentOffset = CurrentLimit + 1;
                        CurrentLimit = 1000 * i;
                        if (CurrentLimit >= rowTotal)
                        {
                            CurrentLimit = rowTotal;
                        }
                        AnalyseCatalogPrice(aspxCommonObj, CurrentOffset, CurrentLimit);
                        //listThreads.Add(newThread);
                        //newThread.Start();
                    //}
                    //else
                    //{
                    //    i--;
                    //}

                }

                //while (listThreads.Last().IsAlive)
                //{

                //}
                TruncateCatalogAffectedPrice(aspxCommonObj.PortalID);               
            }
            else
            {
                DeleteCatalogAffectedPrice(aspxCommonObj);
            }

        }

        public int GetItemID(int iPortalID)
        {
            int result = 0;

            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", iPortalID));
                OracleHandler sqLH = new OracleHandler();
                result = sqLH.ExecuteAsScalar<int>("usp_Aspx_CatalogGetTotalItem", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public void TruncateCatalogAffectedPrice(int iPortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", iPortalID));
                OracleHandler sqLH = new OracleHandler();
                sqLH.ExecuteNonQuery("usp_Aspx_CatalogTruncateAffectedPrice", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckCatalogRuleExist(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isExist = false;
                OracleHandler sqlHandler = new OracleHandler();
                List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSP(aspxCommonObj);
                isExist = sqlHandler.ExecuteNonQueryAsBool("usp_Aspx_CheckCatalogRuleExist", paramList, "IsExist");
                return isExist;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteCatalogAffectedPrice(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlHandler = new OracleHandler();
                List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSP(aspxCommonObj);
                sqlHandler.ExecuteNonQuery("usp_Aspx_CatalogAffectedPriceDelete", paramList);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AnalyseCatalogPrice(AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Parameters.Add(new OracleParameter("StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new OracleParameter("PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new OracleParameter("CultureName", aspxCommonObj.CultureName));
            sqlCommand.Parameters.Add(new OracleParameter("offset", offset));
            sqlCommand.Parameters.Add(new OracleParameter("limit", limit));
            sqlCommand.CommandTimeout = 0;
            sqlCommand.CommandText = "usp_Aspx_ApplyCatalogPricingRuleForSchedular";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            OracleConnection sqlConnection = new OracleConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

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

        public static int CatalogPriceRuleConditionAdd(CatalogPriceRuleCondition catalogPriceRuleCondition, AspxCommonInfo aspxCommonObj)
        {
            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Parameters.Add(new OracleParameter("CatalogPriceRuleID", catalogPriceRuleCondition.CatalogPriceRuleID));
            sqlCommand.Parameters.Add(new OracleParameter("IsAll", catalogPriceRuleCondition.IsAll));
            sqlCommand.Parameters.Add(new OracleParameter("IsTrue", catalogPriceRuleCondition.IsTrue));
            sqlCommand.Parameters.Add(new OracleParameter("ParentID", catalogPriceRuleCondition.ParentID));
            sqlCommand.Parameters.Add(new OracleParameter("IsActive", true));
            sqlCommand.Parameters.Add(new OracleParameter("StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new OracleParameter("PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new OracleParameter("UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new OracleParameter("CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRuleConditionAdd";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            OracleConnection sqlConnection = new OracleConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                if (Convert.ToInt16(val) > 0)
                {
                    int catalogConditionDetailID = -1;
                    foreach (CatalogConditionDetail catalogConditionDetail in catalogPriceRuleCondition.CatalogConditionDetail)
                    {
                        if (catalogConditionDetail != null)
                        {
                            catalogConditionDetail.CatalogPriceRuleConditionID = Convert.ToInt16(val);
                            catalogConditionDetail.CatalogPriceRuleID = catalogPriceRuleCondition.CatalogPriceRuleID;
                            catalogConditionDetailID =
                                AspxCatalogPriceRuleProvider.CatalogConditionDetailAdd(catalogConditionDetail, aspxCommonObj);
                            if (!(catalogConditionDetailID > 0))
                            {
                            }
                        }
                    }
                }
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

        public static int CatalogPriceRuleRoleAdd(CatalogPriceRuleRole catalogPriceRuleRole, AspxCommonInfo aspxCommonObj)
        {
            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Parameters.Add(new OracleParameter("CatalogPriceRuleID", catalogPriceRuleRole.CatalogPriceRuleID));
            sqlCommand.Parameters.Add(new OracleParameter("RoleID", catalogPriceRuleRole.RoleID));
            sqlCommand.Parameters.Add(new OracleParameter("IsActive", true));
            sqlCommand.Parameters.Add(new OracleParameter("StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new OracleParameter("PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new OracleParameter("UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new OracleParameter("CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRuleRoleAdd";
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

        public static int CatalogConditionDetailAdd(CatalogConditionDetail catalogConditionDetail, AspxCommonInfo aspxCommonObj)
        {
            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Parameters.Add(new OracleParameter("CatalogPriceRuleConditionID", catalogConditionDetail.CatalogPriceRuleConditionID));
            sqlCommand.Parameters.Add(new OracleParameter("CatalogPriceRuleID", catalogConditionDetail.CatalogPriceRuleID));
            sqlCommand.Parameters.Add(new OracleParameter("AttributeID", catalogConditionDetail.AttributeID));
            sqlCommand.Parameters.Add(new OracleParameter("RuleOperatorID", catalogConditionDetail.RuleOperatorID));
            sqlCommand.Parameters.Add(new OracleParameter("Value", catalogConditionDetail.Value));
            sqlCommand.Parameters.Add(new OracleParameter("Priority", catalogConditionDetail.Priority));
            sqlCommand.Parameters.Add(new OracleParameter("IsActive", true));
            sqlCommand.Parameters.Add(new OracleParameter("StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new OracleParameter("PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new OracleParameter("UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new OracleParameter("CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogConditionDetailAdd";
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

        public static int CatalogPriceRuleDelete(int catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
        {

            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Parameters.Add(new OracleParameter("CatalogPriceRuleID", catalogPriceRuleID));
            sqlCommand.Parameters.Add(new OracleParameter("StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new OracleParameter("PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new OracleParameter("UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new OracleParameter("CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRuleDelete";
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

        public static int CatalogPriceRulesMultipleDelete(string catRulesIds, AspxCommonInfo aspxCommonObj)
        {
            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Parameters.Add(new OracleParameter("CatalogPriceRulesIDs", catRulesIds));
            sqlCommand.Parameters.Add(new OracleParameter("StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new OracleParameter("PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new OracleParameter("UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new OracleParameter("CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRulesDeleteMultiple";
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

        public static bool CheckCatalogPriorityUniqueness(int catalogPriceRuleID, int priority, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("CatalogPriceRuleID", catalogPriceRuleID));
                parameterCollection.Add(new KeyValuePair<string, object>("Priority", priority));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("usp_Aspx_CatalogPriorityUniquenessCheck", parameterCollection, "@IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
