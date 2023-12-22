//using AutoMapper;
//using FluentValidation;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using ServcieBooking.Business.Interface;
//using ServcieBooking.Business.Repository.Interface;

//namespace ServcieBooking.Business.Features.UiPageType
//{
//    public static class GetResturant
//    {
//        public class Command : IRequest<List<ServcieBooking.Business.Models.UiPageType>>
//        {
//        }
//        public class Authorization : IAuthorizationRule<Command>
//        {

//            public Task Authorize(Command request, CancellationToken cancellationToken, IHttpContextAccessor contex)
//            {
//                //Check If This Rquest Is Accessable To User Or Not
//                var user = new { UserId = 10, UserName = "Rajgupta" };
//                var userClaim = new { UserId = 10, ClaimType = "application", Claim = "GetUiPageType" };
//                if (userClaim.Claim == "GetUiPageTye" && user.UserId == userClaim.UserId)
//                {
//                    return Task.CompletedTask;
//                }
//                return Task.FromException(new UnauthorizedAccessException("You are Unauthorized"));
//            }
//        }
//        public class Handler : IRequestHandler<Command, List<ServcieBooking.Business.Models.UiPageType>>
//        {
//            private readonly IUiPageTypeRepository _uiPageTypeRepository;
//            private readonly IMapper _mapper;

//            public Handler(IUiPageTypeRepository uiPageTypeRepository, IMapper mapper)
//            {
//                _uiPageTypeRepository = uiPageTypeRepository;
//                _mapper = mapper;
//            }

//            Task<List<Models.UiPageType>> IRequestHandler<Command, List<Models.UiPageType>>.Handle(Command request, CancellationToken cancellationToken)
//            {
//                return Task.FromResult(_uiPageTypeRepository.Get());
//            }
//        }
//    }
    
//}
