using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServcieBooking.Business.Features.Resturant;
using ServiceBooking.Buisness.Core.Model;

namespace ServiceBooking.Web.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SystemController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Route("GetConfiguration")]
        [HttpGet]
        public ConfigurationModel GetConfiguration()
        {
            var resp = _mediator.Send(new GetConfiguration.Command());
            var s = resp.Result;
            return resp.Result;
        }
    }
}
