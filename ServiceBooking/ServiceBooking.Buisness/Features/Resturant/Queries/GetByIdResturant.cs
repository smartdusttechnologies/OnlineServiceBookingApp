using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model.Resturant;
using ServiceBooking.Buisness.Repository.Interfaces;

namespace ServcieBooking.Business.Features.Resturant
{
    public static class GetByIdResturant
    {
        public class Command : IRequest<ResturantDetailModel>
        {
            public string resturantId { get; set; }
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
        public class Handler : IRequestHandler<Command, ResturantDetailModel>
        {
            private readonly IResturantRepository _returant;

            public Handler(IResturantRepository resturant)
            {
                _returant = resturant;
            }

            Task<ResturantDetailModel> IRequestHandler<Command, ResturantDetailModel>.Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_returant.Get(request.resturantId));
            }
        }
    }

}
