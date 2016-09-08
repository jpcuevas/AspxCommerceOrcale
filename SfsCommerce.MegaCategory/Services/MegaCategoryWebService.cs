using AspxCommerce.Core;
using AspxCommerce.MegaCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

[ScriptService]
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class MegaCategoryWebService : WebService
{
    public MegaCategoryWebService()
    {
    }

    [WebMethod]
    public MegaCategorySettingInfo GetMegaCategorySetting(AspxCommonInfo aspxCommonObj)
    {
        MegaCategorySettingInfo megaCategorySetting;
        try
        {
            megaCategorySetting = (new MegaCategoryController()).GetMegaCategorySetting(aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return megaCategorySetting;
    }

    [WebMethod]
    public List<MegaCategorySettingInfo> MegaCategorySettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
    {
        List<MegaCategorySettingInfo> megaCategorySettingInfos;
        try
        {
            megaCategorySettingInfos = (new MegaCategoryController()).MegaCategorySettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return megaCategorySettingInfos;
    }
}