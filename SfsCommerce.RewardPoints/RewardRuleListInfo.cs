using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AspxCommerce.RewardPoints
{
    public class RewardRuleListInfo
    {
        private System.Nullable<int> _rewardRuleID;
        private string _rewardRuleType;


        [DataMember]
        public System.Nullable<int> RewardRuleID
        {
            get
            {
                return this._rewardRuleID;
            }
            set
            {
                if (this._rewardRuleID != value)
                {
                    this._rewardRuleID = value;
                }
            }
        }
        [DataMember]
        public string RewardRuleType
        {
            get
            {
                return this._rewardRuleType;
            }
            set
            {
                if (this._rewardRuleType != value)
                {
                    this._rewardRuleType = value;
                }
            }
        }


    }
}