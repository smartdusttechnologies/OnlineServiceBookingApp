using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Buisness.Repository.Interfaces;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;

namespace ServiceBooking.Buisness.Features.SecurityParamters.Queries
{
    public static class EmailExist
    {
        public class Command : IRequest<RequestResult<bool>>
        {
            public string Email { get; set; }
            public Command(string email)
            {
                this.Email = email;
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
        public class Handler : IRequestHandler<Command, RequestResult<bool>>
        {
            private readonly IAuthenticationRepository _authenticationRepository;
            private readonly IUserRepository _userRepository;
            public Handler(IAuthenticationRepository authenticationRepository, IUserRepository userRepository)
            {
                _authenticationRepository = authenticationRepository;
                _userRepository = userRepository;
            }

            public Task<RequestResult<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = _userRepository.EmailExist(request.Email);
                if (result != null)
                {
                    return Task.FromResult(new RequestResult<bool>(true));
                }
                var validationMessages = new List<ValidationMessage>() { new ValidationMessage { Reason = "User Already Exist!", Severity = ValidationSeverity.Error } };
                return Task.FromResult(new RequestResult<bool>(false, validationMessages));
            }
        }
    }
}
