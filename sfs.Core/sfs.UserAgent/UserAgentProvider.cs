using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace SageFrame.UserAgent
{
    public class UserAgentProvider
    {

        public string GetUserAgent(int PortalID, bool IsActive)
        {

            string sp = "usp_UserAgentGetType";
            //SQLHandler sagesql = new SQLHandler();
            OracleHandler sagesql = new OracleHandler();

            string content = "";
            //SqlDataReader reader = null;
            OracleDataReader reader = null;

            int v_IsActive;

            if (IsActive)
            {
                 v_IsActive = 1;
            }
            else
            {
                v_IsActive = 0;
            }

            try
            {
                List<KeyValuePair<string, object>> paramColl = new List<KeyValuePair<string, object>>();
                paramColl.Add(new KeyValuePair<string, object>("v_PortalID", PortalID));
                paramColl.Add(new KeyValuePair<string, object>("v_IsActive", v_IsActive));
                reader = sagesql.ExecuteAsDataReader(sp, paramColl);
                while (reader.Read())
                {
                    content = reader["AgentMode"] as string;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return content;
        }

        public void SaveUserAgentMode(string AgentMode, int PortalID, string UserName, DateTime ChangeDate, bool IsActive)
        {
            string sp = "usp_UserAgentSaveType";
            OracleHandler sagesql = new OracleHandler();
            //SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("AgentMode", AgentMode));
            ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            ParamCollInput.Add(new KeyValuePair<string, object>("ChangedBy", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("ChangedDate", ChangeDate));
            ParamCollInput.Add(new KeyValuePair<string, object>("IsActive", IsActive));
            try
            {
                sagesql.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}