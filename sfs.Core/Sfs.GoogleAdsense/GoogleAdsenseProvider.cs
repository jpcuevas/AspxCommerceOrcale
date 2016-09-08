using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GoogleAdsenseProvider
/// </summary>
namespace SageFrame.GoogleAdsense
{
    public class GoogleAdsenseProvider
    {
        public int CountAdsenseSettings(int UserModuleID, int PortalID)
        {
            try
            {
                string sp = "sp_AdSenseSettingsCount";
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));


                int UserModuleCount = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "UserModuleCount");
                return UserModuleCount;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<GoogleAdsenseInfo> GetAdSenseSettingsByUserModuleID(int UserModuleID, int PortalID)
        {
            try
            {
                string sp = "sp_AdSenseSettingsGetByUserModuleID";
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                return sagesql.ExecuteAsList<GoogleAdsenseInfo>(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }


        public void AddUpdateAdSense(int UserModuleID, string SettingName, string SettingValue, bool IsActive, int PortalID, string UpdatedBy, bool UpdateFlag)
        {
            try
            {
                string sp = "sp_AdSenseAddUpdate";
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("SettingName", SettingName));
                ParamCollInput.Add(new KeyValuePair<string, object>("SettingValue", SettingValue));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsActive", Convert.ToString(IsActive)));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("UpdatedBy", UpdatedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("UpdateFlag", Convert.ToString(UpdateFlag)));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteAdSense(int UserModuleID, int PortalID)
        {
            try
            {
                string sp = "sp_AdSenseDelete";
                //SQLHandler sageSql = new SQLHandler();
                OracleHandler sageSql = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                sageSql.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CountAdSense(int UserModuleID, int PortalID)
        {
            try
            {
                string sp = "sp_AdSenseCount";
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));


                int UserModuleCount = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "UserModuleCount");
                return UserModuleCount;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}