using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxKitProvider
    {
        public List<KitInfo> GetKits(int offset, int limit, string kitName, AspxCommonInfo commonInfo)
        {

            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(commonInfo);
                ParaMeter.Add(new KeyValuePair<string, object>("offset", offset));
                ParaMeter.Add(new KeyValuePair<string, object>("limit", limit));
                ParaMeter.Add(new KeyValuePair<string, object>("kitName", kitName));
                OracleHandler sqLH = new OracleHandler();
                List<KitInfo> kits = sqLH.ExecuteAsList<KitInfo>("usp_Aspx_GetKitsForGrid", ParaMeter);
                return kits;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<ItemKit> GetItemKits(int itemID, AspxCommonInfo commonInfo)
        {

            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(commonInfo);
                ParaMeter.Add(new KeyValuePair<string, object>("ItemID", itemID));
                OracleHandler sqLH = new OracleHandler();
                List<ItemKit> itemKits = sqLH.ExecuteAsList<ItemKit>("usp_Aspx_GetItemKits", ParaMeter);
                return itemKits;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void SaveItemKits(List<ItemKit> mappedKits, int itemID, AspxCommonInfo commonInfo)
        {
            try
            {
                foreach (var kit in mappedKits)
                {

                    KitComponent obj = new KitComponent() { KitComponentID = kit.KitComponentID, KitComponentName = kit.KitComponentName, KitComponentType = kit.KitComponentType };
                    kit.KitComponentID = SaveComponent(obj, commonInfo);

                    SaveItemKitConfig(kit, itemID, commonInfo);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void SaveItemKitConfig(ItemKit kitconfig, int itemId, AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                parameter.Add(new KeyValuePair<string, object>("KitComponentID", kitconfig.KitComponentID));
                parameter.Add(new KeyValuePair<string, object>("KitComponentOrder", kitconfig.KitComponentOrder));
                parameter.Add(new KeyValuePair<string, object>("Price", kitconfig.Price));
                parameter.Add(new KeyValuePair<string, object>("Quantity", kitconfig.Quantity));
                parameter.Add(new KeyValuePair<string, object>("Weight", kitconfig.Weight));
                parameter.Add(new KeyValuePair<string, object>("IsDefault", kitconfig.IsDefault));
                parameter.Add(new KeyValuePair<string, object>("KitID", kitconfig.KitID));
                parameter.Add(new KeyValuePair<string, object>("KitOrder", kitconfig.KitOrder));
                parameter.Add(new KeyValuePair<string, object>("ItemID", itemId));
                OracleHandler sqLh = new OracleHandler();
                sqLh.ExecuteNonQuery("usp_Aspx_MapKitToItem", parameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void DeleteItemKits(int kitComponentID, int kitID, int itemID, AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(commonInfo);
                parameter.Add(new KeyValuePair<string, object>("KitComponentID", kitComponentID));
                parameter.Add(new KeyValuePair<string, object>("KitID", kitID));
                parameter.Add(new KeyValuePair<string, object>("ItemID", itemID));
                OracleHandler sqLH = new OracleHandler();
                sqLH.ExecuteNonQuery("usp_Aspx_DeleteKitFromItem", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int SaveComponent(KitComponent kitcomponent, AspxCommonInfo commonInfo)
        {

            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                parameter.Add(new KeyValuePair<string, object>("KitComponentID", kitcomponent.KitComponentID));
                parameter.Add(new KeyValuePair<string, object>("ComponentName", kitcomponent.KitComponentName));
                parameter.Add(new KeyValuePair<string, object>("KitComponentType", kitcomponent.KitComponentType));
                OracleHandler sqLh = new OracleHandler();
                int kitComponentID = sqLh.ExecuteAsScalar<int>("usp_Aspx_AddUpdateKitComponent", parameter);
                return kitComponentID;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public int SaveKit(Kit kit, AspxCommonInfo commonInfo)
        {

            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                //  parameter.Add(new KeyValuePair<string, object>("@ID", kit.ID));
                parameter.Add(new KeyValuePair<string, object>("KitID", kit.KitID));
                parameter.Add(new KeyValuePair<string, object>("KitName", kit.KitName));
                parameter.Add(new KeyValuePair<string, object>("Price", kit.Price));
                parameter.Add(new KeyValuePair<string, object>("Quantity", kit.Quantity));
                parameter.Add(new KeyValuePair<string, object>("Weight", kit.Weight));
                parameter.Add(new KeyValuePair<string, object>("KitComponentID", kit.KitComponentID));

                OracleHandler sqLh = new OracleHandler();
                int kitComponentID = sqLh.ExecuteAsScalar<int>("usp_Aspx_AddUpdateKit", parameter);
                return kitComponentID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public List<KitComponent> GetComponents(AspxCommonInfo commonInfo)
        {
            try
            {


                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(commonInfo);
                OracleHandler sqLh = new OracleHandler();
                return sqLh.ExecuteAsList<KitComponent>("usp_Aspx_GetKitComponents", parameter);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Kit> GetKits(AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(commonInfo);
                OracleHandler sqLh = new OracleHandler();
                return sqLh.ExecuteAsList<Kit>("usp_Aspx_GetKits", parameter);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckKitComponentExist(string ComponentName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                OracleHandler sqlH = new OracleHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("ComponentName", ComponentName));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("usp_Aspx_CheckKitComponentExist", parameterCollection, "IsComponentExist");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void DeleteKit(string kitIds, AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                //  parameter.Add(new KeyValuePair<string, object>("@ID", kit.ID));
                parameter.Add(new KeyValuePair<string, object>("KitIDs", kitIds));

                OracleHandler sqLh = new OracleHandler();
                sqLh.ExecuteNonQuery("usp_Aspx_DeleteKit", parameter);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void DeleteKitComponent(string kitComponentIds, AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                //  parameter.Add(new KeyValuePair<string, object>("@ID", kit.ID));
                parameter.Add(new KeyValuePair<string, object>("KitComponentIDs", kitComponentIds));

                OracleHandler sqLh = new OracleHandler();
                sqLh.ExecuteNonQuery("usp_Aspx_DeleteKitComponent", parameter);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }






    }
}
