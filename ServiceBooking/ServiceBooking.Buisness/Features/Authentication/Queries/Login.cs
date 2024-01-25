using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Buisness.Repository.Interfaces;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;
using ServiceBooking.Business.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceBooking.Buisness.Features.SecurityParamters.Queries
{
    public static class Login
    {
        public class Command : IRequest<RequestResult<LoginToken>>
        {
            public LoginRequest LoginRequest { get; set; }
            public Command(LoginRequest req)
            {
                this.LoginRequest = req;
            }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                //RuleFor(x => x.Email).EmailAddress().NotNull().MinimumLength(3).WithMessage("Name Cannot be Null");
            }
        }
        public class Authorization : IAuthorizationRule<Command>
        {

            public Task Authorize(Command request, CancellationToken cancellationToken, IHttpContextAccessor contex)
            {
                //Check If This Rquest Is Accessable To User Or Not
                var user = new { UserId = 10, UserName = "Rajgupta" };
                var userClaim = new { UserId = 10, ClaimType = "application", Claim = "CreateUiPageType" };
                if (userClaim.Claim == "CreateUiPageType" && user.UserId == userClaim.UserId)
                {
                    return Task.CompletedTask;
                }
                return Task.FromException(new UnauthorizedAccessException("You are Unauthorized"));
            }
        }
        public class Logger : ILoggerRule<Command>
        {
            public Task Authorize(Command request, CancellationToken cancellationToken, IHttpContextAccessor contex)
            {
                //Check If This Rquest Is Accessable To User Or Not
                var user = new { UserId = 10, UserName = "Rajgupta" };
                var userClaim = new { UserId = 10, ClaimType = "application", Claim = "CreateUiPageType" };
                if (userClaim.Claim == "CreateUiPageType" && user.UserId == userClaim.UserId)
                {
                    return Task.CompletedTask;
                }
                return Task.FromException(new UnauthorizedAccessException("You are Unauthorized"));
            }
        }
        public class Handler : IRequestHandler<Command, RequestResult<LoginToken>>
        {
            private readonly IAuthenticationRepository _authenticationRepository;
            private readonly IUserRepository _userRepository;
            private readonly ILoggerRepository _loggerRepository;
            private readonly IConfiguration _configuration;
            private readonly IRoleRepository _roleRepository;
            public Handler(IAuthenticationRepository authenticationRepository, IUserRepository userRepository, ILoggerRepository loggerRepository, IConfiguration configuration, IRoleRepository roleRepository)
            {
                _authenticationRepository = authenticationRepository;
                _userRepository = userRepository;
                _loggerRepository = loggerRepository;
                _configuration = configuration;
                _roleRepository = roleRepository;
            }
            Task<RequestResult<LoginToken>> IRequestHandler<Command, RequestResult<LoginToken>>.Handle(Command request, CancellationToken cancellationToken)
            {
                List<ValidationMessage> validationMessages = new List<ValidationMessage>();
                try
                {
                    LoginToken token = new LoginToken();
                    var passwordLogin = _authenticationRepository.GetLoginPassword(request.LoginRequest.UserName) ?? new PasswordLogin() { UserId = 1,RoleId = 1};
                    string valueHash = string.Empty;
                    //if (passwordLogin != null && !Hasher.ValidateHash(request.LoginRequest.Password, passwordLogin.PasswordSalt, passwordLogin.PasswordHash, out valueHash))
                    //{
                    //    validationMessages.Add(new ValidationMessage { Reason = "UserName or password mismatch.", Severity = ValidationSeverity.Error });
                    //    return Task.FromResult(new RequestResult<LoginToken>(validationMessages));
                    //}
                    //var user = _userRepository.Get(passwordLogin.UserId);
                    //if (user == null)
                    //{
                    //    validationMessages.Add(new ValidationMessage { Reason = "UserName or password mismatch.", Severity = ValidationSeverity.Error });
                    //    return Task.FromResult(new RequestResult<LoginToken>(validationMessages));
                    //}
                    //if (!user.IsActive || user.Locked)
                    //{
                    //    validationMessages.Add(new ValidationMessage { Reason = "Access denied.", Severity = ValidationSeverity.Error });
                    //    return  Task.FromResult(new RequestResult<LoginToken>(validationMessages));
                    //}
                    #region this needs to be implemented once we have change password UI.
                    //int changeIntervalDays = 30;
                    //if (user.OrgId != 0)
                    //{
                    //    var passwordPolicy = _securityParameterService.Get(user.OrgId);
                    //    changeIntervalDays = passwordPolicy.ChangeIntervalDays;
                    //}
                    //if(passwordLogin.ChangeDate.AddDays(changeIntervalDays) < DateTime.Today)
                    //{
                    //    validationMessages.Add(new ValidationMessage { Reason = "Password expired.", Severity = ValidationSeverity.Error });
                    //    return new RequestResult<LoginToken>(validationMessages);
                    //}
                    #endregion

                    request.LoginRequest.Id = passwordLogin.UserId;
                    token = GenerateTokens(request.LoginRequest.UserName);
                    token.RoleId = passwordLogin.RoleId;

                    //TODO: this should be a async operation and can be made more cross-cutting design feature rather than calling inside the actual feature.
                    request.LoginRequest.LoginDate = DateTime.Now;
                    request.LoginRequest.PasswordHash = valueHash;
                    //_loggerRepository.LoginLog(request.LoginRequest);

                    return Task.FromResult(new RequestResult<LoginToken>(token, validationMessages));
                }
                catch (Exception ex)
                {
                    //_logger.LogException(new ExceptionLog
                    // {
                    //   ExceptionDate = DateTime.Now,
                    //   ExceptionMsg = ex.Message,
                    //  ExceptionSource = ex.Source,
                    //   ExceptionType = "UserService",
                    //  FullException = ex.StackTrace
                    // });
                    validationMessages.Add(new ValidationMessage { Reason = ex.Message, Severity = ValidationSeverity.Error, Description = ex.StackTrace });
                    return Task.FromResult(new RequestResult<LoginToken>(validationMessages));
                }
            }
            /// <summary>
            /// Method to Generate Token
            /// </summary>
            private LoginToken GenerateTokens(string userName)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                DateTime now = DateTime.Now;
                var claims = GetTokenClaims(userName, now);

                var accessJwt = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: claims,
                    notBefore: now,
                    expires: now.AddDays(1),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                );

                var encodedAccessJwt = new JwtSecurityTokenHandler().WriteToken(accessJwt);

                var refreshJwt = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: claims,
                    notBefore: now,
                    expires: now.AddDays(30),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                );
                var encodedRefreshJwt = new JwtSecurityTokenHandler().WriteToken(refreshJwt);

                var loginToken = new LoginToken
                {
                    UserName = userName,
                    AccessToken = encodedAccessJwt,
                    AccessTokenExpiry = DateTime.Now.AddDays(1),
                    RefreshToken = encodedRefreshJwt,
                    RefreshTokenExpiry = DateTime.Now.AddDays(30),
                };
                //_authenticationRepository.SaveLoginToken(loginToken);
                //TODO: this should be a async operation and can be made more cross-cutting design feature rather than calling inside the actual feature.
                //_loggerRepository.LoginTokenLog(loginToken);
                return loginToken;
            }
            /// <summary>
            ///Method to Get Token Cliams
            /// </summary>
            private List<Claim> GetTokenClaims(string sub, DateTime dateTime)
            {
                // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
                // You can add other claims here, if you want:

                //var userModel = _roleRepository.GetUserByUserName(sub);
                //var roleClaims = roleByOrganizationWithClaims.Select(x => new Claim(ClaimTypes.Role, x.RoleName));
                //var userRoleClaim = roleByOrganizationWithClaims.Select(x => new Claim(CustomClaimTypes.Permission, x.ClaimName));

                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, sub),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, Helpers.ToUnixEpochDate(dateTime).ToString(), ClaimValueTypes.Integer64)
            };
                //.Union(roleClaims).Union(userRoleClaim).ToList(); 

                //var roles = _roleRepository.GetRoleWithOrg(sub);
                //foreach (var role in roles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, role.Item2));
                //}

                //claims.Add(new Claim(CustomClaimType.UserId.ToString(), userModel.Id.ToString()));
                claims.Add(new Claim(CustomClaimType.UserId.ToString(),"0"));

                if (sub.ToLower() == "sysadmin")
                    claims.Add(new Claim(CustomClaimType.OrganizationId.ToString(), "0"));
                //else
                    
                    //claims.Add(new Claim(CustomClaimType.OrganizationId.ToString(), userModel.OrgId.ToString()));

                return claims;
            }

        }
    }
}
