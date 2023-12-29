using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;

namespace ServiceBooking.Buisness.Features.SecurityParamters.Queries
{
    public static class GetOrganizations
    {
        public class Command : IRequest<RequestResult<List<Organization>>>
        {
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
        public class Handler : IRequestHandler<Command, RequestResult<List<Organization>>>
        {
            private readonly IOrganizationRepository _organizationRepository;

            public Handler(IOrganizationRepository organizationRepository)
            {
                _organizationRepository = organizationRepository;
            }

            Task<RequestResult<List<Organization>>> IRequestHandler<Command, RequestResult<List<Organization>>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var organization = _organizationRepository.Get();
                if (organization == null)
                {
                    return Task.FromResult(new RequestResult<List<Organization>>());
                }
                return Task.FromResult(new RequestResult<List<Organization>>(organization));
            }
        }
    }
}
