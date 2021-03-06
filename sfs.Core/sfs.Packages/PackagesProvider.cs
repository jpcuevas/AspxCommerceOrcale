﻿#region "Copyright"

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

#endregion


namespace SageFrame.Packages
{
    public class PackagesProvider
    {

        public static void UpdatePackagesChange(string ModuleIDs, string IsActives, string UpdatedBy)
        {
            string sp = "sp_PackagesUpdateChanges";
            OracleHandler sagesql = new OracleHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("ModuleIDs", ModuleIDs));
                ParamCollInput.Add(new KeyValuePair<string, object>("IsActives", IsActives));
                ParamCollInput.Add(new KeyValuePair<string, object>("UpdatedBy", UpdatedBy));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<PackagesInfo> GetPackagesByPortalID(int PortalID, string SearchText)
        {
            try
            {
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("SearchText", SearchText));

                return SQLH.ExecuteAsList<PackagesInfo>("sp_PackagesGetByPortalID", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
