using AspxCommerce.Core;
using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsProvider
    {
        public PopularTagsProvider()
        {
        }

        public List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
        {
            List<TagDetailsInfo> tagDetailsInfos;
            try
            {
                List<KeyValuePair<string, object>> paramSPUC = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                paramSPUC.Add(new KeyValuePair<string, object>("@Count", (object)count));
                tagDetailsInfos = (new OracleHandler()).ExecuteAsList<TagDetailsInfo>("usp_Aspx_GetPopularTags", paramSPUC);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return tagDetailsInfos;
        }

        public List<RssFeedNewTags> GetNewTagsRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            List<RssFeedNewTags> rssFeedNewTags;
            try
            {
                List<RssFeedNewTags> rssFeedNewTags1 = new List<RssFeedNewTags>();
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@StoreID", (object)aspxCommonObj.StoreID),
                    new KeyValuePair<string, object>("@PortalID", (object)aspxCommonObj.PortalID),
                    new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName),
                    new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName),
                    new KeyValuePair<string, object>("@Count", (object)count),
                    new KeyValuePair<string, object>("@Tag", "")
                };
                OracleHandler sQLHandler = new OracleHandler();
                rssFeedNewTags1 = sQLHandler.ExecuteAsList<RssFeedNewTags>("[dbo].[usp_Aspx_GetRssFeedNewTag]", keyValuePairs);
                keyValuePairs.Remove(new KeyValuePair<string, object>("@Tag", ""));
                foreach (RssFeedNewTags rssFeedNewTag in rssFeedNewTags1)
                {
                    List<ItemCommonInfo> itemCommonInfos = new List<ItemCommonInfo>();
                    keyValuePairs.Add(new KeyValuePair<string, object>("@Tag", rssFeedNewTag.TagName));
                    rssFeedNewTag.TagItemInfo = sQLHandler.ExecuteAsList<ItemCommonInfo>("[dbo].[usp_Aspx_GetRssFeedNewTag]", keyValuePairs);
                    keyValuePairs.Remove(new KeyValuePair<string, object>("@Tag", rssFeedNewTag.TagName));
                }
                rssFeedNewTags = rssFeedNewTags1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return rssFeedNewTags;
        }

        public List<PopularTagsRssFeedInfo> GetPopularTagsRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            List<PopularTagsRssFeedInfo> popularTagsRssFeedInfos;
            try
            {
                List<PopularTagsRssFeedInfo> popularTagsRssFeedInfos1 = new List<PopularTagsRssFeedInfo>();
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@StoreID", (object)aspxCommonObj.StoreID),
                    new KeyValuePair<string, object>("@PortalID", (object)aspxCommonObj.PortalID),
                    new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName),
                    new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName),
                    new KeyValuePair<string, object>("@Count", (object)count),
                    new KeyValuePair<string, object>("@Tag", "")
                };
                OracleHandler sQLHandler = new OracleHandler();
                popularTagsRssFeedInfos1 = sQLHandler.ExecuteAsList<PopularTagsRssFeedInfo>("[dbo].[usp_Aspx_GetRssFeedPopularTag]", keyValuePairs);
                keyValuePairs.Remove(new KeyValuePair<string, object>("@Tag", ""));
                foreach (PopularTagsRssFeedInfo popularTagsRssFeedInfo in popularTagsRssFeedInfos1)
                {
                    List<ItemCommonInfo> itemCommonInfos = new List<ItemCommonInfo>();
                    keyValuePairs.Add(new KeyValuePair<string, object>("@Tag", popularTagsRssFeedInfo.TagName));
                    popularTagsRssFeedInfo.TagItemInfo = sQLHandler.ExecuteAsList<ItemCommonInfo>("[dbo].[usp_Aspx_GetRssFeedPopularTag]", keyValuePairs);
                    keyValuePairs.Remove(new KeyValuePair<string, object>("@Tag", popularTagsRssFeedInfo.TagName));
                }
                popularTagsRssFeedInfos = popularTagsRssFeedInfos1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return popularTagsRssFeedInfos;
        }

        public List<PopularTagsSettingInfo> GetPopularTagsSetting(AspxCommonInfo aspxCommonObj)
        {
            List<PopularTagsSettingInfo> popularTagsSettingInfos;
            try
            {
                List<KeyValuePair<string, object>> paramSPC = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                popularTagsSettingInfos = (new OracleHandler()).ExecuteAsList<PopularTagsSettingInfo>("[dbo].[usp_Aspx_PopularTagsSettingGet]", paramSPC);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return popularTagsSettingInfos;
        }

        public PopularTagsSettingKeyPair GetPopularTagsSettingValueByKey(AspxCommonInfo aspxCommonObj, string settingKey)
        {
            PopularTagsSettingKeyPair popularTagsSettingKeyPair;
            try
            {
                List<KeyValuePair<string, object>> paramSPC = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramSPC.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
                OracleHandler sQLHandler = new OracleHandler();
                PopularTagsSettingKeyPair popularTagsSettingKeyPair1 = new PopularTagsSettingKeyPair();
                popularTagsSettingKeyPair = sQLHandler.ExecuteAsObject<PopularTagsSettingKeyPair>("[dbo].[usp_Aspx_PopularTagsSettingValueGetBYKey]", paramSPC);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return popularTagsSettingKeyPair;
        }

        public static List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
        {
            List<ItemBasicDetailsInfo> itemBasicDetailsInfos;
            string empty = string.Empty;
            try
            {
                List<KeyValuePair<string, object>> paramSPUC = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                paramSPUC.Add(new KeyValuePair<string, object>("@offset", (object)offset));
                paramSPUC.Add(new KeyValuePair<string, object>("@limit", (object)limit));
                paramSPUC.Add(new KeyValuePair<string, object>("@TagIDs", tagIDs));
                paramSPUC.Add(new KeyValuePair<string, object>("@RowTotal", (object)rowTotal));
                OracleHandler sQLHandler = new OracleHandler();
                if (SortBy == 1)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByItemIDDesc]";
                }
                else if (SortBy == 2)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByItemIDAsc]";
                }
                else if (SortBy == 3)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByPriceDesc]";
                }
                else if (SortBy == 4)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByPriceAsc]";
                }
                else if (SortBy == 5)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByName]";
                }
                else if (SortBy == 6)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByViewCount]";
                }
                else if (SortBy == 7)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByIsFeatured]";
                }
                else if (SortBy == 8)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByIsSpecial]";
                }
                else if (SortBy == 9)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortBySoldItem]";
                }
                else if (SortBy == 10)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByDiscount]";
                }
                else if (SortBy == 11)
                {
                    empty = "[dbo].[usp_Aspx_GetItemsByTagIDSortByRatedValue]";
                }
                itemBasicDetailsInfos = sQLHandler.ExecuteAsList<ItemBasicDetailsInfo>(empty, paramSPUC);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return itemBasicDetailsInfos;
        }

        public void SaveUpdatePopularTagsSetting(AspxCommonInfo aspxCommonObj, PopularTagsSettingKeyPair pTSettingList)
        {
            try
            {
                List<KeyValuePair<string, object>> paramSPC = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramSPC.Add(new KeyValuePair<string, object>("@SettingKeys", pTSettingList.SettingKey));
                paramSPC.Add(new KeyValuePair<string, object>("@SettingValues", pTSettingList.SettingValue));
                (new OracleHandler()).ExecuteNonQuery("[dbo].[usp_Aspx_PopularTagsSettingsUpdate]", paramSPC);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}