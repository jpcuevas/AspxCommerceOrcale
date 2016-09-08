using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.ModuleMessage
{
    public class ModuleMessageDataProvider
    {
        public static List<ModuleMessageInfo> GetAllModules()
        {
            try
            {
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                return SQLH.ExecuteAsList<ModuleMessageInfo>("usp_ModuleMessageGetModules");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void AddModuleMessage(ModuleMessageInfo objModuleMessage)
        {
            try
            {
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("ModuleID", objModuleMessage.ModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("Message", objModuleMessage.Message));
                ParamCollInput.Add(new KeyValuePair<string, object>("Culture", objModuleMessage.Culture));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsActive", objModuleMessage.IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("MessageType", objModuleMessage.MessageType));
                ParamCollInput.Add(new KeyValuePair<string, object>("MessageMode", objModuleMessage.MessageMode));
                ParamCollInput.Add(new KeyValuePair<string, object>("MessagePosition", objModuleMessage.MessagePosition));

                SQLH.ExecuteNonQuery("usp_ModuleMessageAdd", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static ModuleMessageInfo GetModuleMessage(int ModuleID, string Culture)
        {
            try
            {
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("ModuleID", ModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("Culture", Culture));

                return SQLH.ExecuteAsObject<ModuleMessageInfo>("usp_ModuleMessageGet", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static ModuleMessageInfo GetModuleMessageByUserModuleID(int UserModuleID, string Culture)
        {
            try
            {
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("Culture", Culture));
                
                //return SQLH.ExecuteAsObject<ModuleMessageInfo>("usp_ModuleMessageGetByUserModuleID", ParamCollInput);
                return SQLH.ExecuteAsObject<ModuleMessageInfo>("usp_ModuleMessageGetByUserModu", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void UpdateMessageStatus(int ModuleID, bool IsActive)
        {
            try
            {
                //SQLHandler SQLH = new SQLHandler();
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("ModuleID", ModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsActive", IsActive));

                SQLH.ExecuteNonQuery("usp_ModuleMessageUpdateStatus", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}