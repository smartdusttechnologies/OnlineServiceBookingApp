using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Repository.Interfaces;

namespace ServcieBooking.Business.Features.Resturant
{
    public static class GetConfiguration
    {
        public class Command : IRequest<ConfigurationModel>
        {
        }
        public class Authorization : IAuthorizationRule<Command>
        {

            public Task Authorize(Command request, CancellationToken cancellationToken, IHttpContextAccessor contex)
            {
                //Check If This Rquest Is Accessable To User Or Not
                var user = new { UserId = 10, UserName = "Rajgupta" };
                var userClaim = new { UserId = 10, ClaimType = "application", Claim = "GetUiPageType" };
                if (userClaim.Claim == "GetUiPageType" && user.UserId == userClaim.UserId)
                {
                    return Task.CompletedTask;
                }
                return Task.FromException(new UnauthorizedAccessException("You are Unauthorized"));
            }
        }
        public class Handler : IRequestHandler<Command, ConfigurationModel>
        {
            private readonly ISystemRepository _system;

            public Handler(ISystemRepository system)
            {
                _system = system;
            }

            public Task<ConfigurationModel> Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_system.Get());
            }
        }
    }
    
}
