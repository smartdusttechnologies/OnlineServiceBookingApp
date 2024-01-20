using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;

namespace ServiceBooking.Buisness.Features.SecurityParamters.Queries
{
    public static class GetSecurityParameter
    {
        public class Command : IRequest<SecurityParameter>
        {
            public int OrgId { get; set; }
            public Command(int orgId)
            {
                OrgId = orgId;
            }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                // RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(3).WithMessage("Name Cannot be Null");
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
        public class Handler : IRequestHandler<Command, SecurityParameter>
        {
            private readonly ISecurityParameterRepository _securityParamterRepository;

            public Handler(ISecurityParameterRepository securityParamterRepository)
            {
                _securityParamterRepository = securityParamterRepository;
            }

            Task<SecurityParameter> IRequestHandler<Command, SecurityParameter>.Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_securityParamterRepository.Get(request.OrgId));
            }
        }
    }
}
