using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.ModuleControls
{
    public class ModuleControlDataProvider
    {
        public static int GetModuleID(int UserModuleID)
        {
           
            OracleHandler Objsql = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("UserModuleID", UserModuleID));
                //SQLHandler sqlh = new SQLHandler();
                OracleHandler sqlh = new OracleHandler();
                int ModuleID = 0;
                ModuleID = sqlh.ExecuteAsScalar<int>("usp_ModuleControlGetModuleIdFromUserModuleId", ParaMeterCollection);
                return ModuleID;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static string GetModuleName(int UserModuleID)
        {
            //SQLHandler Objsql = new SQLHandler();
            OracleHandler Objsql = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("UserModuleID", UserModuleID));
                //SQLHandler sqlh = new SQLHandler();
                OracleHandler sqlh = new OracleHandler();
                string ModuleName = "";
                ModuleName = sqlh.ExecuteAsScalar<string>("usp_ModuleControlGetModuleNameFromUserModuleId", ParaMeterCollection);
                return ModuleName;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static List<ModuleControlInfo> GetControlType(int ModuleDefID)
        {
            List<ModuleControlInfo> lstControl = new List<ModuleControlInfo>();
            string StoredProcedureName = "usp_ModuleControlGetControlTypeFromModuleID";
            //SQLHandler sqlh = new SQLHandler();
            OracleHandler sqlh = new OracleHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("ModuleDefID", ModuleDefID));

            try
            {
                lstControl = sqlh.ExecuteAsList<ModuleControlInfo>(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstControl;
        }

    }
}