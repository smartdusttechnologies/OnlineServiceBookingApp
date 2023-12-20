using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServcieBooking.Buisness.Features.Resturant;
using ServiceBooking.Buisness.Models;

namespace ServiceBooking.Web.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResturantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResturantController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Route("Get")]
        [HttpGet]
        public ResturantModel Get()
        {
            var resp = _mediator.Send(new GetResturant.Command());
        var s = resp.Result;
            return resp.Result;
        }
        [Route("GetById")]
        [HttpGet]
        public ResturantDetailModel Get(string resturantId)
        {
            return _mediator.Send(new GetByIdResturant.Command().resturantId = resturantId);
        }
    }
}
