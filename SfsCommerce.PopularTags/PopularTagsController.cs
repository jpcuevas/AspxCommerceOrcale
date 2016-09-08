using AspxCommerce.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsController
    {
        private static Hashtable hst;

        static PopularTagsController()
        {
            PopularTagsController.hst = null;
        }

        public PopularTagsController()
        {
        }

        public List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
        {
            List<TagDetailsInfo> allPopularTags;
            try
            {
                allPopularTags = (new PopularTagsProvider()).GetAllPopularTags(aspxCommonObj, count);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return allPopularTags;
        }

        private static string getLocale(string messageKey)
        {
            PopularTagsController.hst = AppLocalized.getLocale("~/Modules/AspxCommerce/AspxPopularTags/");
            string str = messageKey;
            if ((PopularTagsController.hst == null ? false : PopularTagsController.hst[messageKey] != null))
            {
                str = PopularTagsController.hst[messageKey].ToString();
            }
            return str;
        }

        private List<RssFeedNewTags> GetNewItemTagRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
        {
            List<RssFeedNewTags> newTagsRssContent;
            try
            {
                newTagsRssContent = (new PopularTagsProvider()).GetNewTagsRssContent(aspxCommonObj, rssOption, count);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return newTagsRssContent;
        }

        private List<PopularTagsRssFeedInfo> GetPopularTagRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
        {
            List<PopularTagsRssFeedInfo> popularTagsRssContent;
            try
            {
                popularTagsRssContent = (new PopularTagsProvider()).GetPopularTagsRssContent(aspxCommonObj, rssOption, count);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return popularTagsRssContent;
        }

        public List<PopularTagsSettingInfo> GetPopularTagsSetting(AspxCommonInfo aspxCommonObj)
        {
            List<PopularTagsSettingInfo> popularTagsSetting;
            try
            {
                popularTagsSetting = (new PopularTagsProvider()).GetPopularTagsSetting(aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return popularTagsSetting;
        }

        public PopularTagsSettingKeyPair GetPopularTagsSettingValueByKey(AspxCommonInfo aspxCommonObj, string settingKey)
        {
            PopularTagsSettingKeyPair popularTagsSettingValueByKey;
            try
            {
                popularTagsSettingValueByKey = (new PopularTagsProvider()).GetPopularTagsSettingValueByKey(aspxCommonObj, settingKey);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return popularTagsSettingValueByKey;
        }

        public List<PopularTagsRssFeedInfo> GetRssFeedContens(AspxCommonInfo aspxCommonObj, string pageURL, string rssOption, int count)
        {
            List<PopularTagsRssFeedInfo> popularTagsRssContent;
            try
            {
                popularTagsRssContent = (new PopularTagsProvider()).GetPopularTagsRssContent(aspxCommonObj, rssOption, count);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return popularTagsRssContent;
        }

        public static List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
        {
            List<ItemBasicDetailsInfo> itemBasicDetailsInfos;
            try
            {
                List<ItemBasicDetailsInfo> userTaggedItems = PopularTagsProvider.GetUserTaggedItems(offset, limit, tagIDs, SortBy, rowTotal, aspxCommonObj);
                itemBasicDetailsInfos = userTaggedItems;
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
                (new PopularTagsProvider()).SaveUpdatePopularTagsSetting(aspxCommonObj, pTSettingList);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}