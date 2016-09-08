using SageFrame.FAQ.DataProvider;
using SageFrame.FAQ.Info;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SageFrame.FAQ.Controller
{
    public class FAQController
    {
        public FAQController()
        {
        }

        public static void ApproveFAQ(int FAQId, bool IsApproved)
        {
            FAQDataProvider.ApproveFAQ(FAQId, IsApproved);
        }

        public void DeleteCategory(int catID, int PortalID, int UserModuleID)
        {
            FAQDataProvider.DeleteCategory(catID, PortalID, UserModuleID);
        }

        public void DeleteFAQ(int FAQId, int PortalID, int UserModuleID)
        {
            FAQDataProvider.DeleteFAQ(FAQId, PortalID, UserModuleID);
        }

        public void DeleteReview(int ReviewID)
        {
            FAQDataProvider.DeleteReview(ReviewID);
        }

        public FAQInfo GetCategoryByID(int catID, int PortalID, int UserModuleID)
        {
            return FAQDataProvider.GetCategoryByID(catID, PortalID, UserModuleID);
        }

        public FAQInfo GetFAQByID(int FAQId, int PortalID, int UserModuleID)
        {
            return FAQDataProvider.GetFAQByID(FAQId, PortalID, UserModuleID);
        }

        public List<FAQInfo> GetFaqCategory(int PortalID, int UserModuleID, string CultureName)
        {
            return (new FAQDataProvider()).GetFaqCategory(PortalID, UserModuleID, CultureName);
        }

        public List<FAQInfo> GetFAQList(int PortalID, int UserModuleID, string CultureName, int Offset, int limit)
        {
            FAQDataProvider fAQDataProvider = new FAQDataProvider();
            return fAQDataProvider.GetFAQList(PortalID, UserModuleID, CultureName, Offset, limit);
        }

        public List<FAQInfo> GetFaqUserReview(int FAQId)
        {
            return (new FAQDataProvider()).GetFaqUserReview(FAQId);
        }

        public List<FAQInfo> GetGraphDetails(int FAQId)
        {
            return (new FAQDataProvider()).GetGraphDetails(FAQId);
        }

        public List<FAQInfo> GetSearchList(int PortalID, int UserModuleID, string CultureName, string SearchWord)
        {
            return (new FAQDataProvider()).GetSearchList(PortalID, UserModuleID, CultureName, SearchWord);
        }

        public FAQInfo GetUserInfo(int PortalID, int UserModelID, int FAQId)
        {
            return (new FAQDataProvider()).GetUserInfo(PortalID, UserModelID, FAQId);
        }

        public SqlDataReader LoadCategory(int PortalID, int UserModuleID, string CultureName)
        {
            return FAQDataProvider.LoadCategory(PortalID, UserModuleID, CultureName);
        }

        public List<FAQInfo> LoadCategoryOnGrid(int PortalID, int UserModuleID, string CultureName)
        {
            return (new FAQDataProvider()).LoadCategoryOnGrid(PortalID, UserModuleID, CultureName);
        }

        public List<FAQInfo> LoadFAQList(int PortalID, int UserModuleID, string CultureName)
        {
            return (new FAQDataProvider()).LoadFAQList(PortalID, UserModuleID, CultureName);
        }

        public List<FAQInfo> LoadFAQListAsCategory(int PortalID, int UserModuleID, string CultureName, int CategoryID)
        {
            return (new FAQDataProvider()).LoadFAQListAsCategory(PortalID, UserModuleID, CultureName, CategoryID);
        }

        public List<FAQInfo> LoadSearchFAQList(int PortalID, int UserModuleID, string CultureName, string SearchWord)
        {
            return FAQDataProvider.LoadSearchFAQList(PortalID, UserModuleID, CultureName, SearchWord);
        }

        public void SaveFAQ(FAQInfo obj)
        {
            FAQDataProvider.SaveFAQ(obj);
        }

        public void SubmitFAQViewOption(int PortalID, int UserModuleID, int FAQId, int OptionId, string CultureName)
        {
            FAQDataProvider.SubmitFAQViewOption(PortalID, UserModuleID, FAQId, OptionId, CultureName);
        }

        public void SubmitQuestion(int PortalID, int UserModuleID, string UserName, string EmailAddress, string Question, string CultureName)
        {
            FAQDataProvider.SubmitQuestion(PortalID, UserModuleID, UserName, EmailAddress, Question, CultureName);
        }

        public void SubmitReason(int FAQId, string Reason, string userEmail, string CultureName)
        {
            FAQDataProvider.SubmitReason(FAQId, Reason, userEmail, CultureName);
        }

        public void UpdateCategory(int catID, int PortalID, int UserModuleID, string CategoryName, string AddedBy, string CultureName)
        {
            FAQDataProvider.UpdateCategory(catID, PortalID, UserModuleID, CategoryName, AddedBy, CultureName);
        }
    }
}