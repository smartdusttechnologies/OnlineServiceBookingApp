using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Business.Data.Repository.Interfaces;

namespace ServiceBooking.Buisness.Features.User.Queries
{
    public static class Profile
    {
        public class Command : IRequest<UserModel>
        {
            public int Id { get; set; }
            public Command(int id)
            {
                this.Id = id;
            }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotNull().WithMessage("User Cannot be Null");
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
        public class Handler : IRequestHandler<Command, UserModel>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository, IMapper mapper)
            {
               _userRepository = userRepository;
            }

            Task<UserModel> IRequestHandler<Command, UserModel>.Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_userRepository.Get(request.Id));
            }
        }
    }
}
