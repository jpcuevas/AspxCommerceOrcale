using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using SageFrame.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using System.Reflection;
using SageFrame.Web.Utilities;

/// <summary>
/// Summary description for OracleHandler
/// </summary>

    public partial class OracleHandler
    {
        #region "Private Members"

        private string _objectQualifier = SystemSetting.ObjectQualifer;
        private string _databaseOwner = SystemSetting.DataBaseOwner;
        private string _connectionString = SystemSetting.SageFrameConnectionString;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public string objectQualifier
        {
            get { return _objectQualifier; }
            set { _objectQualifier = value; }
        }

        public string databaseOwner
        {
            get { return _databaseOwner; }
            set { _databaseOwner = value; }
        }

        public string connectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        #endregion

        #region "Transaction Methods"

        public void CommitTransaction(DbTransaction transaction)
        {
            try
            {
                transaction.Commit();
            }
            finally
            {
                if (transaction != null && transaction.Connection != null)
                {
                    transaction.Connection.Close();
                }
            }
        }

        public DbTransaction GetTransaction()
        {
            OracleConnection Conn = new OracleConnection(this.connectionString);
            //SqlConnection Conn = new SqlConnection(this.connectionString);
            Conn.Open();
            OracleTransaction transaction = Conn.BeginTransaction();
            //SqlTransaction transaction = Conn.BeginTransaction();
            return transaction;
        }

        public void RollbackTransaction(DbTransaction transaction)
        {
            try
            {
                transaction.Rollback();
            }
            finally
            {
                if (transaction != null && transaction.Connection != null)
                {
                    transaction.Connection.Close();
                }
            }
        }

        #endregion

        #region Using Transaction Method
        
        public static void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText)
        {

            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //associate the connection with the command
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = commandType;
            command.CommandText = commandText;
            return;
        }

        public int ExecuteNonQuery(OracleTransaction transaction, CommandType commandType, string commandText, List<KeyValuePair<string, object>> ParaMeterCollection, string outParamName)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText);

            for (int i = 0; i < ParaMeterCollection.Count; i++)
            {

                OracleParameter oracleParaMeter = new OracleParameter();
                oracleParaMeter.IsNullable = true;
                oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                oracleParaMeter.Value = ParaMeterCollection[i].Value;
                cmd.Parameters.Add(oracleParaMeter);
            }
            cmd.Parameters.Add(new OracleParameter(outParamName, OracleDbType.Int32));
            cmd.Parameters[outParamName].Direction = ParameterDirection.Output;
            //finally, execute the command.
            cmd.ExecuteNonQuery();
            //int id = (int)cmd.Parameters[outParamName].Value;
            int id = int.Parse(cmd.Parameters[outParamName].Value.ToString());
            // detach the OracleParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();
            return id;
        }

        public void ExecuteNonQuery(OracleTransaction transaction, CommandType commandType, string commandText, List<KeyValuePair<string, object>> ParaMeterCollection)
        {
            //create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText);

            for (int i = 0; i < ParaMeterCollection.Count; i++)
            {
                OracleParameter oracleParaMeter = new OracleParameter();
                oracleParaMeter.IsNullable = true;
                oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                oracleParaMeter.Value = ParaMeterCollection[i].Value;
                cmd.Parameters.Add(oracleParaMeter);
            }

            //finally, execute the command.
            cmd.ExecuteNonQuery();

            // detach the OracleParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();




        }

        #endregion

        #region "SQL Execute Methods"

        private void ExecuteADOScript(OracleTransaction trans, string SQL)
        {
            OracleConnection connection = trans.Connection;
            //Create a new command (with no timeout)
            OracleCommand command = new OracleCommand(SQL, trans.Connection);
            command.Transaction = trans;
            command.CommandTimeout = 0;
            command.ExecuteNonQuery();
        }

        private void ExecuteADOScript(string SQL)
        {
            OracleConnection connection = new OracleConnection(this.connectionString);
            //Create a new command (with no timeout)
            OracleCommand command = new OracleCommand(SQL, connection);
            connection.Open();
            command.CommandTimeout = 0;
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void ExecuteADOScript(string SQL, string ConnectionString)
        {
            OracleConnection connection = new OracleConnection(ConnectionString);
            //Create a new command (with no timeout)
            OracleCommand command = new OracleCommand(SQL, connection);
            connection.Open();
            command.CommandTimeout = 0;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string ExecuteScript(string Script)
        {
            return ExecuteScript(Script, false);
        }

        public DataSet ExecuteScriptAsDataSet(string SQL)
        {
            OracleConnection SQLConn = new OracleConnection(this._connectionString);
            try
            {

                OracleCommand OraCmd = new OracleCommand();
                OracleDataAdapter OraAdapter = new OracleDataAdapter();
                DataSet Orads = new DataSet();
                OraCmd.Connection = SQLConn;
                OraCmd.CommandText = SQL;
                OraCmd.CommandType = CommandType.Text;
                OraAdapter.SelectCommand = OraCmd;
                SQLConn.Open();
                OraAdapter.Fill(Orads);
                SQLConn.Close();
                return Orads;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                SQLConn.Close();
            }
        }

        public string ExecuteScript(string Script, DbTransaction transaction)
        {
            string SQL = string.Empty;
            string Exceptions = string.Empty;
            string Delimiter = "GO" + Environment.NewLine;//
            
            string[] arrSQL = Microsoft.VisualBasic.Strings.Split(Script, Delimiter, -1, Microsoft.VisualBasic.CompareMethod.Text);
            bool IgnoreErrors;
            foreach (string SQLforeach in arrSQL)
            {
                if (!string.IsNullOrEmpty(SQLforeach))
                {
                    //script dynamic substitution
                    SQL = SQLforeach;
                    SQL = SQL.Replace("{databaseOwner}", this.databaseOwner);
                    SQL = SQL.Replace("{objectQualifier}", this.objectQualifier);

                    IgnoreErrors = false;

                    if (SQL.Trim().StartsWith("{IgnoreError}"))
                    {
                        IgnoreErrors = true;
                        SQL = SQL.Replace("{IgnoreError}", "");
                    }
                    try
                    {
                        ExecuteADOScript(transaction as OracleTransaction, SQL);
                    }
                    catch (Exception ex)
                    {
                        if (!IgnoreErrors)
                        {
                            Exceptions += ex.ToString() + Environment.NewLine + Environment.NewLine + SQL + Environment.NewLine + Environment.NewLine;
                        }
                    }
                }
            }
            return Exceptions;
        }

        public string ExecuteScript(string Script, bool UseTransactions)
        {
            string SQL = string.Empty;
            string Exceptions = string.Empty;

            if (UseTransactions)
            {
                DbTransaction transaction = GetTransaction();
                try
                {
                    Exceptions += ExecuteScript(Script, transaction);

                    if (Exceptions.Length == 0)
                    {
                        //No exceptions so go ahead and commit
                        CommitTransaction(transaction);
                    }
                    else
                    {
                        //Found exceptions, so rollback db
                        RollbackTransaction(transaction);
                        Exceptions += "SQL Execution failed.  Database was rolled back" + Environment.NewLine + Environment.NewLine + SQL + Environment.NewLine + Environment.NewLine;
                    }
                }
                finally
                {
                    if (transaction.Connection != null)
                    {

                        transaction.Connection.Close();
                    }
                }
            }
            else
            {
                string Delimiter = "GO" + Environment.NewLine;
                string[] arrSQL = Microsoft.VisualBasic.Strings.Split(Script, Delimiter, -1, CompareMethod.Text);
                foreach (string SQLforeach in arrSQL)
                {
                    if (!string.IsNullOrEmpty(SQLforeach))
                    {
                        SQL = SQLforeach;
                        SQL = SQL.Replace("{databaseOwner}", this.databaseOwner);
                        SQL = SQL.Replace("{objectQualifier}", this.objectQualifier);
                        try
                        {
                            ExecuteADOScript(SQL);
                        }
                        catch (Exception ex)
                        {
                            Exceptions += ex.ToString() + Environment.NewLine + Environment.NewLine + SQL + Environment.NewLine + Environment.NewLine;
                        }
                    }
                }
            }

            return Exceptions;
        }

        public string ExecuteInstallScript(string Script, string ConnectionString)
        {
            string SQL = string.Empty;
            string Exceptions = string.Empty;

            string Delimiter = "GO" + Environment.NewLine;
            string[] arrSQL = Microsoft.VisualBasic.Strings.Split(Script, Delimiter, -1, CompareMethod.Text);
           
            
            foreach (string SQLforeach in arrSQL)
            {
                if (!string.IsNullOrEmpty(SQLforeach))
                {
                    SQL = SQLforeach;
                    SQL = SQL.Replace("{databaseOwner}", this.databaseOwner);
                    SQL = SQL.Replace("{objectQualifier}", this.objectQualifier);
                    try
                    {
                        ExecuteADOScript(SQL, ConnectionString);
                    }
                    catch (Exception ex)
                    {
                        Exceptions += ex.ToString() + Environment.NewLine + Environment.NewLine + SQL + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            return Exceptions;
        }
        #endregion

        #region "Public Methods"

        /// <summary>
        /// RollBack Module Installation If Error Occur During Module Installation
        /// </summary>
        /// <param name="ModuleID">ModuleID</param>
        /// <param name="PortalID">PortalID</param>
        public void ModulesRollBack(int ModuleID, int PortalID)
        {
            try
            {
                OracleConnection OraConn = new OracleConnection(this._connectionString);
                OracleCommand ORACmd = new OracleCommand();
                ORACmd.Connection = OraConn;
                ORACmd.CommandText = "sp_ModulesRollBack";
                ORACmd.CommandType = CommandType.StoredProcedure;
                ORACmd.Parameters.Add(new OracleParameter("ModuleID", ModuleID));
                ORACmd.Parameters.Add(new OracleParameter("PortalID", PortalID));
                OraConn.Open();
                ORACmd.ExecuteNonQuery();
                OraConn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returning Bool After Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName">Store Procedure Name</param>
        /// <param name="ParaMeterCollection"> Parameter Collection</param>
        /// <param name="OutPutParamerterName">Out Parameter Collection</param>
        /// <returns>Bool</returns>
        public bool ExecuteNonQueryAsBool(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection, string OutPutParamerterName)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    oraCmd.Parameters.Add(oracleParaMeter);
                }
                //End of for loop
                //SQLCmd.Parameters.Add(new SqlParameter(OutPutParamerterName, SqlDbType.Bit));
                //SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;

                oraCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Varchar2,200));
                oraCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;

                oraConec.Open();
                oraCmd.ExecuteNonQuery();
               // bool ReturnValue = (bool)oraCmd.Parameters[OutPutParamerterName].Value;
                bool ReturnValue = Convert.ToBoolean(oraCmd.Parameters[OutPutParamerterName].Value.ToString());

                return ReturnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Returning Bool After Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName">Store Procedure Name</param>
        /// <param name="ParaMeterCollection">Parameter Collection</param>
        /// <param name="OutPutParamerterName">OutPut Parameter Name</param>
        /// <param name="OutPutParamerterValue">OutPut Parameter Value</param>
        /// <returns>Bool</returns>
        public bool ExecuteNonQueryAsBool(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection, string OutPutParamerterName, object OutPutParamerterValue)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    oraCmd.Parameters.Add(oracleParaMeter);
                }
                //End of for loop
                oraCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Char));
                oraCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                oraCmd.Parameters[OutPutParamerterName].Value = OutPutParamerterValue;
                //SQLCmd.Parameters.Add(new SqlParameter(OutPutParamerterName, SqlDbType.Bit));
                //SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                //SQLCmd.Parameters[OutPutParamerterName].Value = OutPutParamerterValue;

                oraConec.Open();
                oraCmd.ExecuteNonQuery();
                bool ReturnValue = (bool)oraCmd.Parameters[OutPutParamerterName].Value;
                return ReturnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName">Store Procedure Name In String</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters<KeyValuePair<string, object>> </param>
        public void ExecuteNonQuery(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {
                oraConec.Open();
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;

                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    oraCmd.Parameters.Add(oracleParaMeter);
                }
                //End of for loop

                //oraConec.Open();
                oraCmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName">Store Procedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters <KeyValuePair<string, string>> </param>
        public void ExecuteNonQuery(string StroredProcedureName, List<KeyValuePair<string, string>> ParaMeterCollection)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);


            try
            {
                oraConec.Open();
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;


                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    //SQLCmd.Parameters.Add(new SqlParameter(ParaMeterCollection[i].Key, ParaMeterCollection[i].Value));
                    oraCmd.Parameters.Add(new OracleParameter(ParaMeterCollection[i].Key, OracleDbType.Varchar2, ParaMeterCollection[i].Value, ParameterDirection.Input));
                }
                //End of for loop

                //SQLConn.Open();
                oraCmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName">Store Procedure Name</param>
        public void ExecuteNonQuery(string StroredProcedureName)
        {
            OracleConnection ORAConn = new OracleConnection(this._connectionString);
            try
            {
                OracleCommand ORACmd = new OracleCommand();
                ORACmd.Connection = ORAConn;
                ORACmd.CommandText = StroredProcedureName;
                ORACmd.CommandType = CommandType.StoredProcedure;
                ORAConn.Open();
                ORACmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ORAConn.Close();
            }
        }

        /// <summary>
        /// Accept only int, Int16, long, DateTime, string (NVarcha of size  50),
        /// bool, decimal ( of size 16,2), float
        /// </summary>
        /// <typeparam name="T">Return the given type of object</typeparam>
        /// <param name="StroredProcedureName">Accet SQL procedure name in string</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection for parameters</param>
        /// <param name="OutPutParamerterName">Accept Output parameter for the stored procedures</param>
        /// <returns></returns>
        public T ExecuteNonQueryAsGivenType<T>(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection, string OutPutParamerterName)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {
                oraConec.Open();
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                //oraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    oraCmd.Parameters.Add(oracleParaMeter);
                }
                //End of for loop                
                oraCmd = AddOutPutParametrofGivenType<T>(oraCmd, OutPutParamerterName);
                //SQLConn.Open();
                oraCmd.ExecuteNonQuery();
                if (typeof(T) == typeof(string))
                {
                    Object test = ((Oracle.DataAccess.Types.OracleString)(oraCmd.Parameters[OutPutParamerterName].Value)).Value;
                    return (T)test;
                }
                else if (typeof(T) == typeof(Int32))
                {
                    //Object test = ((Oracle.DataAccess.Types.OracleString)(oraCmd.Parameters[OutPutParamerterName].Value)).Value;
                    //return (T)test;
                    Object test = int.Parse(oraCmd.Parameters[OutPutParamerterName].Value.ToString());
                    return (T)test;
                }
                else if (typeof(T) == typeof(decimal))
                {
                    //Object test = ((Oracle.DataAccess.Types.OracleString)(oraCmd.Parameters[OutPutParamerterName].Value)).Value;
                    //return (T)test;
                    Object test = decimal.Parse(oraCmd.Parameters[OutPutParamerterName].Value.ToString());
                    return (T)test;
                }
                else
                {
                    return (T)oraCmd.Parameters[OutPutParamerterName].Value; 
                }
                //return (T)((Oracle.DataAccess.Types.OracleString)(oraCmd.Parameters[OutPutParamerterName].Value)).Value; 
                return (T)oraCmd.Parameters[OutPutParamerterName].Value; 
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Accept only int, Int16, long, DateTime, string (NVarcha of size  50),
        /// bool, decimal ( of size 16,2), float
        /// </summary>
        /// <typeparam name="T">Return the given type of object</typeparam>
        /// <param name="StroredProcedureName">Accet SQL procedure name in string</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection for parameters</param>
        /// <param name="OutPutParamerterName">Accept Output parameter for the stored procedures</param>
        /// <returns></returns>
        public T ExecuteNonQueryAsGivenType<T>(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection, string OutPutParamerterName, object OutPutParamerterValue)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {
                oraConec.Open();
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    oraCmd.Parameters.Add(oracleParaMeter);
                }
                //End of for loop                
                oraCmd = AddOutPutParametrofGivenType<T>(oraCmd, OutPutParamerterName, OutPutParamerterValue);
                //SQLConn.Open();
                oraCmd.ExecuteNonQuery();
                return (T)oraCmd.Parameters[OutPutParamerterName].Value; 
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName">Store Procedure Name In String</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <param name="OutPutParamerterName">Accept OutPut Key Value Collection For Parameters</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection, string OutPutParamerterName)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);
            try
            {

                oraConec.Open();
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;

                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    //OracleParameterCollection oParms = new OracleCommand().Parameters;
                    OracleParameter oracleParaMeter = new OracleParameter ();
                    //oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    if(ParaMeterCollection[i].Value.Equals(true))
                    {
                        oracleParaMeter.Value = "y";
                    }else if(ParaMeterCollection[i].Value.Equals(false)){
                        oracleParaMeter.Value = "N";
                    }else{
                        oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    }
                    
                    oraCmd.Parameters.Add(oracleParaMeter);
                }
                //End of for loop
                //SQLCmd.Parameters.Add(new SqlParameter(OutPutParamerterName, SqlDbType.Int));
                //SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;

                oraCmd.Parameters.Add(OutPutParamerterName, OracleDbType.Int32, ParameterDirection.Output);




                //oraConec.Open();
                oraCmd.ExecuteNonQuery();
                int ReturnValue = int.Parse(oraCmd.Parameters[OutPutParamerterName].Value.ToString());
                

                oraConec.Close();
                return ReturnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName"> Store Procedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <param name="OutPutParamerterName">Accept Output Key Value Collection For Parameters</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string StroredProcedureName, List<KeyValuePair<string, string>> ParaMeterCollection, string OutPutParamerterName)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {

                oraConec.Open();
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    //   SQLCmd.Parameters.Add(new SqlParameter(ParaMeterCollection[i].Key, ParaMeterCollection[i].Value));
                    oraCmd.Parameters.Add(new OracleParameter(ParaMeterCollection[i].Key, ParaMeterCollection[i].Value));
                }
                //End of for loop
                //SQLCmd.Parameters.Add(new SqlParameter(OutPutParamerterName, SqlDbType.Int));
                //SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;

                oraCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int32));
                oraCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;

                //SQLConn.Open();
                oraCmd.ExecuteNonQuery();
                //int ReturnValue = (int)oraCmd.Parameters[OutPutParamerterName].Value;
                int ReturnValue = int.Parse(oraCmd.Parameters[OutPutParamerterName].Value.ToString());
                oraConec.Close();
                return ReturnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName">Store Procedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <param name="OutPutParamerterName">Accept Output  For Parameters Name</param>
        /// <param name="OutPutParamerterValue">Accept OutPut For Parameters Value</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string StroredProcedureName, List<KeyValuePair<string, string>> ParaMeterCollection, string OutPutParamerterName, object OutPutParamerterValue)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {

                oraConec.Open();
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    //SQLCmd.Parameters.Add(new SqlParameter(ParaMeterCollection[i].Key, ParaMeterCollection[i].Value));
                    oraCmd.Parameters.Add(new OracleParameter(ParaMeterCollection[i].Key, ParaMeterCollection[i].Value));
                }
                //End of for loop
                //SQLCmd.Parameters.Add(new SqlParameter(OutPutParamerterName, SqlDbType.Int));
                oraCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int32));
                //SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                oraCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                //SQLCmd.Parameters[OutPutParamerterName].Value = OutPutParamerterValue;
                oraCmd.Parameters[OutPutParamerterName].Value = OutPutParamerterValue;

                //SQLConn.Open();
                oraCmd.ExecuteNonQuery();
                int ReturnValue = (int)oraCmd.Parameters[OutPutParamerterName].Value;
                oraConec.Close();
                return ReturnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName">Store Procedure Name</param>
        /// <param name="OutPutParamerterName">Accept Output For Parameters Name</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string StroredProcedureName, string OutPutParamerterName)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["orclConnString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

           
            try
            {

                //SqlCommand SQLCmd = new SqlCommand();
                OracleCommand ORACmd = new OracleCommand();
                //SQLCmd.Connection = SQLConn;
                ORACmd.Connection = oraConec;
                //SQLCmd.CommandText = StroredProcedureName;
                ORACmd.CommandText = StroredProcedureName;
                ORACmd.CommandType = CommandType.StoredProcedure;
                ORACmd.BindByName = true;
                //SQLCmd.Parameters.Add(new SqlParameter(OutPutParamerterName, SqlDbType.Int));
                ORACmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int32));
                //SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                ORACmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                //SQLConn.Open();
                ORACmd.ExecuteNonQuery();
                int ReturnValue = (int)ORACmd.Parameters[OutPutParamerterName].Value;
                oraConec.Close();
                return ReturnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <param name="OutPutParamerterName">Accept Output For Parameter Name</param>
        /// <param name="OutPutParamerterValue">Accept Output For Parameter Value</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string StroredProcedureName, string OutPutParamerterName, object OutPutParamerterValue)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["orclConnString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {
                //SqlCommand SQLCmd = new SqlCommand();
                OracleCommand ORACmd = new OracleCommand();
                //SQLCmd.Connection = SQLConn;
                ORACmd.Connection = oraConec;
                ORACmd.CommandText = StroredProcedureName;
                ORACmd.CommandType = CommandType.StoredProcedure;
                ORACmd.BindByName = true;
                //SQLCmd.Parameters.Add(new SqlParameter(OutPutParamerterName, SqlDbType.Int));
                ORACmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int32));
                //SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                ORACmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                //SQLCmd.Parameters[OutPutParamerterName].Value = OutPutParamerterValue;
                ORACmd.Parameters[OutPutParamerterName].Value = OutPutParamerterValue;
                oraConec.Open();
                ORACmd.ExecuteNonQuery();
                int ReturnValue = (int)ORACmd.Parameters[OutPutParamerterName].Value;
                oraConec.Close();
                return ReturnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute As DataSet
        /// </summary>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <returns></returns>
        public DataSet ExecuteAsDataSet(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {
                //SqlCommand SQLCmd = new SqlCommand();
                OracleCommand ORACmd = new OracleCommand();
                OracleDataAdapter ORAAdapter = new OracleDataAdapter();
                DataSet ORAds = new DataSet();
                ORACmd.Connection = oraConec;
                ORACmd.CommandText = StroredProcedureName;
                ORACmd.CommandType = CommandType.StoredProcedure;
                ORACmd.BindByName = true;

                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    ORACmd.Parameters.Add(oracleParaMeter);
                }
                ORACmd.Parameters.Add("cv_1", OracleDbType.RefCursor, ParameterDirection.Output);
                //End of for loop

                ORAAdapter.SelectCommand = ORACmd;
                oraConec.Open();
                ORAAdapter.Fill(ORAds);
                oraConec.Close();
                return ORAds;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute As DataSet
        /// </summary>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <returns></returns>
        public DataSet ExecuteAsDataSet(string StroredProcedureName, List<KeyValuePair<string, string>> ParaMeterCollection)
        {
            //SqlConnection SQLConn = new SqlConnection(this._connectionString);
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);
            try
            {

                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;

                //SqlCommand SQLCmd = new SqlCommand();
                //SqlDataAdapter SQLAdapter = new SqlDataAdapter();
                DataSet Oracleds = new DataSet();
                //SQLCmd.Connection = SQLConn;
                //SQLCmd.CommandText = StroredProcedureName;
                //SQLCmd.CommandType = CommandType.StoredProcedure;

                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    //SQLCmd.Parameters.Add(new SqlParameter(ParaMeterCollection[i].Key, ParaMeterCollection[i].Value));
                    oraCmd.Parameters.Add(new OracleParameter(ParaMeterCollection[i].Key, OracleDbType.Varchar2, ParaMeterCollection[i].Value, ParameterDirection.Input));
                }

                oraCmd.Parameters.Add("cv_1", OracleDbType.RefCursor, ParameterDirection.Output);
                if (StroredProcedureName == "usp_GetPageSetByPageSEONamAdmi") {
                    oraCmd.Parameters.Add("cv_2", OracleDbType.RefCursor, ParameterDirection.Output);
                    oraCmd.Parameters.Add("cv_3", OracleDbType.RefCursor, ParameterDirection.Output);
                    //oraCmd.Parameters.Add("cv_4", OracleDbType.RefCursor, ParameterDirection.Output);
                }
                //End of for loop
                OracleDataAdapter orclData = new OracleDataAdapter(oraCmd);
                orclData.SelectCommand = oraCmd;
                //SQLAdapter.SelectCommand = SQLCmd;
                orclData.Fill(Oracleds);
                //SQLConn.Open();
                //SQLAdapter.Fill(SQLds);
                oraConec.Close();
                return Oracleds;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (oraConec.State != ConnectionState.Closed)
                    oraConec.Close();

            }
        }

        /// <summary>
        /// Execute As DataReader
        /// </summary>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <returns></returns>
        public OracleDataReader ExecuteAsDataReader(string StroredProcedureName)
        {
            
            try
            {
                OracleConnection ORAConn = new OracleConnection(this._connectionString);
                OracleDataReader ORAReader;
                OracleCommand ORACmd = new OracleCommand();
                ORACmd.Connection = ORAConn;
                ORACmd.CommandText = StroredProcedureName;
                ORACmd.CommandType = CommandType.StoredProcedure;
                ORACmd.BindByName = true;
                ORAConn.Open();
                //if (returnValue)
                //{
                    ORACmd.Parameters.Add("cv_1", OracleDbType.RefCursor, ParameterDirection.Output);
                //}
                ORAReader = ORACmd.ExecuteReader(CommandBehavior.CloseConnection);

                return ORAReader;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Execute As DataReader
        /// </summary>
        /// <param name="StroredProcedureName">Store Procedure Name </param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <returns></returns>
        public OracleDataReader ExecuteAsDataReader(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection)
        {
            try
            {

                OracleConnection OraConn = new OracleConnection(this._connectionString);
                OracleDataReader OraReader;
                OracleCommand OraCmd = new OracleCommand();
                OraCmd.Connection = OraConn;
                OraCmd.CommandText = StroredProcedureName;
                OraCmd.CommandType = CommandType.StoredProcedure;
                //OraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    OraCmd.Parameters.Add(oracleParaMeter);
                }

                OraCmd.Parameters.Add("cv_1", OracleDbType.RefCursor, ParameterDirection.Output);
                //End of for loop
                OraConn.Open();
                OraReader = OraCmd.ExecuteReader(CommandBehavior.CloseConnection);
                //OraCmd.ExecuteReader(CommandBehavior.CloseConnection);

                // Construct an OracleDataReader from the REF CURSOR
                //OracleDataReader OraReader = ((OracleRefCursor)p1.Value).GetDataReader();
                return OraReader;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Execute As DataReader
        /// </summary>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <returns></returns>
        public OracleDataReader ExecuteAsDataReader(string StroredProcedureName, List<KeyValuePair<string, string>> ParaMeterCollection)
        {
            try
            {
                OracleConnection OraLConn = new OracleConnection(this._connectionString);
                OracleDataReader OraReader;
                OracleCommand OraCmd = new OracleCommand();
                OraCmd.Connection = OraLConn;
                OraCmd.CommandText = StroredProcedureName;
                OraCmd.CommandType = CommandType.StoredProcedure;
                OraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OraCmd.Parameters.Add(new OracleParameter(ParaMeterCollection[i].Key, ParaMeterCollection[i].Value));
                }
                //End of for loop
                OraLConn.Open();
                OraReader = OraCmd.ExecuteReader(CommandBehavior.CloseConnection);
                return OraReader;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Execute As Object
        /// </summary>
        /// <typeparam name="T"><T></typeparam>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <returns></returns>
        public T ExecuteAsObject<T>(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {

                //SqlDataReader SQLReader;
                //SqlCommand SQLCmd = new SqlCommand();
                //SQLCmd.Connection = SQLConn;
                //SQLCmd.CommandText = StroredProcedureName;
                //SQLCmd.CommandType = CommandType.StoredProcedure;

                
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;
                //Loop for Parameters
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    oraCmd.Parameters.Add(oracleParaMeter);
                }
                //End of for loop

                oraCmd.Parameters.Add("cv_1", OracleDbType.RefCursor, ParameterDirection.Output);
                oraConec.Open();
                OracleDataReader dr = oraCmd.ExecuteReader(CommandBehavior.CloseConnection);
                //dr.Read();

                //SQLConn.Open();
                //SQLReader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection);
                //ArrayList arrColl = DataSourceHelper.FillCollection(SQLReader, typeof(T));

                List<T> mList = new List<T>();
                ArrayList arrColl = DataSourceHelper.FillCollection(dr, typeof(T));

                oraConec.Close();
                if (dr != null)
                {
                    dr.Close();
                }
                if (arrColl != null && arrColl.Count > 0)
                {
                    return (T)arrColl[0];
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute As Object
        /// </summary>
        /// <typeparam name="T"><T></typeparam>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <returns></returns>
        public T ExecuteAsObject<T>(string StroredProcedureName, List<KeyValuePair<string, string>> ParaMeterCollection)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["orclConnString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {

                oraConec.Open();
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;

                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    //SQLCmd.Parameters.Add(new SqlParameter(ParaMeterCollection[i].Key, ParaMeterCollection[i].Value));
                    oraCmd.Parameters.Add(new OracleParameter(ParaMeterCollection[i].Key, ParaMeterCollection[i].Value));
                }
                //End of for loop
                //SQLConn.Open();
                //SQLReader = SQLCmd.ExecuteReader();
                OracleDataReader dr = oraCmd.ExecuteReader();
                //dr.Read(); try

                ArrayList arrColl = DataSourceHelper.FillCollection(dr, typeof(T));
                oraConec.Close();
                if (dr != null)
                {
                    dr.Close();
                }
                if (arrColl != null && arrColl.Count > 0)
                {
                    return (T)arrColl[0];
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute As Object
        /// </summary>
        /// <typeparam name="T"><T></typeparam>
        /// <param name="StroredProcedureName">Accept Key Value Collection For Parameters</param>
        /// <returns></returns>
        public T ExecuteAsObject<T>(string StroredProcedureName)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);
            try
            {

                // SqlDataReader SQLReader;
                OracleDataReader OraReader;
                //SqlCommand SQLCmd = new SqlCommand();
                OracleCommand OraCmd = new OracleCommand();
                //SQLCmd.Connection = SQLConn;
                OraCmd.Connection = oraConec;
                OraCmd.CommandText = StroredProcedureName;
                OraCmd.CommandType = CommandType.StoredProcedure;
                OraCmd.BindByName = true;
                oraConec.Open();
                OraReader = OraCmd.ExecuteReader();
                ArrayList arrColl = DataSourceHelper.FillCollection(OraReader, typeof(T));
                oraConec.Close();
                if (OraReader != null)
                {
                    OraReader.Close();
                }
                if (arrColl != null && arrColl.Count > 0)
                {
                    return (T)arrColl[0];
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute As DataSet
        /// </summary>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <returns></returns>
        public DataSet ExecuteAsDataSet(string StroredProcedureName)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);
            try
            {

                OracleCommand oraCmd = new OracleCommand(StroredProcedureName);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;
                DataSet Oracleds = new DataSet();
                OracleDataAdapter orclData = new OracleDataAdapter(oraCmd);
                orclData.Fill(Oracleds);
                oraConec.Close();
                return Oracleds;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute SQL
        /// </summary>
        /// <param name="SQL">SQL query in string</param>
        /// <returns></returns>
        public DataTable ExecuteSQL(string SQL)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {
                SQL = SQL.Replace("{databaseOwner}", this.databaseOwner);
                SQL = SQL.Replace("{objectQualifier}", this.objectQualifier);

                //SqlCommand SQLCmd = new SqlCommand();
                OracleCommand OraCmd = new OracleCommand();
                //SqlDataAdapter SQLAdapter = new SqlDataAdapter();
                OracleDataAdapter OraAdapter = new OracleDataAdapter();
                DataSet Orads = new DataSet();
                OraCmd.Connection = oraConec;
                OraCmd.CommandText = SQL;
                OraCmd.CommandType = CommandType.Text;
                OraAdapter.SelectCommand = OraCmd;
                oraConec.Open();
                OraAdapter.Fill(Orads);
                oraConec.Close();
                DataTable dt = null;// = new DataTable();
                if (Orads != null && Orads.Tables != null && Orads.Tables[0] != null)
                {
                    dt = Orads.Tables[0];
                }
                return dt;
            }
            catch
            {
                DataTable dt = null;
                return dt;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute As list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <param name="ParaMeterCollection"></param>
        /// <returns></returns>
        public List<T> ExecuteAsList<T>(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {

                //SqlDataReader SQLReader;
                //SqlCommand SQLCmd = new SqlCommand();
                //SQLCmd.Connection = SQLConn;
                //SQLCmd.CommandText = StroredProcedureName;
                //SQLCmd.CommandType = CommandType.StoredProcedure;

                
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;
                //oraCmd.BindByName = true;


                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    oraCmd.Parameters.Add(oracleParaMeter);

                }
                //End of for loop
                oraCmd.Parameters.Add("cv_1", OracleDbType.RefCursor, ParameterDirection.Output);
                oraConec.Open();
                OracleDataReader dr = oraCmd.ExecuteReader();
                //dr.Read();////////////////
                //SQLConn.Open();
                //SQLReader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection); //datareader automatically closes the SQL connection
                List<T> mList = new List<T>();
                mList = DataSourceHelper.FillCollection<T>(dr);
                if (dr != null)
                {
                    dr.Close();
                }
                oraConec.Close();
                return mList;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute As List
        /// </summary>
        /// <typeparam name="T"><T></typeparam>
        /// <param name="StroredProcedureName">Store Procedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <returns></returns>
        public List<T> ExecuteAsList<T>(string StroredProcedureName, List<KeyValuePair<string, string>> ParaMeterCollection)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);

            try
            {
                //SqlDataReader SQLReader;
                OracleDataReader OraReader;
                //SqlCommand SQLCmd = new SqlCommand();
                OracleCommand OraCmd = new OracleCommand();
                OraCmd.Connection = oraConec;
                OraCmd.CommandText = StroredProcedureName;
                OraCmd.CommandType = CommandType.StoredProcedure;
                OraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    OraCmd.Parameters.Add(oracleParaMeter);
                }
                OraCmd.Parameters.Add("cv_1", OracleDbType.RefCursor, ParameterDirection.Output);
                //End of for loop
                oraConec.Open();
                OraReader = OraCmd.ExecuteReader(CommandBehavior.CloseConnection); //datareader automatically closes the SQL connection
                List<T> mList = new List<T>();
                mList = DataSourceHelper.FillCollection<T>(OraReader);
                if (OraReader != null)
                {
                    OraReader.Close();
                }
                oraConec.Close();
                return mList;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }


        /// <summary>
        /// Execute As List
        /// </summary>
        /// <typeparam name="T"><T></typeparam>
        /// <param name="StroredProcedureName">Storedprocedure Name</param>
        /// <returns></returns>
        public List<T> ExecuteAsList<T>(string StroredProcedureName)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);
            try
            {

                oraConec.Open();
               
                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                //oraCmd.BindByName = true;
                oraCmd.Parameters.Add("cv_1", OracleDbType.RefCursor, ParameterDirection.Output);
                //OracleDataAdapter dr = new OracleDataAdapter(oraCmd);
                OracleDataReader dr = oraCmd.ExecuteReader();
                //dr.Read();

                //SQLConn.Open();
                //SQLReader = SQLCmd.ExecuteReader(CommandBehavior.CloseConnection); //datareader automatically closes the SQL connection
                List<T> mList = new List<T>();
                mList = DataSourceHelper.FillCollection<T>(dr);

                if (dr != null)
                {
                    dr.Close();
                }
                oraConec.Close();
                return mList;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute As Scalar 
        /// </summary>
        /// <typeparam name="T"><T></typeparam>
        /// <param name="StroredProcedureName">StoreProcedure Name</param>
        /// <param name="ParaMeterCollection">Accept Key Value Collection For Parameters</param>
        /// <returns></returns>
        public T ExecuteAsScalar<T>(string StroredProcedureName, List<KeyValuePair<string, object>> ParaMeterCollection)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);
            try
            {
                //SqlCommand SQLCmd = new SqlCommand();
                //SQLCmd.Connection = SQLConn;
                //SQLCmd.CommandText = StroredProcedureName;
                // SQLCmd.CommandType = CommandType.StoredProcedure;


                OracleCommand oraCmd = new OracleCommand(StroredProcedureName, oraConec);
                oraCmd.CommandType = CommandType.StoredProcedure;
                oraCmd.BindByName = true;
                //Loop for Paramets
                for (int i = 0; i < ParaMeterCollection.Count; i++)
                {
                    OracleParameter oracleParaMeter = new OracleParameter();
                    oracleParaMeter.IsNullable = true;
                    oracleParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    oracleParaMeter.Value = ParaMeterCollection[i].Value;
                    oraCmd.Parameters.Add(oracleParaMeter);

                    //SqlParameter sqlParaMeter = new SqlParameter();
                    //sqlParaMeter.IsNullable = true;
                    //sqlParaMeter.ParameterName = ParaMeterCollection[i].Key;
                    //sqlParaMeter.Value = ParaMeterCollection[i].Value;
                    //SQLCmd.Parameters.Add(sqlParaMeter);
                }
                oraCmd.Parameters.Add("cv_1", OracleDbType.RefCursor, ParameterDirection.Output);
                //OracleDataAdapter dr = new OracleDataAdapter(oraCmd);
                //End of for loop
                oraConec.Open();
                return (T)oraCmd.ExecuteScalar();
                
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Execute As Scalar
        /// </summary>
        /// <typeparam name="T"><T></typeparam>
        /// <param name="StroredProcedureName">StoredProcedure Name</param>
        /// <returns></returns>
        public T ExecuteAsScalar<T>(string StroredProcedureName)
        {
            string orclConString = System.Configuration.ConfigurationManager.ConnectionStrings["orclConnString"].ConnectionString;
            OracleConnection oraConec = new OracleConnection(orclConString);
            try
            {
                //SqlCommand SQLCmd = new SqlCommand();
                OracleCommand OraCmd = new OracleCommand();
                //SQLCmd.Connection = SQLConn;
                OraCmd.Connection = oraConec;
                OraCmd.CommandText = StroredProcedureName;
                OraCmd.CommandType = CommandType.StoredProcedure;
                oraConec.Open();
                return (T)OraCmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oraConec.Close();
            }
        }

        /// <summary>
        /// Bulid Collection of List<KeyValuePair<string, object>> for Given object
        /// </summary>
        /// <typeparam name="List">List of Type(string,object)</typeparam>
        /// <param name="paramCollection">List of Type(string,object)</param>
        /// <param name="obj">Object</param>
        /// <param name="excludeNullValue">Set True To Exclude Properties Having Null Value In The Object From Adding To The Collection</param>
        /// <returns> Collection of KeyValuePair<string, object></returns>
        public List<KeyValuePair<string, object>> BuildParameterCollection(List<KeyValuePair<string, object>> paramCollection, object obj, bool excludeNullValue)
        {
            try
            {
                if (excludeNullValue)
                {
                    foreach (PropertyInfo objProperty in obj.GetType().GetProperties())
                    {
                        if (objProperty.GetValue(obj, null) != null)
                        {
                            paramCollection.Add(new KeyValuePair<string, object>("@" + objProperty.Name.ToString(), objProperty.GetValue(obj, null)));
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo objProperty in obj.GetType().GetProperties())
                    {
                        paramCollection.Add(new KeyValuePair<string, object>("@" + objProperty.Name.ToString(), objProperty.GetValue(obj, null)));
                    }
                    return paramCollection;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return paramCollection;
        }

        /// <summary>
        /// Bulid Collection of List<KeyValuePair<string, string>> for Given object
        /// </summary>
        /// <typeparam name="List">List of Type(string,string)</typeparam>
        /// <param name="paramCollection">List of Type(string,string)</param>
        /// <param name="obj">Object</param>        
        /// <returns> Collection of KeyValuePair<string, string> </returns>
        public List<KeyValuePair<string, string>> BuildParameterCollection(List<KeyValuePair<string, string>> paramCollection, object obj)
        {
            try
            {
                foreach (PropertyInfo objProperty in obj.GetType().GetProperties())
                {
                    if (objProperty.GetValue(obj, null).ToString() != null)
                    {
                        paramCollection.Add(new KeyValuePair<string, string>("@" + objProperty.Name.ToString(), objProperty.GetValue(obj, null).ToString()));
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return paramCollection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQLCmd"></param>
        /// <param name="OutPutParamerterName"></param>
        /// <returns></returns>
        public OracleCommand AddOutPutParametrofGivenType<T>(OracleCommand SQLCmd, string OutPutParamerterName)
        {
            if (typeof(T) == typeof(int))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int32));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(Int16))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int16));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(long))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int64));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(DateTime))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Date));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(string))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName,OracleDbType.Varchar2,200));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                
            }
            if (typeof(T) == typeof(bool))
            {
                //SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, SqlDbType.Bit));
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Char, 1));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(decimal))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Decimal));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                SQLCmd.Parameters[OutPutParamerterName].Precision = 16;
                SQLCmd.Parameters[OutPutParamerterName].Scale = 2;
            }
            if (typeof(T) == typeof(float))
            {
                //SQLCmd.Parameters.Add(new SqlParameter(OutPutParamerterName, SqlDbType.Float));
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Single));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            return SQLCmd;
        }

        //public T AddOutPutParametrofGivenType<T>(object OutPutParamerterValue)
        //{
        //   object test;
        //    if (typeof(T) == typeof(string))
        //    {
        //        test = Convert.ToString(OutPutParamerterValue);
        //        OracleString
        //    }

        //    return (T)test;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQLCmd"></param>
        /// <param name="OutPutParamerterName"></param>
        /// <param name="OutPutParamerterValue"></param>
        /// <returns></returns>
        public OracleCommand AddOutPutParametrofGivenType<T>(OracleCommand SQLCmd, string OutPutParamerterName, object OutPutParamerterValue)
        {
            if (typeof(T) == typeof(int))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int32));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(Int16))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int16));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(long))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Int64));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(DateTime))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Date));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(string))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Varchar2, 50));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(bool))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Char,1));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            if (typeof(T) == typeof(decimal))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Decimal));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
                SQLCmd.Parameters[OutPutParamerterName].Precision = 16;
                SQLCmd.Parameters[OutPutParamerterName].Scale = 2;
            }
            if (typeof(T) == typeof(float))
            {
                SQLCmd.Parameters.Add(new OracleParameter(OutPutParamerterName, OracleDbType.Single));
                SQLCmd.Parameters[OutPutParamerterName].Direction = ParameterDirection.Output;
            }
            SQLCmd.Parameters[OutPutParamerterName].Value = OutPutParamerterValue;
            return SQLCmd;
        }

        #endregion

   
}