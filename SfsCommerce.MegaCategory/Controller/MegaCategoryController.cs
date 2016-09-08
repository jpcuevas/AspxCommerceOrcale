using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.MegaCategory
{
    public class MegaCategoryController
    {
        public MegaCategoryController()
        {
        }

        public List<MegaCategoryViewInfo> GetCategoryMenuList(AspxCommonInfo aspxCommonObj)
        {
            return (new MegaCategoryProvider()).GetCategoryMenuList(aspxCommonObj);
        }

        public MegaCategorySettingInfo GetMegaCategorySetting(AspxCommonInfo aspxCommonObj)
        {
            MegaCategorySettingInfo megaCategorySetting;
            try
            {
                megaCategorySetting = (new MegaCategoryProvider()).GetMegaCategorySetting(aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return megaCategorySetting;
        }

        public List<MegaCategorySettingInfo> MegaCategorySettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            List<MegaCategorySettingInfo> megaCategorySettingInfos;
            try
            {
                megaCategorySettingInfos = (new MegaCategoryProvider()).MegaCategorySettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return megaCategorySettingInfos;
        }
    }
}