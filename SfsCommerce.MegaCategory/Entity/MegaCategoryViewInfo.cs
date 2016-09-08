using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AspxCommerce.MegaCategory
{
    [DataContract]
    [Serializable]
    public class MegaCategoryViewInfo
    {
        private int _categoryID;

        private string _categoryName;

        private string _levelCategoryName;

        private int _parentID;

        private int? _categoryLevel;

        private string _path;

        private string _attributeValue;

        private string _catImagePath;

        private int _productCount;

        private int _childCount;

        [DataMember]
        public string AttributeValue
        {
            get
            {
                return this._attributeValue;
            }
            set
            {
                this._attributeValue = value;
            }
        }

        [DataMember]
        public int CategoryID
        {
            get
            {
                return this._categoryID;
            }
            set
            {
                this._categoryID = value;
            }
        }

        [DataMember]
        public string CategoryImagePath
        {
            get
            {
                return this._catImagePath;
            }
            set
            {
                this._catImagePath = value;
            }
        }

        [DataMember]
        public int? CategoryLevel
        {
            get
            {
                return this._categoryLevel;
            }
            set
            {
                this._categoryLevel = value;
            }
        }

        [DataMember]
        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }
            set
            {
                this._categoryName = value;
            }
        }

        [DataMember]
        public int ChildCount
        {
            get
            {
                return this._childCount;
            }
            set
            {
                this._childCount = value;
            }
        }

        [DataMember]
        public string LevelCategoryName
        {
            get
            {
                return this._levelCategoryName;
            }
            set
            {
                this._levelCategoryName = value;
            }
        }

        [DataMember]
        public int ParentID
        {
            get
            {
                return this._parentID;
            }
            set
            {
                this._parentID = value;
            }
        }

        [DataMember]
        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
            }
        }

        [DataMember]
        public int ProductCount
        {
            get
            {
                return this._productCount;
            }
            set
            {
                this._productCount = value;
            }
        }

        public MegaCategoryViewInfo()
        {
        }
    }
}