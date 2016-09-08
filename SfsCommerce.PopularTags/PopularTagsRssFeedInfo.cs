using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsRssFeedInfo
    {
        public string TagIDs
        {
            get;
            set;
        }

        public List<ItemCommonInfo> TagItemInfo
        {
            get;
            set;
        }

        public string TagName
        {
            get;
            set;
        }

        public PopularTagsRssFeedInfo()
        {
        }
    }
}