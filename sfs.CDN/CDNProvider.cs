using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.CDN
{
    public class CDNProvider
    {
        public void SaveLinks(List<CDNInfo> objInfo)
        {
            //SQLHandler sagesql = new SQLHandler();
            OracleHandler orasql = new OracleHandler();
            string sp = "usp_CDNSaveLink";
            foreach (CDNInfo cdn in objInfo)
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("URL", cdn.URL));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsJS", cdn.IsJS));
                ParamCollInput.Add(new KeyValuePair<string, object>("URLOrder", cdn.URLOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", cdn.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("Mode", cdn.Mode));
                try
                {
                    orasql.ExecuteNonQuery(sp, ParamCollInput);

                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        public List<CDNInfo> GetCDNLinks(int PortalID)
        {
            OracleHandler orasql = new OracleHandler();
            string sp = "usp_CDNGetLink";
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                return orasql.ExecuteAsList<CDNInfo>(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }


        }

        public void SaveOrder(List<CDNInfo> objOrder)
        {
            //SQLHandler sagesql = new SQLHandler();
            OracleHandler orasql = new OracleHandler();
            string sp = "usp_CDNSaveOrder";
            foreach (CDNInfo cdn in objOrder)
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("URLID", cdn.URLID));
                ParamCollInput.Add(new KeyValuePair<string, object>("URLOrder", cdn.URLOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", cdn.PortalID));
                try
                {
                    orasql.ExecuteNonQuery(sp, ParamCollInput);

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public void DeleteURL(int UrlID, int PortalID)
        {
            OracleHandler orasql = new OracleHandler();
            string sp = "usp_CDNDeleteLink";
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UrlID", UrlID));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                orasql.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}