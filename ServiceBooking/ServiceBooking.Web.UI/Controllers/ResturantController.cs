using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServcieBooking.Business.Features.Resturant;
using ServiceBooking.Business.Models;
using ServiceBooking.Business.Models.Resturant;

namespace ServiceBooking.Web.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResturantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResturantController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Route("GetResturant")]
        [HttpGet]
        public ResturantModel GetResturant()
        {
            var resp = _mediator.Send(new GetResturant.Command());
        var s = resp.Result;
            return resp.Result;
        }
        [Route("GetResturantDetail/{resturantId}")]
        [HttpGet]
        public ResturantDetailModel GetResturantDetail([FromRoute]string resturantId)
        {
            var req = new GetByIdResturant.Command();
            req.resturantId = resturantId;
            var res =  _mediator.Send(req).Result;
            return res;
        }
    }
}
