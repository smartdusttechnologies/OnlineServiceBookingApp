using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Business.Common;

namespace ServiceBooking.Buisness.Features.SecurityParamters.Queries
{
    public static class ValidatePasswordPolicy
    {
        public class Command : IRequest<RequestResult<bool>>
        {
            public string Password { get; set; }
            public int OrgId { get; set; }
            public Command( int orgId,string password)
            {
                this.Password = password;
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
        public class Handler : IRequestHandler<Command, RequestResult<bool>>
        {
           private readonly IMediator _mediator;

            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public Task<RequestResult<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                List<ValidationMessage> validationMessages = new List<ValidationMessage>();
                try
                {
                    var passwordPolicy = _mediator.Send(new GetSecurityParameter.Command(request.OrgId)).Result;
                    var validatePasswordResult = ValidatePassword(request.Password, passwordPolicy);
                    return Task.FromResult(validatePasswordResult);
                }
                catch (Exception ex)
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Validation failed!", Severity = ValidationSeverity.Error });
                    return Task.FromResult(new RequestResult<bool>(false, validationMessages));

                }
            }

            private RequestResult<bool> ValidatePassword(string password,SecurityParameter securityParameter)
            {
                List<ValidationMessage> validationMessages = new List<ValidationMessage>();

                if (password.Length < securityParameter.MinLength)
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Minimum length of the password should be " + securityParameter.MinLength + "characters long", Severity = ValidationSeverity.Error });
                    return new RequestResult<bool>(false, validationMessages); ;
                }
                if (!Helpers.ValidateMinimumSmallChars(password, securityParameter.MinSmallChars))
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Minimum number of small characters the password should have is " + securityParameter.MinSmallChars, Severity = ValidationSeverity.Error });
                    return new RequestResult<bool>(false, validationMessages); ;
                }

                if (!Helpers.ValidateMinimumCapsChars(password, securityParameter.MinCaps))
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Minimum number of capital characters the password should have is " + securityParameter.MinCaps, Severity = ValidationSeverity.Error });
                    return new RequestResult<bool>(false, validationMessages); ;
                }

                if (!Helpers.ValidateMinimumDigits(password, securityParameter.MinNumber))
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Minimum number of numeric characters the password should have is " + securityParameter.MinNumber, Severity = ValidationSeverity.Error });
                    return new RequestResult<bool>(false, validationMessages); ;
                }

                if (!Helpers.ValidateMinimumSpecialChars(password, securityParameter.MinSpecialChars))
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Minimum number of special characters the password should have is " + securityParameter.MinSpecialChars, Severity = ValidationSeverity.Error });
                    return new RequestResult<bool>(false, validationMessages); ;
                }

                if (!Helpers.ValidateDisallowedChars(password, securityParameter.DisAllowedChars))
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Characters which are not allowed in password are " + securityParameter.DisAllowedChars, Severity = ValidationSeverity.Error });
                    return new RequestResult<bool>(false, validationMessages); ;
                }
                return new RequestResult<bool>(true);
            }
        }
    }
}
