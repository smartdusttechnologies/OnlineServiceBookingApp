using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServcieBooking.Business.Repository.Interface;
using ServiceBooking.Business.Models;
using ServiceBooking.Business.Repository.Interface;

namespace ServcieBooking.Business.Features.Resturant
{
    public static class GetResturant
    {
        public class Command : IRequest<ResturantModel>
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
        public class Handler : IRequestHandler<Command, ResturantModel>
        {
            private readonly IResturantRepository _returant;

            public Handler(IResturantRepository resturant)
            {
                _returant = resturant;
            }

            public Task<ResturantModel> Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_returant.Get());
            }
        }
    }
    
}
