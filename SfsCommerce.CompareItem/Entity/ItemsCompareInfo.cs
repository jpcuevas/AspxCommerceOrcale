using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AspxCommerce.CompareItem
{
    [DataContract]
    [Serializable]
    public class ItemsCompareInfo
    {
        #region "Private Members"
        int _compareItemID;
        int _itemID;
        string _costVariantValueID;
        string _itemCostVariantValue;
        string _itemName;
        string _sku;
        string _imagePath;
        #endregion
        #region "Constructors"
        public ItemsCompareInfo()
        {
        }
        public ItemsCompareInfo(int compareItemID, int itemID, string costVariantValueID, string itemCostVariantValue, string itemName, string sku)
        {
            this.CompareItemID = compareItemID;
            this.ItemID = itemID;
            this.CostVariantValueID = costVariantValueID;
            this.ItemCostVariantValue = itemCostVariantValue;
            this.ItemName = itemName;
            this.SKU = sku;
        }
        #endregion
        #region "Public Members"
        [DataMember]
        public int CompareItemID
        {
            get { return _compareItemID; }
            set { _compareItemID = value; }
        }
        [DataMember]
        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }
        [DataMember]
        public string CostVariantValueID
        {
            get { return _costVariantValueID; }
            set { _costVariantValueID = value; }
        }
        [DataMember]
        public string ItemCostVariantValue
        {
            get { return _itemCostVariantValue; }
            set { _itemCostVariantValue = value; }
        }
        [DataMember]
        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        [DataMember]
        public string SKU
        {
            get { return _sku; }
            set { _sku = value; }
        }
        [DataMember]
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        #endregion
    }
}