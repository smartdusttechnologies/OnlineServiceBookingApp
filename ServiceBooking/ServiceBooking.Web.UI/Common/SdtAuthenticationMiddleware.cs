﻿
using Microsoft.IdentityModel.Tokens;
using ServiceBooking.Buisness.Core.Model.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ServuceBooking.Web.UI.Common
{
    public class SdtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        //TODO: need to pass the roleService instead of creting it local
        

        public SdtAuthenticationMiddleware(RequestDelegate requestDelegate, IConfiguration configuration)
        {
            _configuration = configuration;
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            JwtSecurityToken validatedToken;

            //TODO: token validation should also consider the logged out token not only expired tokens.
            if (token != null && ValidateToken(context, token, out validatedToken))
            {
                // attach user to context on successful jwt token validation
                var userIdentity = GetUserIdentity(validatedToken);
                if (userIdentity != null)
                {
                    
                    context.User = new SdtPrincipal(userIdentity);
                    await _next(context);
                    
                }

            }
            else if (context.Request.Path.Value.Equals("/api/Security/Index", StringComparison.OrdinalIgnoreCase)
                || context.Request.Path.Value.Equals("/api/Security/Login", StringComparison.OrdinalIgnoreCase)
                || context.Request.Path.Value.Equals("/api/Login", StringComparison.OrdinalIgnoreCase)
                || context.Request.Path.Value.Equals("/api/Security/RefreshToken", StringComparison.OrdinalIgnoreCase)
                || context.Request.Path.Value.Equals("/api/Security/RevokeToken", StringComparison.OrdinalIgnoreCase)
                || context.Request.Path.Value.StartsWith("/api/Security/SignUp", StringComparison.OrdinalIgnoreCase)
                || context.Request.Path.Value.StartsWith("/api/Swagger", StringComparison.OrdinalIgnoreCase)
                || context.Request.Path.Value.StartsWith("/api/Home/GetOrganizations", StringComparison.OrdinalIgnoreCase)
                || context.Request.Path.Value.StartsWith("/api/Home/ContactUs", StringComparison.OrdinalIgnoreCase)
                || !context.Request.Path.Value.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
            }
            return;
        }

        private SdtUserIdentity GetUserIdentity(JwtSecurityToken jwtSecurityToken)
        {
            var organisations = jwtSecurityToken.Claims.Select(x => x.Type == CustomClaimType.OrganizationId.ToString());
            SdtUserIdentity userIdentity = new SdtUserIdentity
            {
                UserName = jwtSecurityToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value,
                OrganizationId = int.Parse(jwtSecurityToken.Claims.Single(x => x.Type == CustomClaimType.OrganizationId.ToString()).Value),
                UserId = int.Parse(jwtSecurityToken.Claims.Single(x => x.Type == CustomClaimType.UserId.ToString()).Value),
            };
            //var roleByOrganizationWithClaims = _roleService.GetRoleByOrganizationWithClaims(userIdentity.UserName).Where(x => x.OrgId == userIdentity.OrganizationId);
            //var roleClaims = roleByOrganizationWithClaims.Select(x => new Claim(ClaimTypes.Role, x.RoleName)).Distinct().ToList();
            //var userRoleClaim = roleByOrganizationWithClaims.Select(x => new Claim(CustomClaimTypes.Permission, x.ClaimName)).Distinct().ToList();

            //var userByOrganizationWithClaims = _roleService.GetUserByOrganizationWithClaims(userIdentity.UserName).Where(x => x.OrgId == userIdentity.OrganizationId);
            //var userClaims = userByOrganizationWithClaims.Select(x => new Claim(CustomClaimTypes.Permission, x.ClaimName));

            //userIdentity.AddClaims(roleClaims);
            //userIdentity.AddClaims(userRoleClaim);
            //userIdentity.AddClaims(userClaims);

            return userIdentity;
        }

        private bool ValidateToken(HttpContext context, string token, out JwtSecurityToken validatedToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = _configuration["JWT:Secret"];
                var encodedKey = Encoding.ASCII.GetBytes(key);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(encodedKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero. So, tokens expire exactly at token expiration time.
                    ClockSkew = TimeSpan.Zero
                };
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken tokenAfterValidation);
                validatedToken = (JwtSecurityToken)tokenAfterValidation;
                return true;
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = 401;
                context.Response.WriteAsync("Token Expired.");
                validatedToken = null;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
