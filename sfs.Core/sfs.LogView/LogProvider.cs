using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.LogView
{
    public class LogProvider
    {
        public List<LogInfo> GetLogType()
        {
            try
            {
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();

                return SQLH.ExecuteAsList<LogInfo>("sp_LogType");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<LogInfo> GetLogView(int PortalID, string LogType)
        {
            try
            {
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("LogType", LogType));
                return SQLH.ExecuteAsList<LogInfo>("sp_LogView", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}