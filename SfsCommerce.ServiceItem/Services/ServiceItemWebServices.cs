using AspxCommerce.Core;
using AspxCommerce.ServiceItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

[ScriptService]
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ServiceItemWebServices : WebService
{
    public ServiceItemWebServices()
    {
    }

    [WebMethod]
    public void DeleteAppointmentForError(int orderId, AspxCommonInfo aspxCommonObj)
    {
        (new ServiceItemController()).DeleteAppointmentForError(orderId, aspxCommonObj);
    }

    [WebMethod]
    public List<ServiceCategoryInfo> GetAllServices(AspxCommonInfo aspxCommonObj)
    {
        List<ServiceCategoryInfo> allServices;
        try
        {
            allServices = (new ServiceItemController()).GetAllServices(aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return allServices;
    }

    [WebMethod]
    public List<StoreLocatorInfo> GetAllStoresForService(AspxCommonInfo aspxCommonObj, int? serviceCategoryId)
    {
        List<StoreLocatorInfo> allStoresForService;
        try
        {
            allStoresForService = (new ServiceItemController()).GetAllStoresForService(aspxCommonObj, serviceCategoryId);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return allStoresForService;
    }

    [WebMethod]
    public List<FrontServiceCategoryView> GetFrontServices(AspxCommonInfo aspxCommonObj, int count)
    {
        List<FrontServiceCategoryView> frontServices;
        try
        {
            frontServices = (new ServiceItemController()).GetFrontServices(aspxCommonObj, count);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return frontServices;
    }

    [WebMethod]
    public List<ServiceAvailableTime> GetServiceAvailableTime(GetServiceAvailableTime getServiceTimeObj, AspxCommonInfo aspxCommonObj)
    {
        List<ServiceAvailableTime> serviceAvailableTime;
        try
        {
            serviceAvailableTime = (new ServiceItemController()).GetServiceAvailableTime(getServiceTimeObj, aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return serviceAvailableTime;
    }

    [WebMethod]
    public List<ServiceBookedTime> GetServiceBookedTime(GetServiceBookedTimeInfo getServiceBookedTimeObj, AspxCommonInfo aspxCommonObj)
    {
        return (new ServiceItemController()).GetServiceBookedTime(getServiceBookedTimeObj, aspxCommonObj);
    }

    [WebMethod]
    public List<ServiceAvailableDate> GetServiceDates(GetServiceDate getServiceDateObj, AspxCommonInfo aspxCommonObj)
    {
        List<ServiceAvailableDate> serviceDates;
        try
        {
            serviceDates = (new ServiceItemController()).GetServiceDates(getServiceDateObj, aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return serviceDates;
    }

    [WebMethod]
    public List<ServiceDetailsInfo> GetServiceDetails(string servicekey, AspxCommonInfo aspxCommonObj)
    {
        List<ServiceDetailsInfo> serviceDetails;
        try
        {
            serviceDetails = (new ServiceItemController()).GetServiceDetails(servicekey, aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return serviceDetails;
    }

    [WebMethod]
    public List<ServiceCategoryDetailsInfo> GetServiceItemDetails(int itemID, AspxCommonInfo aspxCommonObj)
    {
        List<ServiceCategoryDetailsInfo> serviceItemDetails;
        try
        {
            serviceItemDetails = (new ServiceItemController()).GetServiceItemDetails(itemID, aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return serviceItemDetails;
    }

    [WebMethod]
    public List<ServiceItemSettingInfo> GetServiceItemSetting(AspxCommonInfo aspxCommonObj)
    {
        List<ServiceItemSettingInfo> serviceItemSetting;
        try
        {
            serviceItemSetting = (new ServiceItemController()).GetServiceItemSetting(aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return serviceItemSetting;
    }

    [WebMethod]
    public List<ServiceItemProductInfo> GetServiceProducts(int serviceId, AspxCommonInfo aspxCommonObj)
    {
        List<ServiceItemProductInfo> serviceProducts;
        try
        {
            serviceProducts = (new ServiceItemController()).GetServiceProducts(serviceId, aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return serviceProducts;
    }

    [WebMethod]
    public List<ServiceProviderInfo> GetServiceProviderNameListFront(AspxCommonInfo aspxCommonObj, int storeBranchId, int serviceCategoryId)
    {
        return (new ServiceItemController()).GetServiceProviderNameListFront(aspxCommonObj, storeBranchId, serviceCategoryId);
    }

    [WebMethod(EnableSession = true)]
    public bool SaveBookAppointment(int appointmentId, AspxCommonInfo aspxCommonObj, BookAnAppointmentInfo obj)
    {
        bool flag;
        try
        {
            flag = (new ServiceItemController()).SaveBookAppointment(appointmentId, aspxCommonObj, obj);
        }
        catch (Exception exception)
        {
            flag = false;
        }
        return flag;
    }

    [WebMethod]
    public void ServiceItemSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            (new ServiceItemController()).ServiceItemSettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
}