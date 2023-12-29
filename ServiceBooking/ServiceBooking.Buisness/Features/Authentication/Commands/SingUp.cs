using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Buisness.Features.SecurityParamters.Queries;
using ServiceBooking.Buisness.Features.User.Commands;
using ServiceBooking.Buisness.Features.User.Queries;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;

namespace ServiceBooking.Buisness.Features.Authentication.Commands
{
    public static class SignUp
    {
        public class Command : IRequest<RequestResult<bool>>
        {
            public UserModel User { get; set; }
            public Command(UserModel user)
            {
                User = user;
            }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.User.UserName).NotNull().MinimumLength(3).WithMessage("Name Cannot be Null");
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
            private readonly IAuthenticationRepository _authenticationRepository;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(IAuthenticationRepository authenticationRepository, IMapper mapper, IMediator mediator)
            {
                _authenticationRepository = authenticationRepository;
                _mapper = mapper;
                _mediator = mediator;
            }

            Task<RequestResult<bool>> IRequestHandler<Command, RequestResult<bool>>.Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var validationResult = ValidateNewUserRegistration(request.User);
                    if (validationResult.IsSuccessful)
                    {
                        PasswordLogin passwordLogin = Hasher.HashPassword(request.User.Password);
                        _mediator.Send(new InsertUser.Command(request.User, passwordLogin));
                        return Task.FromResult(new RequestResult<bool>(true));
                    }
                    return Task.FromResult(new RequestResult<bool>(false, validationResult.Message));
                }
                catch (Exception ex)
                {

                    return Task.FromResult(new RequestResult<bool>(false));
                }
            }
            /// <summary>
            /// Method to Validate the New User Registation
            /// </summary>
            private RequestResult<bool> ValidateNewUserRegistration(UserModel user)
            {
                List<ValidationMessage> validationMessages = new List<ValidationMessage>();
                UserModel existingUser = _mediator.Send(new GetUser.Command(user.UserName)).Result;
                if (existingUser != null)
                {
                    var error = new ValidationMessage { Reason = "The UserName not available", Severity = ValidationSeverity.Error };
                    validationMessages.Add(error);
                    return new RequestResult<bool>(false, validationMessages);
                }
                if (user.Password != user.NewPassword)
                {
                    var error = new ValidationMessage { Reason = "Password Didn't Match", Severity = ValidationSeverity.Error };
                    validationMessages.Add(error);
                    return new RequestResult<bool>(false, validationMessages);
                }
                var validatePhoneResult = _mediator.Send(new ValidatePhoneNumber.Command(user.Mobile)).Result;
                if (!validatePhoneResult.IsSuccessful)
                {
                    return validatePhoneResult;
                }
                var validateEmailResult = _mediator.Send(new ValidateEmail.Command(user.Email)).Result;
                if (!validateEmailResult.IsSuccessful)
                {
                    return validateEmailResult;
                }
                var validatePasswordResult = _mediator.Send(new ValidatePasswordPolicy.Command(user.OrgId, user.Password)).Result;
                return validatePasswordResult;
            }
        }
    }
}
