using AspxCommerce.Core;
using AspxCommerce.PopularTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for PopularTagsWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class PopularTagsWebService : System.Web.Services.WebService {

    public PopularTagsWebService()
    {
    }

    [WebMethod]
    public List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
    {
        List<TagDetailsInfo> allPopularTags;
        try
        {
            allPopularTags = (new PopularTagsController()).GetAllPopularTags(aspxCommonObj, count);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return allPopularTags;
    }

    [WebMethod]
    public List<PopularTagsSettingInfo> GetPopularTagsSetting(AspxCommonInfo aspxCommonObj)
    {
        List<PopularTagsSettingInfo> popularTagsSetting;
        try
        {
            popularTagsSetting = (new PopularTagsController()).GetPopularTagsSetting(aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return popularTagsSetting;
    }

    [WebMethod]
    public PopularTagsSettingKeyPair GetPopularTagsSettingValueByKey(AspxCommonInfo aspxCommonObj, string settingKey)
    {
        PopularTagsSettingKeyPair popularTagsSettingValueByKey;
        try
        {
            popularTagsSettingValueByKey = (new PopularTagsController()).GetPopularTagsSettingValueByKey(aspxCommonObj, settingKey);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return popularTagsSettingValueByKey;
    }

    [WebMethod]
    public List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
    {
        List<ItemBasicDetailsInfo> itemBasicDetailsInfos;
        try
        {
            List<ItemBasicDetailsInfo> userTaggedItems = PopularTagsController.GetUserTaggedItems(offset, limit, tagIDs, SortBy, rowTotal, aspxCommonObj);
            itemBasicDetailsInfos = userTaggedItems;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return itemBasicDetailsInfos;
    }

    [WebMethod]
    public void SaveUpdatePopularTagsSetting(AspxCommonInfo aspxCommonObj, PopularTagsSettingKeyPair pTSettingList)
    {
        try
        {
            (new PopularTagsController()).SaveUpdatePopularTagsSetting(aspxCommonObj, pTSettingList);
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
    
}
