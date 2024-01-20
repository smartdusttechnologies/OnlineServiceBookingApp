using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;
using System.Text.RegularExpressions;

namespace ServiceBooking.Buisness.Features.SecurityParamters.Queries
{
    public static class ValidateEmail
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
            public Handler()
            {
            }
            Task<RequestResult<bool>> IRequestHandler<Command, RequestResult<bool>>.Handle(Command request, CancellationToken cancellationToken)
            {
                List<ValidationMessage> validationMessages = new List<ValidationMessage>();

                // Use a regular expression to validate the email format
                Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                if (!emailRegex.IsMatch(request.Email))
                {
                    validationMessages.Add(new ValidationMessage { Reason = "Invalid email format", Severity = ValidationSeverity.Error });
                    return Task.FromResult(new RequestResult<bool>(false, validationMessages));
                }

                // Perform any additional validation logic specific to email addresses, if needed

                return Task.FromResult(new RequestResult<bool>(true));
            }
        }
    }
}
