using AspxCommerce.Core;
using SageFrame.Message;
using SageFrame.SageFrameClass.MessageManagement;
using SageFrame.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.ServiceItem
{
    public class ServiceItemProvider
    {
        public ServiceItemProvider()
        {
        }

        public void DeleteAppointmentForError(int orderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("OrderID", (object)orderId));
                (new OracleHandler()).ExecuteNonQuery("usp_Aspx_DeleteAppointmentForError", parameter);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ServiceCategoryInfo> GetAllServices(AspxCommonInfo aspxCommonObj)
        {
            List<ServiceCategoryInfo> serviceCategoryInfos;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                serviceCategoryInfos = (new OracleHandler()).ExecuteAsList<ServiceCategoryInfo>("[usp_Aspx_GetAllServices]", parameterCollection);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceCategoryInfos;
        }

        public List<StoreLocatorInfo> GetAllStoresForService(AspxCommonInfo aspxCommonObj, int? serviceCategoryId)
        {
            List<StoreLocatorInfo> storeLocatorInfos;
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("ServiceID", (object)serviceCategoryId));
            try
            {
                storeLocatorInfos = (new OracleHandler()).ExecuteAsList<StoreLocatorInfo>("usp_Aspx_GetAllStoreForService", parameterCollection);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return storeLocatorInfos;
        }

        public string[] GetAllToken(string template)
        {
            List<string> returnValue = new List<string>();
            int preIndex = template.IndexOf('%', 0);
            int postIndex = template.IndexOf('%', preIndex + 1);
            while (preIndex > -1)
            {
                returnValue.Add(template.Substring(preIndex, postIndex - preIndex + 1));
                template = template.Substring(postIndex + 1, template.Length - postIndex - 1);
                preIndex = template.IndexOf('%', 0);
                postIndex = template.IndexOf('%', preIndex + 1);
            }
            return returnValue.ToArray();
        }

        public List<FrontServiceCategoryView> GetFrontServices(AspxCommonInfo aspxCommonObj, int count)
        {
            List<FrontServiceCategoryView> frontServiceCategoryViews;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("Count", (object)count));
                frontServiceCategoryViews = (new OracleHandler()).ExecuteAsList<FrontServiceCategoryView>("usp_Aspx_GetFrontServices", parameterCollection);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return frontServiceCategoryViews;
        }

        public List<ServiceAvailableTime> GetServiceAvailableTime(GetServiceAvailableTime getServiceTimeObj, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceAvailableTime> serviceAvailableTimes;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("CategoryID", (object)getServiceTimeObj.CategoryID));
                parameter.Add(new KeyValuePair<string, object>("BranchID", (object)getServiceTimeObj.BranchID));
                parameter.Add(new KeyValuePair<string, object>("EmployeeID", (object)getServiceTimeObj.EmployeeID));
                parameter.Add(new KeyValuePair<string, object>("ServiceDateID", getServiceTimeObj.ServiceDateID));
                parameter.Add(new KeyValuePair<string, object>("ServiceDate", getServiceTimeObj.ServiceDate));
                parameter.Add(new KeyValuePair<string, object>("ItemID", (object)getServiceTimeObj.ItemID));
                serviceAvailableTimes = (new OracleHandler()).ExecuteAsList<ServiceAvailableTime>("usp_Aspx_GetServiceTime", parameter);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceAvailableTimes;
        }

        public List<ServiceBookedTime> GetServiceBookedTime(GetServiceBookedTimeInfo bookedTimeObj, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceBookedTime> serviceBookedTimes;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ServiceCategoryID", (object)bookedTimeObj.ServiceCategoryID));
                parameter.Add(new KeyValuePair<string, object>("BranchID", (object)bookedTimeObj.BranchID));
                parameter.Add(new KeyValuePair<string, object>("EmployeeID", (object)bookedTimeObj.EmployeeID));
                parameter.Add(new KeyValuePair<string, object>("ServiceDateID", bookedTimeObj.ServiceDateID));
                parameter.Add(new KeyValuePair<string, object>("ServiceDate", bookedTimeObj.ServiceDate));
                parameter.Add(new KeyValuePair<string, object>("ServiceTimeID", (object)bookedTimeObj.ServiceTimeID));
                parameter.Add(new KeyValuePair<string, object>("ItemID", (object)bookedTimeObj.ItemID));
                serviceBookedTimes = (new OracleHandler()).ExecuteAsList<ServiceBookedTime>("usp_Aspx_GetServiceBookedTime", parameter);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceBookedTimes;
        }

        public List<ServiceAvailableDate> GetServiceDates(GetServiceDate getServiceDateObj, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceAvailableDate> serviceAvailableDates;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ServiceID", (object)getServiceDateObj.ServiceID));
                parameter.Add(new KeyValuePair<string, object>("BranchID", (object)getServiceDateObj.BranchID));
                parameter.Add(new KeyValuePair<string, object>("EmployeeID", (object)getServiceDateObj.EmployeeID));
                serviceAvailableDates = (new OracleHandler()).ExecuteAsList<ServiceAvailableDate>("usp_Aspx_GetServiceDates", parameter);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceAvailableDates;
        }

        public List<ServiceDetailsInfo> GetServiceDetails(string servicekey, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceDetailsInfo> serviceDetailsInfos;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("ServiceKey", servicekey));
                serviceDetailsInfos = (new OracleHandler()).ExecuteAsList<ServiceDetailsInfo>("usp_Aspx_GetServiceDetails", parameterCollection);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceDetailsInfos;
        }

        public List<ServiceCategoryDetailsInfo> GetServiceItemDetails(int itemID, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceCategoryDetailsInfo> serviceCategoryDetailsInfos;
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("ItemID", (object)itemID));
                serviceCategoryDetailsInfos = (new OracleHandler()).ExecuteAsList<ServiceCategoryDetailsInfo>("usp_Aspx_GetServiceItemDetails", parameterCollection);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceCategoryDetailsInfos;
        }

        public List<ServiceItemInfo> GetServiceItemInfo(AspxCommonInfo aspxCommonObj, int categoryId)
        {
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("Option", "serviceInfo"));
            parameter.Add(new KeyValuePair<string, object>("CategoryID", (object)categoryId));
            OracleHandler sqlH = new OracleHandler();
            List<ServiceItemInfo> serviceInfo = sqlH.ExecuteAsList<ServiceItemInfo>("usp_Aspx_GetServiceItemInfo", parameter);
            parameter.Remove(new KeyValuePair<string, object>("Option", "serviceInfo"));
            foreach (ServiceItemInfo serviceItemInfo in serviceInfo)
            {
                parameter.Add(new KeyValuePair<string, object>("Option", "serviceEmployeeData"));
                parameter.Add(new KeyValuePair<string, object>("ServiceID", (object)serviceItemInfo.ServiceId));
                List<ServiceEmployeeInfo> serviceEmployeeDataInfo = sqlH.ExecuteAsList<ServiceEmployeeInfo>("usp_Aspx_GetServiceItemInfo", parameter);
                serviceItemInfo.EmployeeData = serviceEmployeeDataInfo;
                parameter.Remove(new KeyValuePair<string, object>("Option", "serviceEmployeeData"));
                parameter.Remove(new KeyValuePair<string, object>("ServiceID", (object)serviceItemInfo.ServiceId));
                foreach (ServiceEmployeeInfo serviceEmployeeInfo in serviceEmployeeDataInfo)
                {
                    parameter.Add(new KeyValuePair<string, object>("Option", "serviceDate"));
                    parameter.Add(new KeyValuePair<string, object>("ServiceID", (object)serviceItemInfo.ServiceId));
                    parameter.Add(new KeyValuePair<string, object>("ServiceEmployeeID", (object)serviceEmployeeInfo.ServiceEmployeeId));
                    List<ServiceDateInfo> serviceDateInfo = sqlH.ExecuteAsList<ServiceDateInfo>("usp_Aspx_GetServiceItemInfo", parameter);
                    serviceEmployeeInfo.AvailableDate = serviceDateInfo;
                    parameter.Remove(new KeyValuePair<string, object>("Option", "serviceDate"));
                    parameter.Remove(new KeyValuePair<string, object>("ServiceID", (object)serviceItemInfo.ServiceId));
                    parameter.Remove(new KeyValuePair<string, object>("ServiceEmployeeID", (object)serviceEmployeeInfo.ServiceEmployeeId));
                    foreach (ServiceDateInfo dateInfo in serviceDateInfo)
                    {
                        parameter.Add(new KeyValuePair<string, object>("Option", "serviceTime"));
                        parameter.Add(new KeyValuePair<string, object>("ServiceDateID", (object)dateInfo.ServiceDateId));
                        parameter.Add(new KeyValuePair<string, object>("ServiceEmployeeID", (object)serviceEmployeeInfo.ServiceEmployeeId));
                        dateInfo.ServiceTime = sqlH.ExecuteAsList<ServiceTimeInfo>("usp_Aspx_GetServiceItemInfo", parameter);
                        parameter.Remove(new KeyValuePair<string, object>("Option", "serviceTime"));
                        parameter.Remove(new KeyValuePair<string, object>("ServiceDateID", (object)dateInfo.ServiceDateId));
                        parameter.Remove(new KeyValuePair<string, object>("ServiceEmployeeID", (object)serviceEmployeeInfo.ServiceEmployeeId));
                    }
                }
            }
            return serviceInfo;
        }

        public List<ServiceItemSettingInfo> GetServiceItemSetting(AspxCommonInfo aspxCommonObj)
        {
            List<ServiceItemSettingInfo> serviceItemSettingInfos;
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                serviceItemSettingInfos = (new OracleHandler()).ExecuteAsList<ServiceItemSettingInfo>("usp_Aspx_ServiceItemSettingGet", paramCol);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceItemSettingInfos;
        }

        public List<ServiceItemProductInfo> GetServiceProducts(int serviceId, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceItemProductInfo> serviceItemProductInfos;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("ServiceID", (object)serviceId));
                serviceItemProductInfos = (new OracleHandler()).ExecuteAsList<ServiceItemProductInfo>("usp_Aspx_GetServiceProducts", parameter);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceItemProductInfos;
        }

        public List<ServiceProviderInfo> GetServiceProviderNameListFront(AspxCommonInfo aspxCommonObj, int storeBranchId, int serviceCategoryId)
        {
            List<ServiceProviderInfo> serviceProviderInfos;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("StoreBranchID", (object)storeBranchId));
                parameter.Add(new KeyValuePair<string, object>("ServiceCategoryID", (object)serviceCategoryId));
                serviceProviderInfos = (new OracleHandler()).ExecuteAsList<ServiceProviderInfo>("usp_Aspx_GetServiceProviderForStore", parameter);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceProviderInfos;
        }

        public List<ServiceItemRss> GetServiceTypeRssContent(AspxCommonInfo aspxCommonObj, int count)
        {
            List<ServiceItemRss> serviceItemRsses;
            try
            {
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("StoreID", (object)aspxCommonObj.StoreID),
                    new KeyValuePair<string, object>("PortalID", (object)aspxCommonObj.PortalID),
                    new KeyValuePair<string, object>("CultureName", aspxCommonObj.CultureName),
                    new KeyValuePair<string, object>("UserName", aspxCommonObj.UserName),
                    new KeyValuePair<string, object>("Count", (object)count)
                };
                serviceItemRsses = (new OracleHandler()).ExecuteAsList<ServiceItemRss>("usp_Aspx_GetRssFeedServiceTypeItem", Parameter);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceItemRsses;
        }

        public bool SaveBookAppointment(int appointmentId, AspxCommonInfo aspxCommonObj, BookAnAppointmentInfo obj)
        {
            bool isSuccess = false;
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("AppointmentID", (object)appointmentId));
                parameter.Add(new KeyValuePair<string, object>("OrderID", (object)obj.OrderID));
                parameter.Add(new KeyValuePair<string, object>("ServiceCategoryID", (object)obj.ServiceCategoryID));
                parameter.Add(new KeyValuePair<string, object>("ServiceProductID", (object)obj.ServiceProductID));
                parameter.Add(new KeyValuePair<string, object>("ServiceProductPrice", obj.ServiceProductPrice));
                parameter.Add(new KeyValuePair<string, object>("AppointmentStatusID", (object)obj.AppointmentStatusID));
                parameter.Add(new KeyValuePair<string, object>("Title", obj.Title));
                parameter.Add(new KeyValuePair<string, object>("FirstName", obj.FirstName));
                parameter.Add(new KeyValuePair<string, object>("LastName", obj.LastName));
                parameter.Add(new KeyValuePair<string, object>("Gender", obj.Gender));
                parameter.Add(new KeyValuePair<string, object>("Mobile", obj.MobileNumber));
                parameter.Add(new KeyValuePair<string, object>("Phone", obj.PhoneNumber));
                parameter.Add(new KeyValuePair<string, object>("Email", obj.Email));
                parameter.Add(new KeyValuePair<string, object>("PreferredDateID", (object)obj.ServiceDateId));
                parameter.Add(new KeyValuePair<string, object>("PreferredDate", (object)obj.PreferredDate));
                parameter.Add(new KeyValuePair<string, object>("PreferredTime", obj.PreferredTime));
                parameter.Add(new KeyValuePair<string, object>("TypeOfTreatment", obj.TypeOfTreatment));
                parameter.Add(new KeyValuePair<string, object>("StoreLocation", obj.StoreLocation));
                parameter.Add(new KeyValuePair<string, object>("CustomerType", obj.TypeOfCustomer));
                parameter.Add(new KeyValuePair<string, object>("MembershipElite", obj.MembershipElite));
                parameter.Add(new KeyValuePair<string, object>("UserName", obj.UserName));
                parameter.Add(new KeyValuePair<string, object>("PaymentMethodID", (object)obj.PaymentMethodID));
                parameter.Add(new KeyValuePair<string, object>("PreferredTimeInterval", obj.PreferredTimeInterval));
                parameter.Add(new KeyValuePair<string, object>("PreferredTimeID", (object)obj.PreferredTimeId));
                parameter.Add(new KeyValuePair<string, object>("EmployeeID", (object)obj.EmployeeID));
                (new OracleHandler()).ExecuteNonQuery("usp_Aspx_AddAppointment", parameter);
                if (!(appointmentId == 0 ? true : !(obj.AppointmentStatusName.ToLower() == "completed")))
                {
                    this.SendMailNotificatiion(aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName, obj);
                }
                else if ((appointmentId != 0 ? false : obj.AppointmentStatusName.ToLower() == "pending"))
                {
                    this.SendMailNotificatiion(aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName, obj);
                }
                isSuccess = true;
            }
            catch (Exception exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public void SendMailNotificatiion(int storeId, int portalId, string cultureName, BookAnAppointmentInfo objInfo)
        {
            DateTime now;
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, storeId, portalId, cultureName);
            string name = "Appointment Approval - Email";
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("PortalID", (object)portalId),
                new KeyValuePair<string, object>("CultureName", cultureName),
                new KeyValuePair<string, object>("MessageTemplateTypeName", name)
            };
            int messageTemplateTypeId = (new OracleHandler()).ExecuteNonQuery("usp_Aspx_GetMessageTemplateTypeID", parameter, "MessageTemplateID");
            MessageManagementInfo template = (new MessageManagementController()).GetMessageTemplate(messageTemplateTypeId, portalId);
            string messageTemplate = template.Body;
            string src = string.Concat(HttpContext.Current.Request.ServerVariables["SERVER_NAME"], "/");
            string receiverEmailID = objInfo.Email;
            string subject = template.Subject;
            string senderEmail = template.MailFrom;
            string headerMsg = string.Empty;
            string customMessage = "";
            if (objInfo.AppointmentID > 0)
            {
                headerMsg = "status has been modified as follow.";
            }
            else if (objInfo.AppointmentID == 0)
            {
                headerMsg = "has been scheduled as following date and time.";
            }
            if (template != null)
            {
                string[] allToken = this.GetAllToken(messageTemplate);
                for (int i = 0; i < (int)allToken.Length; i++)
                {
                    string token = allToken[i];
                    string str = token;
                    if (str != null)
                    {
                        switch (str)
                        {
                            case "%LogoSource%":
                                {
                                    string imgSrc = string.Concat(src, logosrc);
                                    messageTemplate = messageTemplate.Replace(token, imgSrc);
                                    break;
                                }
                            case "%DateTime%":
                                {
                                    now = DateTime.Now;
                                    messageTemplate = messageTemplate.Replace(token, now.ToString("MM/dd/yyyy"));
                                    break;
                                }
                            case "%PreferredDate%":
                                {
                                    now = objInfo.PreferredDate;
                                    messageTemplate = messageTemplate.Replace(token, now.ToString("MM/dd/yyyy"));
                                    break;
                                }
                            case "%PreferredTime%":
                                {
                                    messageTemplate = messageTemplate.Replace(token, objInfo.PreferredTime);
                                    break;
                                }
                            case "%PreferredTimeInterval%":
                                {
                                    messageTemplate = messageTemplate.Replace(token, objInfo.PreferredTimeInterval);
                                    break;
                                }
                            case "%AppointmentStatus%":
                                {
                                    messageTemplate = messageTemplate.Replace(token, objInfo.AppointmentStatusName);
                                    break;
                                }
                            case "%ServerPath%":
                                {
                                    messageTemplate = messageTemplate.Replace(token, src);
                                    break;
                                }
                            case "%DateYear%":
                                {
                                    int year = DateTime.Now.Year;
                                    messageTemplate = messageTemplate.Replace(token, year.ToString());
                                    break;
                                }
                            case "%AppointmentHeadingMessage%":
                                {
                                    messageTemplate = messageTemplate.Replace(token, headerMsg);
                                    break;
                                }
                            case "%AppointmentCustomMessage%":
                                {
                                    messageTemplate = messageTemplate.Replace(token, customMessage);
                                    break;
                                }
                            case "%ServiceProductName%":
                                {
                                    messageTemplate = messageTemplate.Replace(token, objInfo.ServiceProductName);
                                    break;
                                }
                        }
                    }
                }
            }
            SageFrameConfig pagebase = new SageFrameConfig();
            string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
            string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
            MailHelper.SendMailNoAttachment(senderEmail, receiverEmailID, subject, messageTemplate, emailSiteAdmin, emailSuperAdmin);
        }

        public void ServiceItemSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSP(aspxCommonObj);
                paramCol.Add(new KeyValuePair<string, object>("SettingKeys", SettingKeys));
                paramCol.Add(new KeyValuePair<string, object>("SettingValues", SettingValues));
                (new OracleHandler()).ExecuteNonQuery("usp_Aspx_ServiceItemSettingsUpdate", paramCol);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}