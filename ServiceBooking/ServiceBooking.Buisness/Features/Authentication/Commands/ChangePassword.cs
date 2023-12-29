using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Buisness.Features.SecurityParamters.Queries;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;

namespace ServiceBooking.Buisness.Features.Authentication.Commands
{
    public static class ChangePassword
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
            private readonly IUserRepository _userRepository;

            public Handler(IAuthenticationRepository authenticationRepository, IMapper mapper, IMediator mediator, IUserRepository userRepository)
            {
                _authenticationRepository = authenticationRepository;
                _mapper = mapper;
                _mediator = mediator;
                _userRepository = userRepository;
            }

            Task<RequestResult<bool>> IRequestHandler<Command, RequestResult<bool>>.Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var passworsResult =  _mediator.Send(new ChangePasswordPolicy.Command(request.ChangePassword)).Result;
                    if (passworsResult.IsSuccessful)
                    {
                        var validationResult = _mediator.Send(new ValidatePasswordPolicy.Command(0, request.ChangePassword.NewPassword)).Result;

                        var passwordLogin = _authenticationRepository.GetLoginPassword(request.ChangePassword.Username);
                        List<ValidationMessage> validationMessages = new List<ValidationMessage>();
                        string valueHash = string.Empty;
                        if (request.ChangePassword != null && !Hasher.ValidateHash(request.ChangePassword.OldPassword, passwordLogin.PasswordSalt, passwordLogin.PasswordHash, out valueHash))
                        {
                            validationMessages.Add(new ValidationMessage { Reason = "Old password is incorrect.", Severity = ValidationSeverity.Error, SourceId = "OldPassword" });
                            return Task.FromResult(new RequestResult<bool>(validationMessages));
                        }
                        if (request.ChangePassword.ConfirmPassword != request.ChangePassword.NewPassword)
                        {
                            validationMessages.Add(new ValidationMessage { Reason = "New Password Didn't Match.", Severity = ValidationSeverity.Error, SourceId = "OldPassword" });
                            return Task.FromResult(new RequestResult<bool>(validationMessages));
                        }
                        if (validationResult.IsSuccessful)
                        {
                            if (passworsResult.IsSuccessful)
                            {
                                PasswordLogin newPasswordLogin = Hasher.HashPassword(request.ChangePassword.NewPassword);
                                ChangePasswordModel passwordModel = new ChangePasswordModel();
                                passwordModel.PasswordHash = newPasswordLogin.PasswordHash;
                                passwordModel.UserId = request.ChangePassword.UserId;
                                passwordModel.PasswordSalt = newPasswordLogin.PasswordSalt;
                                _userRepository.Update(passwordModel);
                                return Task.FromResult(new RequestResult<bool>(true));
                            }
                        }
                        return Task.FromResult(new RequestResult<bool>(false, validationResult.Message));
                    }
                    return Task.FromResult(new RequestResult<bool>(false, passworsResult.Message));
                }
                catch (Exception ex)
                {
                    return Task.FromResult(new RequestResult<bool>(false));
                }

            }
        }
    }
}
