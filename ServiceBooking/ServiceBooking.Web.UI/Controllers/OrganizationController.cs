using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using ServiceBooking.Buisness.Features.SecurityParamters.Queries;

namespace ServiceBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private readonly IMediator _mediator;
        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var result = _mediator.Send(new GetOrganizations.Command()).Result;
            if(result.IsSuccessful)
            {
                return Ok(result.RequestedObject);
            }
            return BadRequest(result.Message);
        }
       
    }
}
