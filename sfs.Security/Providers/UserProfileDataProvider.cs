﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.UserProfile
{
    public class UserProfileDataProvider
    {

        public static void AddUpdateProfile(UserProfileInfo objProfile)
        {
            try
            {
                string sp = "usp_AddUpdateUserProfile";
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();


                ParamCollInput.Add(new KeyValuePair<string, object>("image", objProfile.Image));
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", objProfile.UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("FirstName", objProfile.FirstName));
                ParamCollInput.Add(new KeyValuePair<string, object>("LastName", objProfile.LastName));
                ParamCollInput.Add(new KeyValuePair<string, object>("FullName", objProfile.FullName));
                ParamCollInput.Add(new KeyValuePair<string, object>("BirthDate", objProfile.BirthDate));
                ParamCollInput.Add(new KeyValuePair<string, object>("Gender", objProfile.Gender));

                ParamCollInput.Add(new KeyValuePair<string, object>("Location", objProfile.Location));
                ParamCollInput.Add(new KeyValuePair<string, object>("AboutYou", objProfile.AboutYou));
                ParamCollInput.Add(new KeyValuePair<string, object>("Email", objProfile.Email));
                ParamCollInput.Add(new KeyValuePair<string, object>("ResPhone", objProfile.ResPhone));
                ParamCollInput.Add(new KeyValuePair<string, object>("Mobile", objProfile.MobilePhone));

                ParamCollInput.Add(new KeyValuePair<string, object>("Others", objProfile.Others));
                ParamCollInput.Add(new KeyValuePair<string, object>("AddedOn", objProfile.AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("AddedBy", objProfile.AddedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("UpdatedOn", objProfile.UpdatedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", objProfile.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("UpdatedBy", objProfile.UpdatedBy));

                SQLH.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static UserProfileAdditionalInfo GetProfile(string UserName, int PortalID)
        {
            string sp = "usp_GetUserProfile";
            OracleHandler sagesql = new OracleHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("UserName", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
            try
            {
                return (sagesql.ExecuteAsObject<UserProfileAdditionalInfo>(sp, ParamCollInput));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void DeleteProfilePic(string UserName, int PortalID)
        {
            try
            {
                string sp = "usp_UserProfilePicDelete";
                OracleHandler SQLH = new OracleHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("UserName", UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("PortalID", PortalID));
                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool UpdateCartAnonymoususertoRegistered(int storeID, int portalID, int customerID, string sessionCode)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("StoreID", storeID));
                ParaMeter.Add(new KeyValuePair<string, object>("PortalID", portalID));
                ParaMeter.Add(new KeyValuePair<string, object>("CustomerID", customerID));
                ParaMeter.Add(new KeyValuePair<string, object>("SessionCode", sessionCode));
                OracleHandler sqlH = new OracleHandler();
                return sqlH.ExecuteNonQueryAsBool("usp_Aspx_UpdateCartAnonymoususertoRegistered", ParaMeter, "IsUpdate");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}