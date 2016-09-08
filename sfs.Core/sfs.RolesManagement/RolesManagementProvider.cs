using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace SageFrame.RolesManagement
{
    public class RolesManagementProvider
    {
        public RolesManagementInfo GetRoleIDByRoleName(string RoleName)
        {
            //SqlDataReader reader = null;
            OracleDataReader reader = null;
            try
            {

                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("v_Rolename", RoleName));
                reader = SQLH.ExecuteAsDataReader("sp_GetRoleIDByRoleName", ParamCollInput);
                RolesManagementInfo objList = new RolesManagementInfo();
                while (reader.Read())
                {
                    //objList.ApplicationId = new Guid(reader["ApplicationId"].ToString());
                    //objList.RoleId = new Guid(reader["RoleId"].ToString());
                    //objList.RoleName = reader["RoleName"].ToString();
                    //objList.LoweredRoleName = reader["LoweredRoleName"].ToString();
                    //objList.Description = reader["Description"].ToString();
                    objList.ApplicationId = new Guid(reader.GetValue(0) as byte[]);
                    objList.RoleId = new Guid(reader.GetValue(1)as byte[]);
                    objList.RoleName = reader.GetValue(2).ToString();
                    objList.LoweredRoleName = reader.GetValue(3).ToString();
                    objList.Description = reader.GetValue(4).ToString();
                }
                reader.Close();
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
        public List<RolesManagementInfo> PortalRoleList(int PortalID, bool IsAll, string Username)
        {
            try
            {
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsAll", IsAll));
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", Username));
                return SQLH.ExecuteAsList<RolesManagementInfo>("sp_PortalRoleList", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<RolesManagementInfo> GetPortalRoleSelectedList(int PortalID, string Username)
        {
            try
            {
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", Username));
                return SQLH.ExecuteAsList<RolesManagementInfo>("usp_GetPortalRoleSelectedList", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}