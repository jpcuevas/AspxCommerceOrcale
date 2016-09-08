using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CategorySqlProvider
/// </summary>
namespace AspxCommerce.Core
{
    public class CategorySqlProvider
    {
        public List<AttributeFormInfo> GetCategoryFormAttributes(Int32 categoryID, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeFormInfo> formAttributeList;
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("CategoryID", categoryID));
            parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
            formAttributeList = sqlHandler.ExecuteAsList<AttributeFormInfo>("usp_Aspx_GetCategoryFormAttributes", parameterCollection);
            return formAttributeList;
        }

        public List<CategoryInfo> GetCategoryAll(bool isActive, AspxCommonInfo aspxCommonObj)
        {
            List<CategoryInfo> catList;
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("IsActive", isActive));
            parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
            catList = sqlHandler.ExecuteAsList<CategoryInfo>("usp_Aspx_GetCategoryAll", parameterCollection);
            return catList;
        }

        public List<CategoryAttributeInfo> GetCategoryByCategoryID(Int32 categoryID, AspxCommonInfo aspxCommonObj)
        {
            List<CategoryAttributeInfo> catList;
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("CategoryID", categoryID));
            parameterCollection.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
            catList = sqlHandler.ExecuteAsList<CategoryAttributeInfo>("usp_Aspx_GetCategoryByCategoryID", parameterCollection);
            return catList;
        }

        public bool CheckUniqueName(string catName, int catId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("CategoryName", catName));
                parameterCollection.Add(new KeyValuePair<string, object>("CategoryID", catId));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
                return sqlH.ExecuteNonQueryAsBool("usp_Aspx_CategoryNameUniquenessCheck", parameterCollection, "IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CategoryAddUpdate(CategoryInfo categoryInfo, string selectedItems, List<CategoryAttributeInfo> listCA, string userName, string culture)
        {
            OracleHandler sqlHandler = new OracleHandler();
            int categoryID = 0;
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("CategoryID", categoryInfo.CategoryID));
            parameterCollection.Add(new KeyValuePair<string, object>("SelectedItems", selectedItems));
            parameterCollection.Add(new KeyValuePair<string, object>("ParentID", categoryInfo.ParentID));
            parameterCollection.Add(new KeyValuePair<string, object>("IsShowInSearch", categoryInfo.IsShowInSearch));
            parameterCollection.Add(new KeyValuePair<string, object>("IsShowInCatalog", categoryInfo.IsShowInCatalog));
            parameterCollection.Add(new KeyValuePair<string, object>("IsShowInMenu", categoryInfo.IsShowInMenu));
            parameterCollection.Add(new KeyValuePair<string, object>("ActiveFrom", categoryInfo.ActiveFrom));
            parameterCollection.Add(new KeyValuePair<string, object>("ActiveTo", categoryInfo.ActiveTo));
            parameterCollection.Add(new KeyValuePair<string, object>("StoreID", categoryInfo.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("PortalID", categoryInfo.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("IsActive", true));
            parameterCollection.Add(new KeyValuePair<string, object>("UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("CultureName", culture));
            try
            {
                categoryID = sqlHandler.ExecuteNonQueryAsGivenType<int>("usp_Aspx_CategoryAddUpdate", parameterCollection, "NewCategoryID");

                int inputTypeID;
                int validationTypeID;
                string valueType = string.Empty;
                foreach (CategoryAttributeInfo ca in listCA)
                {
                    parameterCollection.Clear();
                    inputTypeID = ca.InputTypeID;
                    validationTypeID = ca.ValidationTypeID;
                    if (inputTypeID == 1)
                    {
                        if (validationTypeID == 3)
                        {
                            parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", ca.DecimalValue));
                            valueType = "DECIMAL";
                        }
                        else if (validationTypeID == 5)
                        {
                            parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", ca.IntValue));
                            valueType = "NUMBER";
                        }
                        else
                        {
                            parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", ca.NvarcharValue));
                            valueType = "VARCHAR2";
                        }
                    }
                    else if (inputTypeID == 2)
                    {
                        parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", ca.TextValue));
                        valueType = "TEXT";
                    }
                    else if (inputTypeID == 3)
                    {
                        parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", ca.DateValue));
                        valueType = "DATE";
                    }
                    else if (inputTypeID == 4)
                    {
                        parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", ca.BooleanValue));
                        valueType = "Boolean";
                    }
                    else if (inputTypeID == 5 || inputTypeID == 6 || inputTypeID == 9 || inputTypeID == 10 || inputTypeID == 11 || inputTypeID == 12)
                    {
                        parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", ca.OptionValues));
                        valueType = "OPTIONS";
                    }
                    else if (inputTypeID == 7)
                    {
                        parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", ca.DecimalValue));
                        valueType = "DECIMAL";
                    }
                    else if (inputTypeID == 8)
                    {
                        parameterCollection.Add(new KeyValuePair<string, object>("AttributeValue", ca.FileValue));
                        valueType = "FILE";
                    }
                    parameterCollection.Add(new KeyValuePair<string, object>("CategoryID", categoryID));
                    parameterCollection.Add(new KeyValuePair<string, object>("AttributeID", ca.AttributeID));
                    parameterCollection.Add(new KeyValuePair<string, object>("StoreID", categoryInfo.StoreID));
                    parameterCollection.Add(new KeyValuePair<string, object>("PortalID", categoryInfo.PortalID));
                    parameterCollection.Add(new KeyValuePair<string, object>("UserName", userName));
                    parameterCollection.Add(new KeyValuePair<string, object>("CultureName", culture));
                    sqlHandler.ExecuteNonQuery("usp_Aspx_CategoryAttributesValue" + valueType + "AddUpdate", parameterCollection);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categoryID;
        }

        public void DeleteCategory(Int32 storeID, Int32 portalID, Int32 categoryID, string userName, string culture)
        {
            try
            {
                OracleHandler sqlHandler = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("CategoryID", categoryID));
                parameterCollection.Add(new KeyValuePair<string, object>("UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("CultureName", culture));
                sqlHandler.ExecuteNonQuery("usp_Aspx_CategoryDelete", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryItemInfo> GetCategoryItems(Int32 offset, int? limit, GetCategoryItemInfo categoryItemsInfo, AspxCommonInfo aspxCommonObj)
        {
            List<CategoryItemInfo> listCategoryItem;
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("CategoryID", categoryItemsInfo.CategoryID));
            parameterCollection.Add(new KeyValuePair<string, object>("SKU", categoryItemsInfo.SKU));
            parameterCollection.Add(new KeyValuePair<string, object>("Name", categoryItemsInfo.Name));
            parameterCollection.Add(new KeyValuePair<string, object>("PriceFrom", categoryItemsInfo.PriceFrom));
            parameterCollection.Add(new KeyValuePair<string, object>("PriceTo", categoryItemsInfo.PriceTo));
            parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName));
            //listCategoryItem = sqlHandler.ExecuteAsList<CategoryItemInfo>("dbo.usp_Aspx_GetItemsByCategoryID", parameterCollection);
            listCategoryItem = sqlHandler.ExecuteAsList<CategoryItemInfo>("usp_Aspx_GetItemsByCategoryIDService", parameterCollection);
            return listCategoryItem;
        }

        public bool SaveChangesCategoryTree(string categoryIDs, AspxCommonInfo aspxCommonObj)
        {
            List<CategoryItemInfo> listCategoryItem = new List<CategoryItemInfo>();
            OracleHandler sqlHandler = new OracleHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("CategoryIDs", categoryIDs));
            parameterCollection.Add(new KeyValuePair<string, object>("StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName));
            sqlHandler.ExecuteNonQuery("usp_Aspx_CategoryTreeUpdate", parameterCollection);
            return true;
        }
    }
}