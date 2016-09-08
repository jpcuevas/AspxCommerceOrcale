using SageFrame.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;

namespace SageFrame.ModuleManager
{
    public class LayoutMgrDataProvider
    {
        public static List<LayoutMgrInfo> GetModules(int PortalID)
        {
            //SQLHandler SQLH = new SQLHandler();
            OracleHandler SQLH = new OracleHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            return SQLH.ExecuteAsList<LayoutMgrInfo>("usp_ModuleManagerGetPortalModules", ParaMeterCollection);
        }

        public static List<LayoutMgrInfo> GetAdminModules(int PortalID)
        {
            //SQLHandler SQLH = new SQLHandler();
            OracleHandler SQLH = new OracleHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            return SQLH.ExecuteAsList<LayoutMgrInfo>("usp_ModuleManagerGetAdminModules", ParaMeterCollection);
        }

        public static List<LayoutMgrInfo> GetModuleInformation(string ModuleName)
        {
            //SQLHandler SQLH = new SQLHandler();
            OracleHandler SQLH = new OracleHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("FriendlyName", ModuleName));

            return SQLH.ExecuteAsList<LayoutMgrInfo>("usp_GetModuleInformation", ParaMeterCollection);
        }

        public static List<LayoutMgrInfo> SearchModules(string search, int PortalID, bool IsAdmin)
        {
            //SQLHandler SQLH = new SQLHandler();
            OracleHandler SQLH = new OracleHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("SearchText", search));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("IsAdmin", IsAdmin));
            return SQLH.ExecuteAsList<LayoutMgrInfo>("usp_ModuleManagerGetSearchModules", ParaMeterCollection);
        }

        public static List<LayoutMgrInfo> GetSortModules(int flag, bool isAdmin, int PortalID, int IncludePortalModules)
        {
            //SQLHandler SQLH = new SQLHandler();
            OracleHandler SQLH = new OracleHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("flag", flag));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("isAdmin", isAdmin));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("IncludePortalModules", IncludePortalModules));
            try
            {
                return SQLH.ExecuteAsList<LayoutMgrInfo>("usp_ModuleMgrSortModules", ParaMeterCollection);
            }
            catch (Exception)
            {

                throw;
            }

        }



        public static int AddLayOutMgr(LayoutMgrInfo obj)
        {
            try
            {
                OracleConnection SQLConn = new OracleConnection(SystemSetting.SageFrameConnectionString);
                //SqlConnection SQLConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                //SqlCommand SQLCmd = new SqlCommand();
                OracleCommand SQLCmd = new OracleCommand();
                int ReturnValue = -1;
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = "usp_AddModulesOrder";
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.Parameters.Add(new OracleParameter("ModuleOrder", obj.ModuleOrder));
                SQLCmd.Parameters.Add(new OracleParameter("PortelID", obj.PortelID));
                SQLCmd.Parameters.Add(new OracleParameter("ModuleID", obj.ModuleID));
                SQLCmd.Parameters.Add(new OracleParameter("ModuleName", obj.ModuleName));
                SQLCmd.Parameters.Add(new OracleParameter("PaneName", obj.PaneName));
                SQLCmd.Parameters.Add(new OracleParameter("UserModuleID", obj.UserModuleID));

                SQLCmd.Parameters.Add(new OracleParameter("NewModuleID", OracleDbType.Int32));
                SQLCmd.Parameters["NewModuleID"].Direction = ParameterDirection.Output;

                SQLConn.Open();
                SQLCmd.ExecuteNonQuery();
                ReturnValue = (int)SQLCmd.Parameters["NewModuleID"].Value;
                return ReturnValue;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}