using AspxCommerce.Core;
using SageFrame.Core.Services;
using System;
using System.Collections.Generic;
using System.Xml;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsIModuleExtraCodeExecute : IModuleExtraCodeExecute
    {
        public PopularTagsIModuleExtraCodeExecute()
        {
        }

        public void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath)
        {
            ModuleSinglePageInfo moduleSinglePageInfo = new ModuleSinglePageInfo();
            List<ModuleSinglePageInfo> moduleSinglePageInfos = new List<ModuleSinglePageInfo>();
            moduleSinglePageInfo.FolderName = "AspxCommerce/AspxPopularTags";
            moduleSinglePageInfo.FriendlyName = "PopularTagsViewAll";
            moduleSinglePageInfo.PageName = "AllTags";
            moduleSinglePageInfo.PageTitle = "AllTags";
            moduleSinglePageInfo.Description = "Display All Popular Tags";
            PageControlInfo pageControlInfo = new PageControlInfo();
            List<PageControlInfo> pageControlInfos = new List<PageControlInfo>();
            pageControlInfo.ControlSource = "Modules/AspxCommerce/AspxPopularTags/ViewAllTags.ascx";
            pageControlInfo.ControlType = "View";
            pageControlInfos.Add(pageControlInfo);
            moduleSinglePageInfo.PageControls = pageControlInfos;
            moduleSinglePageInfo.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            moduleSinglePageInfo.Version = "02.05.00";
            moduleSinglePageInfo.SupportPartialRendering = false;
            moduleSinglePageInfos.Add(moduleSinglePageInfo);
            moduleSinglePageInfo = new ModuleSinglePageInfo()
            {
                FolderName = "AspxCommerce/AspxPopularTags",
                FriendlyName = "PopularTagsTaggedItem",
                PageName = "TaggedItem",
                PageTitle = "TaggedItem",
                Description = "Display All Tagged Items"
            };
            pageControlInfo = new PageControlInfo();
            pageControlInfos = new List<PageControlInfo>();
            pageControlInfo.ControlSource = "Modules/AspxCommerce/AspxPopularTags/ViewTaggedItems.ascx";
            pageControlInfo.ControlType = "View";
            pageControlInfos.Add(pageControlInfo);
            moduleSinglePageInfo.PageControls = pageControlInfos;
            moduleSinglePageInfo.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            moduleSinglePageInfo.Version = "02.05.00";
            moduleSinglePageInfo.SupportPartialRendering = false;
            moduleSinglePageInfos.Add(moduleSinglePageInfo);
            moduleSinglePageInfo = new ModuleSinglePageInfo()
            {
                FolderName = "AspxCommerce/AspxPopularTags",
                FriendlyName = "PopularTagsRssView",
                PageName = "TagsRssFeed",
                PageTitle = "TagsRssFeed",
                Description = "Popular Tags Rss Feed View"
            };
            pageControlInfo = new PageControlInfo();
            pageControlInfos = new List<PageControlInfo>();
            pageControlInfo.ControlSource = "Modules/AspxCommerce/AspxPopularTags/PopularTagsRss.ascx";
            pageControlInfo.ControlType = "View";
            pageControlInfos.Add(pageControlInfo);
            moduleSinglePageInfo.PageControls = pageControlInfos;
            moduleSinglePageInfo.HelpURL = "";
            moduleSinglePageInfo.Version = "02.05.00";
            moduleSinglePageInfo.SupportPartialRendering = false;
            moduleSinglePageInfos.Add(moduleSinglePageInfo);
            (new CreateModulePackage()).CreateMultiplePagesModulePackage(moduleSinglePageInfos);
        }

        public void ExecuteOnUnInstallation(XmlDocument doc)
        {
            ModuleSinglePageInfo moduleSinglePageInfo = new ModuleSinglePageInfo();
            List<ModuleSinglePageInfo> moduleSinglePageInfos = new List<ModuleSinglePageInfo>();
            moduleSinglePageInfo.FolderName = "AspxCommerce/AspxPopularTags";
            moduleSinglePageInfo.FriendlyName = "PopularTagsViewAll";
            moduleSinglePageInfo.PageName = "AllTags";
            moduleSinglePageInfo.PageTitle = "AllTags";
            moduleSinglePageInfo.Description = "Display All Popular Tags";
            PageControlInfo pageControlInfo = new PageControlInfo();
            List<PageControlInfo> pageControlInfos = new List<PageControlInfo>();
            pageControlInfo.ControlSource = "Modules/AspxCommerce/AspxPopularTags/ViewAllTags.ascx";
            pageControlInfo.ControlType = "View";
            pageControlInfos.Add(pageControlInfo);
            moduleSinglePageInfo.PageControls = pageControlInfos;
            moduleSinglePageInfo.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            moduleSinglePageInfo.Version = "02.05.00";
            moduleSinglePageInfo.SupportPartialRendering = false;
            moduleSinglePageInfos.Add(moduleSinglePageInfo);
            moduleSinglePageInfo = new ModuleSinglePageInfo()
            {
                FolderName = "AspxCommerce/AspxPopularTags",
                FriendlyName = "PopularTagsTaggedItem",
                PageName = "TaggedItem",
                PageTitle = "TaggedItem",
                Description = "Display All Tagged Items"
            };
            pageControlInfo = new PageControlInfo();
            pageControlInfos = new List<PageControlInfo>();
            pageControlInfo.ControlSource = "Modules/AspxCommerce/AspxPopularTags/ViewTaggedItems.ascx";
            pageControlInfo.ControlType = "View";
            pageControlInfos.Add(pageControlInfo);
            moduleSinglePageInfo.PageControls = pageControlInfos;
            moduleSinglePageInfo.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            moduleSinglePageInfo.Version = "02.05.00";
            moduleSinglePageInfo.SupportPartialRendering = false;
            moduleSinglePageInfos.Add(moduleSinglePageInfo);
            moduleSinglePageInfo = new ModuleSinglePageInfo()
            {
                FolderName = "AspxCommerce/AspxPopularTags",
                FriendlyName = "PopularTagsRssView",
                PageName = "TagsRssFeed",
                PageTitle = "TagsRssFeed",
                Description = "Popular Tags Rss Feed View"
            };
            pageControlInfo = new PageControlInfo();
            pageControlInfos = new List<PageControlInfo>();
            pageControlInfo.ControlSource = "Modules/AspxCommerce/AspxPopularTags/PopularTagsRss.ascx";
            pageControlInfo.ControlType = "View";
            pageControlInfos.Add(pageControlInfo);
            moduleSinglePageInfo.PageControls = pageControlInfos;
            moduleSinglePageInfo.HelpURL = "";
            moduleSinglePageInfo.Version = "02.05.00";
            moduleSinglePageInfo.SupportPartialRendering = false;
            moduleSinglePageInfos.Add(moduleSinglePageInfo);
            (new CreateModulePackage()).DeleteMultiplePageModulePackage(moduleSinglePageInfos);
        }
    }
}