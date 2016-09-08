#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
#endregion


namespace SageFrame.Templating
{
    public class TemplateDataProvider
    {
        public static void ActivateTemplate(string TemplateName, int PortalID)
        {
             
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@TemplateName", TemplateName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", true));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

            OracleHandler sagesql = new OracleHandler();
            sagesql.ExecuteNonQuery("usp_sftemplate_activate", ParaMeterCollection);

       
        }

        public static TemplateInfo GetActiveTemplate(int PortalID)
        {
            OracleHandler sagesql = new OracleHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("v_Portalid", PortalID));
            return (sagesql.ExecuteAsObject<TemplateInfo>("USP_SFTEM_GETACTIVETEMPLATE", ParaMeterCollection));
        }
        public static List<TemplateInfo> GetPortalTemplates()
        {
            OracleHandler sagesql = new OracleHandler();
            return (sagesql.ExecuteAsList<TemplateInfo>("usp_TemplateGetPortalTemplate"));
        }


        public static void UpdActivateTemplate(string TemplateName, string conn)
        {

            SqlConnection sqlcon = new SqlConnection(conn);
            sqlcon.Open();
            SqlCommand sqlcmd = new SqlCommand("usp_sftemplate_updactive", sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@TemplateName", TemplateName);
            sqlcmd.ExecuteNonQuery();
            sqlcon.Close();
        }

        public static SettingInfo GetSettingByKey(SettingInfo objSetting)
        {
            string sp = "usp_DashboardGetSettingByKey";
            OracleHandler sagesql = new OracleHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("SettingKey", objSetting.SettingKey));
            ParamCollInput.Add(new KeyValuePair<string, object>("UserName", objSetting.UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", objSetting.PortalID));
            try
            {
                return (sagesql.ExecuteAsObject<SettingInfo>(sp, ParamCollInput));

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
