using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using SageFrame.Web;
using System.Data;
using SageFrame.Localization.Info;

namespace SageFrame.Localization
{
    public class LocalizationSqlDataProvider
    {
        public static List<Language> GetAvailableLocales()
        {
            List<Language> lstAvailableLocales = new List<Language>();
            string StoredProcedureName = "sp_LanguageGet";
            //SqlDataReader SQLReader;
            OracleDataReader SQLReader;
            try
            {
                OracleConnection SQLConn = new OracleConnection(SystemSetting.SageFrameConnectionString);
                //SqlConnection SQLConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                OracleCommand SQLCmd = new OracleCommand();
                //SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StoredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLConn.Open();
                SQLReader = SQLCmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }

            while (SQLReader.Read())
            {

                Language obj = new Language(int.Parse(SQLReader["LanguageID"].ToString()), SQLReader["CultureName"].ToString(), SQLReader["CultureCode"].ToString());
                obj.LanguageN = SQLReader["CultureName"].ToString();
                obj.Country = SQLReader["CultureName"].ToString();
                lstAvailableLocales.Add(obj);
            }
            SQLReader.Close();
            return lstAvailableLocales;


        }
        public static void AddLanguage(Language objLanguage)
        {
            List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
            ParaMeterCollection.Add(new KeyValuePair<string, string>("CultureName", objLanguage.LanguageName));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("CultureCode", objLanguage.LanguageCode));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("FallbackCulture", objLanguage.FallBackLanguage));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("FallbackCultureCode", objLanguage.FallBackLanguageCode));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("CreatedByUserID", "0"));

            try
            {
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                sagesql.ExecuteNonQuery("sp_LanguageAdd", ParaMeterCollection);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static List<Countries> GetCountryList()
        {
            List<Countries> lstCountries = new List<Countries>();
            DataSet ds = new DataSet();
            try
            {
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                ds = sagesql.ExecuteAsDataSet("sp_GetCountryList");
            }
            catch (Exception)
            {
                throw;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lstCountries.Add(new Countries("images/flags/" + row["Value"].ToString().ToLower() + ".png", row["Text"].ToString(), row["Culture"].ToString()));
            }
            return lstCountries;
        }

        public static void EnableLanguage(int portalId, int languageId, string addedBy, int isEnabled, int isPublished)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", portalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("LanguageID", languageId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("AddedBy", addedBy));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("IsEnabled", isEnabled));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("IsPublished", isPublished));
            try
            {
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                sagesql.ExecuteNonQuery("usp_loc_EnableLanguage", ParaMeterCollection);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Language> GetPortalLanguages(int portalId)
        {
            List<Language> lstPortalLanguages = new List<Language>();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            string StoredProcedureName = "usp_loc_PortalLanguagesGet";
            //SqlDataReader SQLReader;
            OracleDataReader SQLReader;
            try
            {
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
            while (SQLReader.Read())
            {
                Language obj = new Language(int.Parse(SQLReader["LanguageID"].ToString()), SQLReader["CultureName"].ToString(), SQLReader["CultureCode"].ToString());
                obj.LanguageN = SQLReader["CultureName"].ToString();
                obj.Country = SQLReader["CultureName"].ToString();
                lstPortalLanguages.Add(obj);
            }
            SQLReader.Close();
            return lstPortalLanguages;
        }

        public static List<ModuleInfo> GetCoreModules()
        {
            List<ModuleInfo> lstCoreModules = new List<ModuleInfo>();
            string StoredProcedureName = "usp_loc_CoreModulesGet";
            //SqlDataReader SQLReader;
            OracleDataReader SQLReader;
            try
            {
                //SqlConnection SQLConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                OracleConnection SQLConn = new OracleConnection(SystemSetting.SageFrameConnectionString);
                //SqlCommand SQLCmd = new SqlCommand();
                OracleCommand SQLCmd = new OracleCommand();
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StoredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLConn.Open();
                SQLReader = SQLCmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }

            while (SQLReader.Read())
            {
                ModuleInfo obj = new ModuleInfo();
                obj.ModuleID = int.Parse(SQLReader["ModuleID"].ToString());
                obj.ModuleName = SQLReader["ModuleName"].ToString();
                lstCoreModules.Add(obj);
            }
            SQLReader.Close();
            return lstCoreModules;
        }

        public static void AddLanguageSwitchSettings(List<LanguageSwitchKeyValue> lstKeyValue, int UserModuleID, int PortalID)
        {
            foreach (LanguageSwitchKeyValue obj in lstKeyValue)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("UserModuleID", UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("SettingKey", obj.Key));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("SettingValue", obj.Value));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("IsActive", obj.IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("UpdatedBy", obj.AddedBy));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("AddedBy", obj.AddedBy));

                try
                {
                    //SQLHandler sagesql = new SQLHandler();
                    OracleHandler sagesql = new OracleHandler();
                    sagesql.ExecuteNonQuery("usp_loc_AddLanguageSwitchSettings", ParaMeterCollection);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public static List<LanguageSwitchKeyValue> GetLanguageSwitchSettings(int portalId, int UserModuleID)
        {
            List<LanguageSwitchKeyValue> lstSettings = new List<LanguageSwitchKeyValue>();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("v_PortalID", portalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("v_UserModuleID", UserModuleID));
            string StoredProcedureName = "usp_loc_GetLanguageSwitchSetti";
            //SqlDataReader SQLReader;
            OracleDataReader SQLReader;
            try
            {
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
            while (SQLReader.Read())
            {
                lstSettings.Add(new LanguageSwitchKeyValue(SQLReader["SettingKey"].ToString(), SQLReader["SettingValue"].ToString()));
            }
            SQLReader.Close();
            return lstSettings;
        }



        public static List<LocalPageInfo> GetLocalPageName(int PortalID, string CultureCode)
        {
            //SQLHandler sqlHan = new SQLHandler();
            OracleHandler sqlHan = new OracleHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("CultureCode", CultureCode));
            return sqlHan.ExecuteAsList<LocalPageInfo>("usp_MenuLocalizeGetPages", ParaMeterCollection);
        }


        public static List<LocalModuleInfo> GetLocalModuleTitle(int PortalID, string CultureCode)
        {
            //SQLHandler sqlHan = new SQLHandler();
            OracleHandler sqlHan = new OracleHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("CultureCode", CultureCode));
            return sqlHan.ExecuteAsList<LocalModuleInfo>("usp_MenuLocalizeGetModuleTitle", ParaMeterCollection);

        }

        public static void AddUpdateLocalPage(List<LocalPageInfo> lstLocalPage)
        {
            string StoredProcedureName = "usp_AddUpdateLocalPage";
            //SQLHandler sqlHan = new SQLHandler();
            OracleHandler sqlHan = new OracleHandler();
            foreach (LocalPageInfo objPage in lstLocalPage)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("PageID", objPage.PageID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("LocalPageName", objPage.LocalPageName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("CultureCode", objPage.CultureCode));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("LocalPageCaption", objPage.LocalPageCaption));

                sqlHan.ExecuteNonQuery(StoredProcedureName, ParaMeterCollection);
            }
        }

        public static void AddUpdateLocalModuleTitle(List<LocalModuleInfo> lstLocalPage)
        {
            string StoredProcedureName = "usp_AddUpdateLocalModuleTitle";
            //SQLHandler sqlHan = new SQLHandler();
            OracleHandler sqlHan = new OracleHandler();
            foreach (LocalModuleInfo objPage in lstLocalPage)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("UserModuleID", objPage.UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("LocalModuleTitle", objPage.LocalModuleTitle));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("CultureCode", objPage.CultureCode));

                sqlHan.ExecuteNonQuery(StoredProcedureName, ParaMeterCollection);
            }
        }

        public static void DeleteLanguage(string code)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("code", code));
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                SQLH.ExecuteNonQuery("sp_LanguageDelete", ParaMeterCollection);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

}