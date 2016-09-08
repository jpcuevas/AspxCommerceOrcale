
using SageFrame.ErrorLog;
using SageFrame.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseAdministrationUserControl
/// </summary>
public class BaseAdministrationUserControl : SageUserControl
{
    #region "Protectected Methods"

    protected void ProcessException(Exception exc)
    {
        int inID = 0;
        ErrorLogController objController = new ErrorLogController();
        inID = objController.InsertLog((int)SageFrame.Web.SageFrameEnums.ErrorType.AdministrationArea, 11, exc.Message, exc.ToString(),
           HttpContext.Current.Request.UserHostAddress, Request.RawUrl, true, GetPortalID, GetUsername);

        SageFrameConfig pagebase = new SageFrameConfig();
        if (pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages))
        {
            ShowMessage(SageMessageTitle.Exception.ToString(), exc.Message, exc.ToString(), SageMessageType.Error);
        }
    }

    #endregion

    #region "Public Methods"

    /// <summary>
    /// 
    /// </summary>
    public BaseAdministrationUserControl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Get Module Control Types
    /// </summary>
    /// <param name="ModuleID">ModuleID</param>
    /// <returns>Module Information, Control Types, Module Package Detail</returns>
    public DataSet GetExtensionSettings(string ModuleID)
    {
        try
        {
            List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
            ParaMeterCollection.Add(new KeyValuePair<string, string>("ModuleID", ModuleID));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("PortalID", GetPortalID.ToString()));
            DataSet ds = new DataSet();
            //SQLHandler sagesql = new SQLHandler();
            OracleHandler sagesql = new OracleHandler();
            ds = sagesql.ExecuteAsDataSet("sp_GetExtensionSetting", ParaMeterCollection);
            return ds;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// Returns To The URL If The Process Is Cancel 
    /// </summary>
    /// <param name="RedirectUrl">Redirect URL</param>
    public void ProcessCancelRequest(string RedirectUrl)
    {
        try
        {
            ProcessCancelRequestBase(RedirectUrl);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rawUrl"></param>
    /// <param name="controlPath"></param>
    /// <param name="parameter"></param>
    public void ProcessSourceControlUrl(string rawUrl, string controlPath, string parameter)
    {
        ProcessSourceControlUrlBase(rawUrl, controlPath, parameter);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="RedirectUrl"></param>
    /// <param name="IsSupress"></param>
    /// <param name="ExtensionMessage"></param>
    public void ProcessCancelRequestBase(string RedirectUrl, bool IsSupress, string ExtensionMessage)
    {
        string strURL = string.Empty;

        if (RedirectUrl.Contains("?"))
        {
            string[] d = RedirectUrl.Split('?');
            strURL = d[0];
        }

        if (strURL.Contains("?"))
        {
            strURL += "&ExtensionMessage=" + ExtensionMessage;
        }
        else if (strURL.Contains("&"))
        {
            strURL += "&ExtensionMessage=" + ExtensionMessage;
        }
        else
        {
            strURL += "?ExtensionMessage=" + ExtensionMessage;
        }

        HttpContext.Current.Response.Redirect(strURL, IsSupress);
    }

    /// <summary>
    /// Splits An Param In An Array
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public string[] GetParam(string param)
    {
        string[] stringParam = param.Split('/');
        return stringParam;
    }
    #endregion
}
