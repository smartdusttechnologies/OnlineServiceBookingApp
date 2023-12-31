﻿using Dapper;
using System.Data;
using ServiceBooking.Business.Data.Repository.Interfaces;
using ServcieBooking.Business.Infrastructure;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Buisness.Core.Model;

namespace ServiceBooking.Business.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public RoleRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        /// <summary>
        /// Get Role with Orgnization based on UserName
        /// </summary>
        public List<(int, string)> GetRoleWithOrg(string userName)
        {
            using IDbConnection db = _connectionFactory.GetConnection;
            string query = @"Select u.OrgId, r.Name from [User] u 
                                inner join [UserRole] ur on u.id = ur.userId
                                inner join [Role] r on r.Id = ur.RoleId
                             where u.UserName=@userName and u.IsDeleted=0 and r.IsDeleted=0 and ur.IsDeleted=0";

            return db.Query<(int, string)>(query, new { userName }).ToList();
        }
        public UserModel GetUserByUserName(string userName)
        {
            using IDbConnection db = _connectionFactory.GetConnection;
            string query = @"Select * from [User] u 
                             where u.UserName=@userName and u.IsDeleted=0";

            return db.Query<UserModel>(query, new { userName }).FirstOrDefault();
        }
        ///// <summary>
        ///// Get Role by Orgnization including Claims
        ///// </summary>
        //public List<UserRoleClaim> GetRoleByOrganizationWithClaims(string userName)
        //{
        //    using IDbConnection db = _connectionFactory.GetConnection;
        //    string query = @"Select u.OrgId, r.Name as RoleName, ur.RoleId, ur.UserId, p.Name as PermissionName, pt.Name as ClaimName 
        //                    from [User] u 
        //                        inner join [UserRole] ur on u.id = ur.userId
        //                        inner join [Role] r on r.Id = ur.RoleId
        //                        inner join [UserRoleClaim] urc on urc.RoleId = ur.RoleId
        //                        inner join [Permission] p on p.Id = urc.PermissionId
        //                        inner join [PermissionType] pt on pt.Id = p.PermissionTypeId
        //                    where u.UserName=@userName 
        //                        and u.IsDeleted=0  
        //                        and r.IsDeleted=0 
        //                        and p.IsDeleted=0 
        //                        and urc.IsDeleted=0
        //                        and pt.IsDeleted=0
        //                        and ur.IsDeleted=0";

        //    return db.Query<UserRoleClaim>(query, new { userName }).ToList();
        //}

        ///// <summary>
        ///// Get Role by Orgnization including Claims
        ///// </summary>
        //public List<UserClaim> GetUserByOrganizationWithClaims(string userName)
        //{
        //    using IDbConnection db = _connectionFactory.GetConnection;
        //    string query = @"Select u.OrgId, uc.userId, p.Name as PermissionName, pt.Name as ClaimName 
        //                    from [User] u 
        //                        inner join [UserClaim] uc on uc.userId = u.Id
        //                        inner join [Permission] p on p.Id = uc.PermissionId
        //                        inner join [PermissionType] pt on pt.Id = p.PermissionTypeId
        //                    where u.UserName=@userName 
        //                        and u.IsDeleted=0  
        //                        and p.IsDeleted=0 
        //                        and uc.IsDeleted=0
        //                        and pt.IsDeleted=0";

        //    return db.Query<UserClaim>(query, new { userName }).ToList();
        //}
        public List<GroupClaim> GetGroupClaims(int organizationId, int userId,string permissionModuleType, string subPermissionModuleType, CustomClaimType claimType)
        {
            using IDbConnection db = _connectionFactory.GetConnection;
            string query = @"  Select gc.ClaimTypeId as claimType, pt.Name as ClaimValue
                            from [UserGroup] g
                                inner join [GroupClaim] gc on gc.GroupId = g.GroupId
                                inner join [PermissionModuleType] pmt on pmt.Name = @permissionModuleType
								inner join [SubPermissionModuleType] spmt on pmt.Id = spmt.PermissionModuleTypeId and spmt.Name = @subPermissionModuleType
                                inner join [Permission] p on p.Id = gc.PermissionId and p.PermissionModuleTypeId=pmt.Id
                                inner join [PermissionType] pt on pt.Id = p.PermissionTypeId
                            where g.UserId=@userId
                                and gc.ClaimTypeId=@claimType
                                and gc.IsDeleted=0  
                                and p.IsDeleted=0 
                                and g.IsDeleted=0
                                and pt.IsDeleted=0
                                and pmt.IsDeleted=0";

            return db.Query<GroupClaim>(query, new { userId, organizationId, claimType, permissionModuleType, subPermissionModuleType }).ToList();
        }
        /// <summary>
        /// Get Role by Orgnization including Claims
        /// </summary>
        public List<UserClaim> GetUserClaims(int organizationId, int userId, string permissionModuleType, string subPermissionModuleType, CustomClaimType claimType)
        {
            using IDbConnection db = _connectionFactory.GetConnection;
            string query = @" Select uc.ClaimTypeId as claimType, pt.Name as ClaimValue 
                            from [User] u 
                                inner join [UserClaim] uc on uc.userId = u.Id
                                inner join [PermissionModuleType] pmt on pmt.Name = @permissionModuleType
                                inner join [SubPermissionModuleType] spmt on pmt.Id = spmt.PermissionModuleTypeId and spmt.Name = @subPermissionModuleType
								inner join [Permission] p on p.Id = uc.PermissionId and p.PermissionModuleTypeId=pmt.Id
                                inner join [PermissionType] pt on pt.Id = p.PermissionTypeId
                            where u.Id=@userId 
                                and u.OrgId=@organizationId
                                and uc.ClaimTypeId=@claimType
                                and u.IsDeleted=0  
                                and p.IsDeleted=0 
                                and uc.IsDeleted=0
                                and pmt.IsDeleted=0
                                and pt.IsDeleted=0
								and spmt.IsDeleted=0";

            return db.Query<UserClaim>(query, new { userId, organizationId, permissionModuleType, claimType, subPermissionModuleType }).ToList();
        }
        public List<UserRoleClaim> GetUserRoleClaims(int organizationId, int userId, string permissionModuleType, string subPermissionModuleType, CustomClaimType claimType)
        {
            using IDbConnection db = _connectionFactory.GetConnection;
            string query = @"Select urc.ClaimTypeId as claimType, pt.Name as ClaimValue 
                            from [User] u 
                                inner join [UserRole] ur on u.id = ur.userId
                                inner join [RoleClaim] urc on urc.RoleId = ur.RoleId
                                inner join [PermissionModuleType] pmt on pmt.Name = @permissionModuleType
                                inner join [SubPermissionModuleType] spmt on pmt.Id = spmt.PermissionModuleTypeId and spmt.Name = @subPermissionModuleType
                                inner join [Permission] p on p.Id = urc.PermissionId and p.PermissionModuleTypeId=pmt.Id
                                inner join [PermissionType] pt on pt.Id = p.PermissionTypeId
                            where u.Id=@userId
                                and u.OrgId=@organizationId
                                and urc.ClaimTypeId=@claimType
                                and u.IsDeleted=0  
                                and ur.IsDeleted=0 
                                and p.IsDeleted=0 
                                and urc.IsDeleted=0
                                and pmt.IsDeleted=0
                                and pt.IsDeleted=0";

            return db.Query<UserRoleClaim>(query, new { userId, organizationId, permissionModuleType, claimType, subPermissionModuleType }).ToList();
        }
        //List<string> GetRequiredClaimsForModule(PermissionModuleType permissionModuleType)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
