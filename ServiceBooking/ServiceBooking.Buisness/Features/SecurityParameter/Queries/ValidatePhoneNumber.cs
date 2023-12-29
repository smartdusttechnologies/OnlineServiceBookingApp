using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;

namespace ServiceBooking.Buisness.Features.SecurityParamters.Queries
{
    public static class ValidatePhoneNumber
    {
        public class Command : IRequest<RequestResult<bool>>
        {
            public string PhoneNumber { get; set; }
            public Command(string phone)
            {
                this.PhoneNumber = phone;
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
            private readonly ISecurityParameterRepository _securityParameterRepository;

            public Handler(ISecurityParameterRepository securityParameterRepository, IMapper mapper)
            {
               _securityParameterRepository = securityParameterRepository;
            }

            Task<RequestResult<bool>> IRequestHandler<Command, RequestResult<bool>>.Handle(Command request, CancellationToken cancellationToken)
            {
                List<ValidationMessage> validationMessages = new List<ValidationMessage>();

                // Remove any non-digit characters from the phone number
                string sanitizedPhoneNumber = new string(request.PhoneNumber.Where(char.IsDigit).ToArray());

                if (sanitizedPhoneNumber.Length != 10)
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Phone number must be 10 digits long", Severity = ValidationSeverity.Error });
                    return Task.FromResult(new RequestResult<bool>(false, validationMessages));
                }

                // Perform any additional validation logic specific to phone numbers, if needed

                return Task.FromResult(new RequestResult<bool>(true));
            }
        }
    }
}
