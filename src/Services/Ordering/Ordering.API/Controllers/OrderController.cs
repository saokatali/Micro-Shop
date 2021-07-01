using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Application.Dtos;
using Ordering.API.Application.Messages.Commands.Orders;
using Ordering.API.Application.Messages.Queries;

namespace Ordering.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long orderId)
        {
            var order = await mediator.Send(new GetById.Query { orderId = orderId });
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderDto order)
        {
            var resonse = await mediator.Send(new Add.Command { Order = order });
           

            return Ok(resonse);
        }

    }
}
