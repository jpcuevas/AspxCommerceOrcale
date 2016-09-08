using System;
using System.Runtime.CompilerServices;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsSettingInfo
    {
        public bool IsEnablePopularTag
        {
            get;
            set;
        }

        public bool IsEnablePopularTagRss
        {
            get;
            set;
        }

        public int PopularTagCount
        {
            get;
            set;
        }

        public int PopularTagRssCount
        {
            get;
            set;
        }

        public string PopularTagsRssPageName
        {
            get;
            set;
        }

        public int TaggedItemInARow
        {
            get;
            set;
        }

        public string ViewAllTagsPageName
        {
            get;
            set;
        }

        public string ViewTaggedItemPageName
        {
            get;
            set;
        }

        public PopularTagsSettingInfo()
        {
        }
    }
}