using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SageFrame.RolesManagement
{
    public class RolesManagementController
    {
        public RolesManagementInfo GetRoleIDByRoleName(string RoleName)
        {
            try
            {
                RolesManagementProvider objProvider = new RolesManagementProvider();
                  return objProvider.GetRoleIDByRoleName(RoleName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<RolesManagementInfo> PortalRoleList(int PortalID, bool IsAll, string Username)
        {
            try
            {
                RolesManagementProvider objProvider = new RolesManagementProvider();
                return objProvider.PortalRoleList(PortalID, IsAll, Username);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RolesManagementInfo> GetPortalRoleSelectedList(int PortalID, string Username)
        {
            try
            {
                RolesManagementProvider objProvider = new RolesManagementProvider();
                return objProvider.GetPortalRoleSelectedList(PortalID, Username);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}