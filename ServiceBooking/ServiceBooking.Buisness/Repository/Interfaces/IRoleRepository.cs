﻿using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Core.Model.Security;

namespace ServiceBooking.Business.Data.Repository.Interfaces
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Abstract method to get Role for Orgnization
        /// </summary>
        List<(int, string)> GetRoleWithOrg(string userName);

        /// <summary>
        /// Abstract method to get Role by Organization including claims
        /// </summary>
        //List<UserRoleClaim> GetRoleByOrganizationWithClaims(string userName);
        //List<UserClaim> GetUserByOrganizationWithClaims(string userName);
        //TODO: move this to user Repository.
        UserModel GetUserByUserName(string userName);
        // List<string> GetRequiredClaimsForModule(PermissionModuleType permissionModuleType);
        List<GroupClaim> GetGroupClaims(int organizationId, int userId, string moduleId, string stageId, CustomClaimType claimType);
        List<UserRoleClaim> GetUserRoleClaims(int organizationId, int userId, string moduleId, string stageId, CustomClaimType claimType);
        List<UserClaim> GetUserClaims(int organizationId, int userId, string moduleId, string stageId, CustomClaimType claimType);
    }
}
