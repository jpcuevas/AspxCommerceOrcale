using SageFrame.ErrorLog;
using SageFrame.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseUserControl
/// </summary>
public class BaseUserControl : SageUserControl
{
    #region "Protected Methods"

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

    public BaseUserControl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="RedirectUrl"></param>
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

    #endregion
}