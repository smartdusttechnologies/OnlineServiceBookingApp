using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model.Orders;
using ServiceBooking.Buisness.Repository.Interfaces;

namespace ServcieBooking.Business.Features.Orders.Queries
{
    public static class GetOrders
    {
        public class Command : IRequest<OrdersModel>
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
        public class Handler : IRequestHandler<Command, OrdersModel>
        {
            private readonly IOrderRepository _orders;

            public Handler(IOrderRepository orders)
            {
                _orders = orders;
            }

            Task<OrdersModel> IRequestHandler<Command, OrdersModel>.Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_orders.OrdersList());
            }
        }
    }

}
