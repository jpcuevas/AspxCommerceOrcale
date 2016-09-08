using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.ServiceItem
{
    public class ServiceItemController
    {
        public ServiceItemController()
        {
        }

        public void DeleteAppointmentForError(int orderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                (new ServiceItemProvider()).DeleteAppointmentForError(orderId, aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ServiceCategoryInfo> GetAllServices(AspxCommonInfo aspxCommonObj)
        {
            List<ServiceCategoryInfo> allServices;
            try
            {
                allServices = (new ServiceItemProvider()).GetAllServices(aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return allServices;
        }

        public List<StoreLocatorInfo> GetAllStoresForService(AspxCommonInfo aspxCommonObj, int? serviceCategoryId)
        {
            List<StoreLocatorInfo> allStoresForService;
            try
            {
                allStoresForService = (new ServiceItemProvider()).GetAllStoresForService(aspxCommonObj, serviceCategoryId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return allStoresForService;
        }

        public List<FrontServiceCategoryView> GetFrontServices(AspxCommonInfo aspxCommonObj, int count)
        {
            List<FrontServiceCategoryView> frontServices;
            try
            {
                frontServices = (new ServiceItemProvider()).GetFrontServices(aspxCommonObj, count);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return frontServices;
        }

        public List<ServiceAvailableTime> GetServiceAvailableTime(GetServiceAvailableTime getServiceTimeObj, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceAvailableTime> serviceAvailableTime;
            try
            {
                serviceAvailableTime = (new ServiceItemProvider()).GetServiceAvailableTime(getServiceTimeObj, aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceAvailableTime;
        }

        public List<ServiceBookedTime> GetServiceBookedTime(GetServiceBookedTimeInfo bookedTimeObj, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceBookedTime> serviceBookedTime;
            try
            {
                serviceBookedTime = (new ServiceItemProvider()).GetServiceBookedTime(bookedTimeObj, aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceBookedTime;
        }

        public List<ServiceAvailableDate> GetServiceDates(GetServiceDate getServiceDateObj, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceAvailableDate> serviceDates;
            try
            {
                serviceDates = (new ServiceItemProvider()).GetServiceDates(getServiceDateObj, aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceDates;
        }

        public List<ServiceDetailsInfo> GetServiceDetails(string servicekey, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceDetailsInfo> serviceDetails;
            try
            {
                serviceDetails = (new ServiceItemProvider()).GetServiceDetails(servicekey, aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceDetails;
        }

        public List<ServiceCategoryDetailsInfo> GetServiceItemDetails(int itemID, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceCategoryDetailsInfo> serviceItemDetails;
            try
            {
                serviceItemDetails = (new ServiceItemProvider()).GetServiceItemDetails(itemID, aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceItemDetails;
        }

        public List<ServiceItemInfo> GetServiceItemInfo(AspxCommonInfo aspxCommonObj, int categoryId)
        {
            return (new ServiceItemProvider()).GetServiceItemInfo(aspxCommonObj, categoryId);
        }

        public List<ServiceItemSettingInfo> GetServiceItemSetting(AspxCommonInfo aspxCommonObj)
        {
            List<ServiceItemSettingInfo> serviceItemSetting;
            try
            {
                serviceItemSetting = (new ServiceItemProvider()).GetServiceItemSetting(aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceItemSetting;
        }

        public List<ServiceItemProductInfo> GetServiceProducts(int serviceId, AspxCommonInfo aspxCommonObj)
        {
            List<ServiceItemProductInfo> serviceProducts;
            try
            {
                serviceProducts = (new ServiceItemProvider()).GetServiceProducts(serviceId, aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceProducts;
        }

        public List<ServiceProviderInfo> GetServiceProviderNameListFront(AspxCommonInfo aspxCommonObj, int storeBranchId, int serviceCategoryId)
        {
            List<ServiceProviderInfo> serviceProviderNameListFront;
            try
            {
                serviceProviderNameListFront = (new ServiceItemProvider()).GetServiceProviderNameListFront(aspxCommonObj, storeBranchId, serviceCategoryId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceProviderNameListFront;
        }

        public List<ServiceItemRss> GetServiceTypeRssFeedContent(AspxCommonInfo aspxCommonObj, int count)
        {
            List<ServiceItemRss> serviceTypeRssContent;
            try
            {
                serviceTypeRssContent = (new ServiceItemProvider()).GetServiceTypeRssContent(aspxCommonObj, count);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return serviceTypeRssContent;
        }

        public bool SaveBookAppointment(int appointmentId, AspxCommonInfo aspxCommonObj, BookAnAppointmentInfo obj)
        {
            bool isSuccess = false;
            try
            {
                this.SetServiceSessionVariable("AppointmentCollection", obj);
                (new ServiceItemProvider()).SaveBookAppointment(appointmentId, aspxCommonObj, obj);
                isSuccess = true;
            }
            catch (Exception exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public void ServiceItemSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                (new ServiceItemProvider()).ServiceItemSettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SetServiceSessionVariable(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}