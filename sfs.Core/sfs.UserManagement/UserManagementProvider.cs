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
using Oracle.DataAccess.Client;
using System.Data;

#endregion


namespace SageFrame.UserManagement
{
    public class UserManagementProvider
    {
        public static void UpdateUsersChanges(string Usernames, string IsActives, int PortalId, string UpdatedBy)
        {
            string sp = "sp_UsersUpdateChanges";
            OracleHandler sagesql = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("Usernames", Usernames));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsActives", IsActives));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalId));
                ParamCollInput.Add(new KeyValuePair<string, object>("UpdatedBy", UpdatedBy));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void DeleteSelectedUser(string Usernames, int PortalId, string DeletedBy)
        {
            string sp = "sp_UsersDeleteSeleted";
            OracleHandler sagesql = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("Usernames", Usernames));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalId));
                ParamCollInput.Add(new KeyValuePair<string, object>("DeletedBy", DeletedBy));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<UserManagementInfo> GetUsernameByPortalIDAuto(string Prefix, int Count, int PortalID, string Username)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("Prefix", Prefix));
                ParamCollInput.Add(new KeyValuePair<string, object>("Count", Count));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", Username));

                return SQLH.ExecuteAsList<UserManagementInfo>("sp_GetUsernameByPortalIDAuto", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static ForgotPasswordInfo GetMessageTemplateByMessageTemplateTypeID(int MessageTemplateTypeID, int PortalID)
        {
            OracleDataReader reader = null;
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("MessageTemplateTypeID", MessageTemplateTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                


                //reader = SQLH.ExecuteAsDataReader("sp_MessageTemplateByMessageTemplateTypeID", ParamCollInput);
                reader = SQLH.ExecuteAsDataReader("sp_MessageTemplateByMessageTem", ParamCollInput);
                ForgotPasswordInfo objList = new ForgotPasswordInfo();

                while (reader.Read())
                {

                    objList.MessageTemplateID = int.Parse(reader["MessageTemplateID"].ToString());
                    objList.MessageTemplateTypeID = int.Parse(reader["MessageTemplateTypeID"].ToString());
                    objList.Subject = reader["Subject"].ToString();
                    objList.Body = reader["Body"].ToString();
                    objList.MailFrom = reader["MailFrom"].ToString();

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
        public static List<ForgotPasswordInfo> GetMessageTemplateListByMessageTemplateTypeID(int MessageTemplateTypeID, int PortalID)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("MessageTemplateTypeID", MessageTemplateTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));

                return SQLH.ExecuteAsList<ForgotPasswordInfo>("sp_MessageTemplateByMessageTemplateTypeID", ParamCollInput);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        
        
        public static DataTable GetPasswordRecoverySuccessfulTokenValue(string Username, int PortalID)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                DataSet ds = SQLH.ExecuteAsDataSet("sp_GetPasswordRecoverySuccessfulTokenValue", ParamCollInput);
                return ds.Tables[0];
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
         public static void DeactivateRecoveryCode(string Username, int PortalID)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                SQLH.ExecuteNonQuery("usp_PasswordRecoveryDeactivateCode", ParamCollInput);
              
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static DataTable GetActivationSuccessfulTokenValue(string Username, int PortalID)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));

                DataSet ds = SQLH.ExecuteAsDataSet("sp_GetActivationSuccessfulTokenValue", ParamCollInput);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        
        public static DataTable GetActivationTokenValue(string Username, int PortalID)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));

                DataSet ds = SQLH.ExecuteAsDataSet("sp_GetActivationTokenValue", ParamCollInput);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static DataTable  GetPasswordRecoveryTokenValue(string Username, int PortalID)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));

                DataSet ds= SQLH.ExecuteAsDataSet("sp_GetPasswordRecoveryTokenValue", ParamCollInput);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        
    
        public static ForgotPasswordInfo GetUsernameByActivationOrRecoveryCode(string Code, int PortalID)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("Code", Code));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));

                List<ForgotPasswordInfo> lstpwdinfo = new List<ForgotPasswordInfo>();
                lstpwdinfo = SQLH.ExecuteAsList<ForgotPasswordInfo>("sp_GetUsernameByActivationOrRecoveryCode", ParamCollInput);
                ForgotPasswordInfo objInfo = new ForgotPasswordInfo();              
                if (lstpwdinfo.Count > 0)
                {
                    return lstpwdinfo[0];
                }
                else
                {
                    objInfo.IsAlreadyUsed = true;
                    return objInfo;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
       

    }
}
