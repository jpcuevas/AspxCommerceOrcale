using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.LogView
{
    public class LogController
    {
        public List<LogInfo> GetLogType()
        {
            try
            {
                LogProvider objProvider = new LogProvider();
                return objProvider.GetLogType();
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
                LogProvider objProvider = new LogProvider();
                return objProvider.GetLogView(PortalID, LogType);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}