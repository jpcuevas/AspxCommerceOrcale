using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AspxCommerce.PopularTags
{
    public class RssFeedNewTags
    {
        public string AddedOn
        {
            get;
            set;
        }

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

        public string TagStatus
        {
            get;
            set;
        }

        public RssFeedNewTags()
        {
        }
    }
}