using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServcieBooking.Business.Features.Orders.Queries;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Buisness.Features.Order.Commands;

namespace ServiceBooking.Web.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("NewOrder")]
        public IActionResult New(OrderModel order)
        {
            return Ok(_mediator.Send(new NewOrder.Command(order)));
        }
        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetOrders()
        {
            var resp = _mediator.Send(new GetOrders.Command()).Result;
            return Ok(resp);
        }
    }
}
