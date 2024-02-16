using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ServcieBooking.Business.Interface;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Buisness.Repository.Interfaces;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces;

namespace ServiceBooking.Buisness.Features.Coupon.Commands
{
    public static class ApplyCoupon
    {
        public class Command : IRequest<RequestResult<bool>>
        {
            public string Coupon { get; set; }
            public Command(string coupon)
            {
                Coupon = coupon;
            }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Coupon).NotNull().MinimumLength(3).WithMessage("Coupon Code Cannot be Null!");
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
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;

            public Handler(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            Task<RequestResult<bool>> IRequestHandler<Command, RequestResult<bool>>.Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_orderRepository.ApplyCoupon(request.Coupon));
            }
        }
    }
}
