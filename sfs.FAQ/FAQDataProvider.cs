using Oracle.DataAccess.Client;
using SageFrame.FAQ.Info;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SageFrame.FAQ.DataProvider
{
    public class FAQDataProvider
    {
        public FAQDataProvider()
        {
        }

        public static void ApproveFAQ(int FAQId, bool IsApproved)
        {
            List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@FAQId", (object)FAQId),
                new KeyValuePair<string, object>("@IsApproved", (object)IsApproved)
            };
            try
            {
                (new OracleHandler()).ExecuteNonQuery("usp_FAQ_FAQApprove", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public static void DeleteCategory(int catID, int PortalID, int UserModuleID)
        {
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@catID", (object)catID),
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID)
                };
                (new OracleHandler()).ExecuteNonQuery("[usp_FAQ_DeleteCategory]", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteFAQ(int FAQId, int PortalID, int UserModuleID)
        {
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@FAQId", (object)FAQId),
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID)
                };
                (new OracleHandler()).ExecuteNonQuery("[usp_FAQ_DeleteFAQ]", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteReview(int ReviewID)
        {
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@ReviewID", (object)ReviewID)
                };
                (new OracleHandler()).ExecuteNonQuery("[dbo].[usp_FAQ_DeleteUserReview]", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static FAQInfo GetCategoryByID(int catID, int PortalID, int UserModuleID)
        {
            FAQInfo fAQInfo;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@catID", (object)catID),
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID)
                };
                fAQInfo = (new OracleHandler()).ExecuteAsObject<FAQInfo>("usp_FAQ_GetCategoryByID", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfo;
        }

        public static FAQInfo GetFAQByID(int FAQId, int PortalID, int UserModuleID)
        {
            FAQInfo fAQInfo;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@FAQId", (object)FAQId),
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID)
                };
                fAQInfo = (new OracleHandler()).ExecuteAsObject<FAQInfo>("usp_FAQ_GetRecordByID", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfo;
        }

        public List<FAQInfo> GetFaqCategory(int PortalID, int UserModuleID, string CultureName)
        {
            List<FAQInfo> fAQInfos;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@CultureName", CultureName)
                };
                OracleDataReader sqlDataReader = (new OracleHandler()).ExecuteAsDataReader("[dbo].[usp_FAQ_GetFAQCategory]", keyValuePairs);
                List<FAQInfo> fAQInfos1 = new List<FAQInfo>();
                while (sqlDataReader.Read())
                {
                    FAQInfo fAQInfo = new FAQInfo()
                    {
                        CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]),
                        CategoryName = sqlDataReader["CategoryName"].ToString()
                    };
                    fAQInfos1.Add(fAQInfo);
                }
                fAQInfos = fAQInfos1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfos;
        }

        public List<FAQInfo> GetFAQList(int PortalID, int UserModuleID, string CultureName, int offset, int limit)
        {
            List<FAQInfo> fAQInfos;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@CultureName", CultureName),
                    new KeyValuePair<string, object>("@offset", (object)offset),
                    new KeyValuePair<string, object>("@limit", (object)limit)
                };
                OracleDataReader  sqlDataReader = (new OracleHandler()).ExecuteAsDataReader("[dbo].[usp_FAQ_GetFAQViewList]", keyValuePairs);
                List<FAQInfo> fAQInfos1 = new List<FAQInfo>();
                while (sqlDataReader.Read())
                {
                    FAQInfo fAQInfo = new FAQInfo()
                    {
                        CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]),
                        RowTotal = Convert.ToInt32(sqlDataReader["RowTotal"]),
                        FAQId = Convert.ToInt32(sqlDataReader["FAQId"]),
                        Question = sqlDataReader["Question"].ToString(),
                        Answer = sqlDataReader["Answer"].ToString(),
                        CategoryName = sqlDataReader["CategoryName"].ToString()
                    };
                    fAQInfos1.Add(fAQInfo);
                }
                fAQInfos = fAQInfos1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfos;
        }

        public List<FAQInfo> GetFaqUserReview(int FAQId)
        {
            List<FAQInfo> fAQInfos;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@FAQId", (object)FAQId)
                };
                OracleDataReader sqlDataReader = (new OracleHandler()).ExecuteAsDataReader("[dbo].[usp_FAQ_GetFAQReview]", keyValuePairs);
                List<FAQInfo> fAQInfos1 = new List<FAQInfo>();
                while (sqlDataReader.Read())
                {
                    FAQInfo fAQInfo = new FAQInfo()
                    {
                        Review = sqlDataReader["Review"].ToString(),
                        userEmail = sqlDataReader["userEmail"].ToString(),
                        AddedOn = sqlDataReader["AddedOn"].ToString(),
                        UserReviewID = Convert.ToInt32(sqlDataReader["UserReviewID"])
                    };
                    fAQInfos1.Add(fAQInfo);
                }
                fAQInfos = fAQInfos1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfos;
        }

        public List<FAQInfo> GetGraphDetails(int FAQId)
        {
            List<FAQInfo> fAQInfos;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@FAQId", (object)FAQId)
                };
                OracleDataReader sqlDataReader = (new OracleHandler()).ExecuteAsDataReader("[dbo].[usp_FAQ_GetFAQGraphDetails]", keyValuePairs);
                List<FAQInfo> fAQInfos1 = new List<FAQInfo>();
                while (sqlDataReader.Read())
                {
                    FAQInfo fAQInfo = new FAQInfo()
                    {
                        Option = sqlDataReader["Option"].ToString(),
                        Counter = Convert.ToInt32(sqlDataReader["Counter"])
                    };
                    fAQInfos1.Add(fAQInfo);
                }
                fAQInfos = fAQInfos1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfos;
        }

        public List<FAQInfo> GetSearchList(int PortalID, int UserModuleID, string CultureName, string SearchWord)
        {
            List<FAQInfo> fAQInfos;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@CultureName", CultureName),
                    new KeyValuePair<string, object>("@SearchWord", SearchWord)
                };
                OracleDataReader sqlDataReader = (new OracleHandler()).ExecuteAsDataReader("[dbo].[usp_FAQ_GetSearchList]", keyValuePairs);
                List<FAQInfo> fAQInfos1 = new List<FAQInfo>();
                while (sqlDataReader.Read())
                {
                    FAQInfo fAQInfo = new FAQInfo()
                    {
                        CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]),
                        FAQId = Convert.ToInt32(sqlDataReader["FAQId"]),
                        Question = sqlDataReader["Question"].ToString(),
                        Answer = sqlDataReader["Answer"].ToString(),
                        CategoryName = sqlDataReader["CategoryName"].ToString()
                    };
                    fAQInfos1.Add(fAQInfo);
                }
                fAQInfos = fAQInfos1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfos;
        }

        public FAQInfo GetUserInfo(int PortalID, int UserModuleID, int FAQId)
        {
            FAQInfo fAQInfo;
            try
            {
                FAQInfo fAQInfo1 = new FAQInfo();
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@FAQId", (object)FAQId)
                };
                fAQInfo = (new OracleHandler()).ExecuteAsObject<FAQInfo>("[usp_FAQ_GetUserInfo]", keyValuePairs);
            }
            catch
            {
                throw;
            }
            return fAQInfo;
        }

        public static SqlDataReader LoadCategory(int PortalID, int UserModuleID, string CultureName)
        {
            SqlDataReader sqlDataReader;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand sqlCommand = new SqlCommand()
                {
                    Connection = sqlConnection,
                    CommandText = "usp_FAQ_GetCategoryList",
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.Add(new SqlParameter("@PortalID", (object)PortalID));
                sqlCommand.Parameters.Add(new SqlParameter("@UserModuleID", (object)UserModuleID));
                sqlCommand.Parameters.Add(new SqlParameter("@CultureName", CultureName));
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return sqlDataReader;
        }

        public List<FAQInfo> LoadCategoryOnGrid(int PortalID, int UserModuleID, string CultureName)
        {
            List<FAQInfo> fAQInfos;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@CultureName", CultureName)
                };
                OracleDataReader sqlDataReader = (new OracleHandler()).ExecuteAsDataReader("[dbo].[usp_FAQ_LoadCategoryOnGrid]", keyValuePairs);
                List<FAQInfo> fAQInfos1 = new List<FAQInfo>();
                while (sqlDataReader.Read())
                {
                    FAQInfo fAQInfo = new FAQInfo()
                    {
                        CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]),
                        CategoryName = sqlDataReader["CategoryName"].ToString()
                    };
                    fAQInfos1.Add(fAQInfo);
                }
                fAQInfos = fAQInfos1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfos;
        }

        public List<FAQInfo> LoadFAQList(int PortalID, int UserModuleID, string CultureName)
        {
            List<FAQInfo> fAQInfos;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@CultureName", CultureName)
                };
                OracleDataReader sqlDataReader = (new OracleHandler()).ExecuteAsDataReader("[dbo].[usp_FAQ_LoadFAQList]", keyValuePairs);
                List<FAQInfo> fAQInfos1 = new List<FAQInfo>();
                while (sqlDataReader.Read())
                {
                    FAQInfo fAQInfo = new FAQInfo()
                    {
                        FAQId = Convert.ToInt32(sqlDataReader["FAQId"]),
                        AddedBy = sqlDataReader["AddedBy"].ToString(),
                        EmailAddress = sqlDataReader["EmailAddress"].ToString(),
                        Question = sqlDataReader["Question"].ToString(),
                        Answer = sqlDataReader["Answer"].ToString(),
                        IsActive = Convert.ToBoolean(sqlDataReader["IsActive"]),
                        AddedOn = sqlDataReader["AddedOn"].ToString()
                    };
                    fAQInfos1.Add(fAQInfo);
                }
                fAQInfos = fAQInfos1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfos;
        }

        public List<FAQInfo> LoadFAQListAsCategory(int PortalID, int UserModuleID, string CultureName, int CategoryID)
        {
            List<FAQInfo> fAQInfos;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@CultureName", CultureName),
                    new KeyValuePair<string, object>("@CategoryID", (object)CategoryID)
                };
                OracleDataReader sqlDataReader = (new OracleHandler()).ExecuteAsDataReader("[dbo].[usp_FAQ_LoadFAQListByCategory]", keyValuePairs);
                List<FAQInfo> fAQInfos1 = new List<FAQInfo>();
                while (sqlDataReader.Read())
                {
                    FAQInfo fAQInfo = new FAQInfo()
                    {
                        FAQId = Convert.ToInt32(sqlDataReader["FAQId"]),
                        AddedBy = sqlDataReader["AddedBy"].ToString(),
                        EmailAddress = sqlDataReader["EmailAddress"].ToString(),
                        Question = sqlDataReader["Question"].ToString(),
                        Answer = sqlDataReader["Answer"].ToString(),
                        IsActive = Convert.ToBoolean(sqlDataReader["IsActive"]),
                        AddedOn = sqlDataReader["AddedOn"].ToString()
                    };
                    fAQInfos1.Add(fAQInfo);
                }
                fAQInfos = fAQInfos1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfos;
        }

        public static List<FAQInfo> LoadSearchFAQList(int PortalID, int UserModuleID, string CultureName, string SearchWord)
        {
            List<FAQInfo> fAQInfos;
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@CultureName", CultureName),
                    new KeyValuePair<string, object>("@SearchWord", SearchWord)
                };
                fAQInfos = (new OracleHandler()).ExecuteAsList<FAQInfo>("usp_FAQ_GetSearchListAdmin", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return fAQInfos;
        }

        public static void SaveFAQ(FAQInfo obj)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand sqlCommand = new SqlCommand()
                {
                    Connection = sqlConnection,
                    CommandText = "usp_FAQ_AddFAQ",
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.Add(new SqlParameter("@FAQId", (object)obj.FAQId));
                sqlCommand.Parameters.Add(new SqlParameter("@CategoryID", (object)obj.CategoryID));
                sqlCommand.Parameters.Add(new SqlParameter("@UserName", obj.UserName));
                sqlCommand.Parameters.Add(new SqlParameter("@EmailAddress", obj.EmailAddress));
                sqlCommand.Parameters.Add(new SqlParameter("@Question", obj.Question));
                sqlCommand.Parameters.Add(new SqlParameter("@Answer", obj.Answer));
                sqlCommand.Parameters.Add(new SqlParameter("@AddedBy", obj.AddedBy));
                sqlCommand.Parameters.Add(new SqlParameter("@IsActive", (object)1));
                sqlCommand.Parameters.Add(new SqlParameter("@PortalID", (object)obj.PortalID));
                sqlCommand.Parameters.Add(new SqlParameter("@UserModuleID", (object)obj.UserModuleID));
                sqlCommand.Parameters.Add(new SqlParameter("@IsAdmin", (object)1));
                sqlCommand.Parameters.Add(new SqlParameter("@CultureName", obj.CultureName));
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitFAQViewOption(int PortalID, int UserModuleID, int FAQId, int OptionId, string CultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@FAQId", (object)FAQId),
                    new KeyValuePair<string, object>("@OptionId", (object)OptionId),
                    new KeyValuePair<string, object>("@CultureName", CultureName)
                };
                (new OracleHandler()).ExecuteNonQuery("[dbo].[usp_FAQ_AddViewOption]", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitQuestion(int PortalID, int UserModuleID, string UserName, string EmailAddress, string Question, string CultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                    new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                    new KeyValuePair<string, object>("@UserName", UserName),
                    new KeyValuePair<string, object>("@EmailAddress", EmailAddress),
                    new KeyValuePair<string, object>("@Question", Question),
                    new KeyValuePair<string, object>("@IsActive", (object)0),
                    new KeyValuePair<string, object>("@FAQId", (object)0),
                    new KeyValuePair<string, object>("@Answer", ""),
                    new KeyValuePair<string, object>("@AddedBy", UserName),
                    new KeyValuePair<string, object>("@IsAdmin", (object)0),
                    new KeyValuePair<string, object>("@CultureName", CultureName),
                    new KeyValuePair<string, object>("@CategoryID", (object)1)
                };
                (new OracleHandler()).ExecuteNonQuery("[dbo].[usp_FAQ_AddFAQ]", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitReason(int FaqID, string Review, string userEmail, string CultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@FaqID", (object)FaqID),
                    new KeyValuePair<string, object>("@Review", Review),
                    new KeyValuePair<string, object>("@userEmail", userEmail),
                    new KeyValuePair<string, object>("@CultureName", CultureName)
                };
                (new OracleHandler()).ExecuteNonQuery("[dbo].[usp_FAQ_SubmitReason]", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateCategory(int catID, int PortalID, int UserModuleID, string CategoryName, string AddedBy, string CultureName)
        {
            List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("@catID", (object)catID),
                new KeyValuePair<string, object>("@PortalID", (object)PortalID),
                new KeyValuePair<string, object>("@UserModuleID", (object)UserModuleID),
                new KeyValuePair<string, object>("@CategoryName", CategoryName),
                new KeyValuePair<string, object>("@AddedBy", AddedBy),
                new KeyValuePair<string, object>("@CultureName", CultureName)
            };
            try
            {
                (new OracleHandler()).ExecuteNonQuery("usp_FAQ_FAQAdd_UpdateCategory", keyValuePairs);
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}