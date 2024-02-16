using Dapper;
using System.Data;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;
using ServcieBooking.Business.Infrastructure;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Buisness.Core.Models.Context;

namespace ServiceBooking.Business.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConnectionFactory _connectionFactory;
        public AuthenticationRepository(IConnectionFactory connectionFactory, AppDbContext appDbContext)
        {
            _connectionFactory = connectionFactory;
            _appDbContext = appDbContext;
        }
        public PasswordLogin GetLoginPassword(string userName)
        {
            //var query = from u in _appDbContext.Users
            //            where u.UserName == userName && !u.IsDeleted
            //            join pl in _appDbContext.PasswordLogins on u.Id equals pl.UserId
            //            join ur in _appDbContext.UserRoles on u.Id equals ur.UserId into urGroup
            //            from ur in urGroup.DefaultIfEmpty()
            //            orderby pl.Id descending
            //            select new { pl, ur.RoleId };

            //var result = query.FirstOrDefault().pl;
            
            using IDbConnection db = _connectionFactory.GetConnection;
            var query = @"SELECT TOP 1 pl.*, ur.RoleId
                         FROM [User] u
                         INNER JOIN [PasswordLogin] pl ON u.id = pl.userId
                         LEFT JOIN [UserRole] ur ON u.id = ur.UserId
                         WHERE u.userName = @userName AND (u.IsDeleted = 0)
                         ORDER BY pl.Id DESC";

            var result = db.QueryFirstOrDefault<PasswordLogin>(query, new { userName });
            return result;
        }
        /// <summary>
        /// Save Login Token in DB
        /// </summary>
        public int SaveLoginToken(LoginToken loginToken)
        {
            using IDbConnection db = _connectionFactory.GetConnection;
            int userId = db.Query<int>(@"Select u.Id From [User] u Where u.UserName = @UserName", new { loginToken.UserName }).FirstOrDefault();
            loginToken.UserId = userId;

            int loginTokenUserId = db.Query<int>(@"Select userId From [LoginToken] Where  UserId = @userId", new { userId }).FirstOrDefault();

            string query = loginTokenUserId > 0 ?
              @"update [LoginToken] Set 
                    AccessToken = @AccessToken,
                    RefreshToken = @RefreshToken,
                    AccessTokenExpiry = @AccessTokenExpiry,
                    DeviceCode = @DeviceCode,
                    DeviceName = @DeviceName,
                    RefreshTokenExpiry = @RefreshTokenExpiry
                  Where UserId = @UserId"
              :
              @"Insert into [LoginToken](UserId, AccessToken, RefreshToken, AccessTokenExpiry, DeviceCode, DeviceName, RefreshTokenExpiry) 
                values (@UserId, @AccessToken, @RefreshToken, @AccessTokenExpiry, @DeviceCode, @DeviceName, @RefreshTokenExpiry)";

            return db.Execute(query, loginToken);
        }
    }
}
