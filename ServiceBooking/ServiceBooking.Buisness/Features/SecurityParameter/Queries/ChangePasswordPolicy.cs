using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;

namespace ServiceBooking.Buisness.Features.SecurityParamters.Queries
{
    public static class ChangePasswordPolicy
    {
        public class Command : IRequest<RequestResult<bool>>
        {
            public ChangePasswordModel ChangePassword { get; set; }
            public Command(ChangePasswordModel changePassword)
            {
                this.ChangePassword = changePassword;
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
        public class Handler : IRequestHandler<Command, RequestResult<bool>>
        {
            private readonly ISecurityParameterRepository _securityParamterRepository;

            public Handler(ISecurityParameterRepository securityParamterRepository)
            {
                _securityParamterRepository = securityParamterRepository;
            }

            Task<RequestResult<bool>> IRequestHandler<Command, RequestResult<bool>>.Handle(Command request, CancellationToken cancellationToken)
            {
                List<ValidationMessage> validationMessages = new List<ValidationMessage>();

                if (string.IsNullOrEmpty(request.ChangePassword.OldPassword))
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Please enter Old Password", Severity = ValidationSeverity.Error, SourceId = "OldPassword" });
                    return Task.FromResult(new RequestResult<bool>(false, validationMessages));

                }
                else if (string.IsNullOrEmpty(request.ChangePassword.NewPassword))
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Please Enter New Password.", Severity = ValidationSeverity.Error, SourceId = "NewPassword" });
                    return Task.FromResult(new RequestResult<bool>(false, validationMessages)); ;
                }
                else if (string.IsNullOrEmpty(request.ChangePassword.ConfirmPassword))
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Please Enter Confirm Password.", Severity = ValidationSeverity.Error, SourceId = "ConfirmPassword" });
                    return Task.FromResult(new RequestResult<bool>(false, validationMessages)); ;
                }
                else if (request.ChangePassword.OldPassword == request.ChangePassword.NewPassword)
                {
                    validationMessages.Add(new ValidationMessage { Reason = "New password must be different from old password.", Severity = ValidationSeverity.Error, SourceId = "NewPassword" });
                    return Task.FromResult(new RequestResult<bool>(false, validationMessages)); ;

                }
                else if (request.ChangePassword.NewPassword != request.ChangePassword.ConfirmPassword)
                {
                    validationMessages.Add(new ValidationMessage { Reason = "New password and confirm password fields must match.", Severity = ValidationSeverity.Error, SourceId = "ConfirmPassword" });
                    return Task.FromResult(new RequestResult<bool>(false, validationMessages)); ;

                }
                return Task.FromResult(new RequestResult<bool>(true));
            }
        }
    }
}
