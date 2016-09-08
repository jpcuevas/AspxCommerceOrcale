using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;


namespace SageFrame.Security.Helpers
{
    /// <summary>
    /// Summary description for SageFrameSQLHelper
    /// </summary>
    public class SageFrameSQLHelper : OracleHandler
    {
        public SageFrameSQLHelper()
            : base()
        {
        }

        public List<KeyValuePair<int, string>> ExecuteNonQueryWithMultipleOutput(string StroredProcedureName, List<KeyValuePair<string, object>> InputParamColl, List<KeyValuePair<string, object>> OutPutParamColl)
        {
            OracleConnection OraConn = new OracleConnection(base.connectionString);
            try
            {
                OracleCommand OraCmd = new OracleCommand();
                OraCmd.Connection = OraConn;
                OraCmd.CommandText = StroredProcedureName;
                OraCmd.CommandType = CommandType.StoredProcedure;
                //Loop for Paramets
                foreach (KeyValuePair<string, object> kvp in InputParamColl)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = kvp.Key;
                    oracleParaMeter.Value = kvp.Value;
                    OraCmd.Parameters.Add(oracleParaMeter);
                }

                foreach (KeyValuePair<string, object> kvp in OutPutParamColl)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = kvp.Key;
                    oracleParaMeter.Value = kvp.Value;
                    oracleParaMeter.Direction = ParameterDirection.InputOutput;
                    oracleParaMeter.Size = 256;
                    OraCmd.Parameters.Add(oracleParaMeter);
                }
                OraConn.Open();
                OraCmd.ExecuteNonQuery();
                List<KeyValuePair<int, string>> lstRetValues = new List<KeyValuePair<int, string>>();
                for (int i = 0; i < OutPutParamColl.Count; i++)
                {
                    lstRetValues.Add(new KeyValuePair<int, string>(i, OraCmd.Parameters[InputParamColl.Count + i].Value.ToString()));
                }
                return lstRetValues;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                OraConn.Close();
            }
        }

        public List<KeyValuePair<int, string>> ExecuteNonQueryWithMultipleOutput(OracleTransaction transaction, CommandType commandType, string StroredProcedureName, List<KeyValuePair<string, object>> InputParamColl, List<KeyValuePair<string, object>> OutPutParamColl)
        {
            try
            {
                //create a command and prepare it for execution
                OracleCommand cmd = new OracleCommand();
                OracleHandler.PrepareCommand(cmd, transaction.Connection, transaction, commandType, StroredProcedureName);
                //Loop for Paramets
                foreach (KeyValuePair<string, object> kvp in InputParamColl)
                {
                    OracleParameter sqlParaMeter = new OracleParameter();
                    sqlParaMeter.IsNullable = true;
                    sqlParaMeter.ParameterName = kvp.Key;
                    sqlParaMeter.Value = kvp.Value;
                    cmd.Parameters.Add(sqlParaMeter);
                }

                foreach (KeyValuePair<string, object> kvp in OutPutParamColl)
                {
                    OracleParameter sqlParaMeter = new OracleParameter();
                    sqlParaMeter.IsNullable = true;
                    sqlParaMeter.ParameterName = kvp.Key;
                    sqlParaMeter.Value = kvp.Value;
                    sqlParaMeter.Direction = ParameterDirection.InputOutput;
                    sqlParaMeter.Size = 256;
                    cmd.Parameters.Add(sqlParaMeter);
                }

                cmd.ExecuteNonQuery();
                List<KeyValuePair<int, string>> lstRetValues = new List<KeyValuePair<int, string>>();
                for (int i = 0; i < OutPutParamColl.Count; i++)
                {
                    lstRetValues.Add(new KeyValuePair<int, string>(i, cmd.Parameters[InputParamColl.Count + i].Value.ToString()));
                }
                return lstRetValues;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}