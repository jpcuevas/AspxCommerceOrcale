using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.ErrorLog
{
    public class ErrorLogDataProvider
    {
        public void ClearLog(int PortalID)
        {
            string sp = "sp_LogClear";
            OracleHandler oraclesql = new OracleHandler();
            
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                oraclesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteLogByLogID(int ID, int PortalID, string UserName)
        {
            string sp = "sp_LogDeleteByLogID";
            OracleHandler oraclesql = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("LogID", ID));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("DeletedBy", UserName));
                oraclesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertLog(int logTypeID, int severity, string message, string exception, string clientIPAddress, string pageURL, bool isActive, int portalID, string addedBy)
        {
            string sp = "sp_LogInsert";
            OracleHandler oraclesql = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();

                ParamCollInput.Add(new KeyValuePair<string, object>("LogTypeID", logTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("Severity", severity));
                ParamCollInput.Add(new KeyValuePair<string, object>("Message", message));

                ParamCollInput.Add(new KeyValuePair<string, object>("EXCEPTIO", exception));
                ParamCollInput.Add(new KeyValuePair<string, object>("ClientIPAddress", clientIPAddress));
                ParamCollInput.Add(new KeyValuePair<string, object>("PageURL", pageURL));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsActive", isActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", portalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("AddedBy", addedBy));

                return oraclesql.ExecuteNonQuery(sp, ParamCollInput, "LogID");

                


            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}