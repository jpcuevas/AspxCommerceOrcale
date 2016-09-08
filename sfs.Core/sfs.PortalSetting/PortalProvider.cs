using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;


namespace SageFrame.PortalSetting
{
    public class PortalProvider
    {

        public static List<PortalInfo> GetPortalList()
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                return SQLH.ExecuteAsList<PortalInfo>("sp_PortalGetList");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static PortalInfo GetPortalByPortalID(int PortalID, string UserName)
        {
            string sp = "sp_PortalGetByPortalID";
            OracleDataReader reader = null;
            try
            {

                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", UserName));


                reader = SQLH.ExecuteAsDataReader(sp, ParamCollInput);
                PortalInfo objList = new PortalInfo();

                while (reader.Read())
                {

                    objList.PortalID = int.Parse(reader["PortalID"].ToString());
                    objList.Name = reader["Name"].ToString();
                    objList.SEOName = reader["SEOName"].ToString();
                    objList.IsParent = bool.Parse(reader["IsParent"].ToString());
                    objList.ParentPortalName = reader["ParentPortalName"].ToString();
                }
                return objList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

        }
        public static void DeletePortal(int PortalID, string UserName)
        {
            string sp = "sp_PortalDelete";
            OracleHandler SQLH = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", UserName));

                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdatePortal(int PortalID, string PortalName, bool IsParent, string UserName, string PortalURL, int ParentID)
        {
            string sp = "sp_PortalUpdate";
            OracleHandler SQLH = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalName", PortalName));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsParent", IsParent));
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalURL", PortalURL));
                ParamCollInput.Add(new KeyValuePair<string, object>("ParentID", ParentID));

                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<PortalInfo> GetPortalModulesByPortalID(int PortalID, string UserName)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", UserName));

                return SQLH.ExecuteAsList<PortalInfo>("sp_PortalModulesGetByPortalID", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static void UpdatePortalModules(string ModuleIDs, string IsActives, int PortalID, string UpdatedBy)
        {
            string sp = "sp_PortalModulesUpdate";
            OracleHandler SQLH = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("ModuleIDs", ModuleIDs));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsActives", IsActives));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("UpdatedBy", UpdatedBy));

                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }







        public List<PortalInfo> GetParentPortalList()
        {
            string sp = "usp_PortalGetParent";
            OracleHandler SQLH = new OracleHandler();
            try
            {
                return SQLH.ExecuteAsList<PortalInfo>(sp);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }




    }
}