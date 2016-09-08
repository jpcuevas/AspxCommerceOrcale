using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxHeaderController
    {
        public AspxHeaderController()
        {
        }

        public static int GetCartItemsCount(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                decimal cartItCount = AspxHeaderProvider.GetCartItemsCount(aspxCommonObj);
                return (int)cartItCount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int CountWishItems(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int wishItCount = AspxHeaderProvider.CountWishItems(aspxCommonObj);
                return wishItCount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SetHeaderSetting(string headerType, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxHeaderProvider.SetHeaderSetting(headerType, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string GetHeaderSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string headerType = AspxHeaderProvider.GetHeaderSetting(aspxCommonObj);
                return headerType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
