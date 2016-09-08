using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace SageFrame.Shared
{
    /// <summary>
    /// Summary description for SettingProvider
    /// </summary>
    public class SettingProvider
    {
        public SettingProvider()
        {
        }

        private DataSet GetSettingsByPortalAndSettingType(string PortalID, string SettingType)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("v_PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("v_SettingType", SettingType));
                DataSet ds = new DataSet();
                OracleHandler sfsoracle = new OracleHandler();
                ds = sfsoracle.ExecuteAsDataSet("sp_GetAllSettings", ParaMeterCollection);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable GetAllPortals()
        {
            try
            {
                DataSet ds = new DataSet();
                OracleHandler sfsoracle = new OracleHandler();
                ds = sfsoracle.ExecuteAsDataSet("sp_GetAllPortals");
                DataTable dt = ds.Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable GetSettingsByPortal(string PortalID, string SettingType)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = GetSettingsByPortalAndSettingType(PortalID, SettingType);
                if (ds != null && ds.Tables != null && ds.Tables[0] != null)
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveSageSettings(string SettingTypes, string SettingKeys, string SettingValues, string Username, string PortalID)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("SettingTypes", SettingTypes));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("SettingKeys", SettingKeys));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("SettingValues", SettingValues));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("UserName", Username));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("PortalID", PortalID));
                OracleHandler sfsoracle = new OracleHandler();
                sfsoracle.ExecuteNonQuery("sp_InsertUpdateSettings", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveSageSetting(string SettingType, string SettingKey, string SettingValue, string Username, string PortalID)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("SettingType", SettingType));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("SettingKey", SettingKey));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("SettingValue", SettingValue));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("UserName", Username));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("PortalID", PortalID));
                OracleHandler sfsoracle = new OracleHandler();
                sfsoracle.ExecuteNonQuery("sp_InsertUpdateSetting", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region "Google Analytics"

        public List<GoogleAnalyticsInfo> GetGoogleAnalyticsActiveOnlyByPortalID(int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                //SQLHandler sqlH = new SQLHandler();
                OracleHandler sqlH = new OracleHandler();
                List<GoogleAnalyticsInfo> defaultList = sqlH.ExecuteAsList<GoogleAnalyticsInfo>("sp_GoogleAnalyticsListActiveOn", ParaMeterCollection);
                return defaultList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GoogleAnalyticsInfo GetGoogleAnalyticsByPortalID(int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                //SQLHandler sqlH = new SQLHandler();
                OracleHandler sqlH = new OracleHandler();
                GoogleAnalyticsInfo defaultList = sqlH.ExecuteAsObject<GoogleAnalyticsInfo>("sp_GoogleAnalyticsListByPortalID", ParaMeterCollection);
                return defaultList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GoogleAnalyticsAddUpdate(string GoogleJSCode, bool IsActive, int PortalID, string AddedBy)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("GoogleJSCode", GoogleJSCode));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("IsActive", IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("AddedBy", AddedBy));
                //SQLHandler sqlH = new SQLHandler();
                OracleHandler sqlH = new OracleHandler();
                sqlH.ExecuteNonQuery("sp_GoogleAnalyticsAddUpdate", ParaMeterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        public List<SagePortals> PortalGetList()
        {
            try
            {
                //string sqlquery = "";
                OracleHandler sqlH = new OracleHandler();
                List<SagePortals> sagePortals = sqlH.ExecuteAsList<SagePortals>("sp_PortalGetList");
                //List<SagePortals> sagePortals = sqlH.ExecuteAsList<SagePortals>("SELECT Por.PortalID ,Name , LOWER(LTRIM(RTRIM(SEOName))) SEOName  ,Isparent , Por.Parentid, ( Select Seoname From Portal Port Where Por.Parentid = Port.Portalid ) "+ 
                   // "Parentportalname  , SettingValue.SettingValue DefaultPage  FROM Portal Por JOIN SettingValue  ON Por.PortalID = SettingValue.settingtypeid   Where Settingtype = 'SiteAdmin' And Settingkey = 'PortalDefaultPage' ");
                return sagePortals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SageUserRole> RoleListGetByUsername(string userName, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("v_UserName", userName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("v_PortalID", portalID));
                //SQLHandler sqlH = new SQLHandler();
                OracleHandler sqlH = new OracleHandler();
                List<SageUserRole> sagePortalList = sqlH.ExecuteAsList<SageUserRole>("sp_RoleGetByUsername", ParaMeterCollection);
                return sagePortalList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Change portal
        public void ChangePortal(int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                sagesql.ExecuteNonQuery("usp_ChangePortal", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public KeyValue GetSettingValueByIndividualKey(string settingKey, int portalID)
        {
            KeyValue value = new KeyValue();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("v_PortalID", portalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("v_SettingKey", settingKey));
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sfsoracle = new OracleHandler();
                value = sfsoracle.ExecuteAsObject<KeyValue>("usp_GetSetValueByIndividualKey", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
            return value;
        }

        public DataSet GetAllSettings(string portalID, string settingType)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("PortalID", portalID));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("SettingType", settingType));
                DataSet dataset = new DataSet();
                //SQLHandler sagesql = new SQLHandler();
                OracleHandler sagesql = new OracleHandler();
                dataset = sagesql.ExecuteAsDataSet("usp_GetAllSettings", ParaMeterCollection);
                return dataset;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
    }

    public class SagePortals
    {

        private int _PortalID;

        private string _Name;

        private string _SEOName;

        private System.Nullable<bool> _IsParent;

        private string _DefaultPage;

        public SagePortals()
        {
        }

        public SagePortals(int portalID, string name, string sEOName, bool isParent, string defaultPage)
        {
            this.PortalID = portalID;
            this.Name = name;
            this.SEOName = sEOName;
            this.IsParent = isParent;
            this.DefaultPage = defaultPage;
        }


        public int PortalID
        {
            get
            {
                return this._PortalID;
            }
            set
            {
                if ((this._PortalID != value))
                {
                    this._PortalID = value;
                }
            }
        }


        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }


        public string SEOName
        {
            get
            {
                return this._SEOName;
            }
            set
            {
                if ((this._SEOName != value))
                {
                    this._SEOName = value;
                }
            }
        }


        public System.Nullable<bool> IsParent
        {
            get
            {
                return this._IsParent;
            }
            set
            {
                if ((this._IsParent != value))
                {
                    this._IsParent = value;
                }
            }
        }


        public string DefaultPage
        {
            get
            {
                return this._DefaultPage;
            }
            set
            {
                if ((this._DefaultPage != value))
                {
                    this._DefaultPage = value;
                }
            }
        }
    }

    public class SageUserRole
    {

        private System.Guid _RoleId;

        public SageUserRole()
        {
        }

        public SageUserRole(System.Guid roleId)
        {
            this.RoleId = roleId;
        }

        public System.Guid RoleId
        {
            get
            {
                return this._RoleId;
            }
            set
            {
                if ((this._RoleId != value))
                {
                    this._RoleId = value;
                }
            }
        }
    }
}