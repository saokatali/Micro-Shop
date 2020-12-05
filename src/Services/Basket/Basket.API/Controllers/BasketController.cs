using Basket.API.Application.Messages.Commands;
using Basket.API.Application.Messages.Queries;
using Basket.API.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator mediator;

        public BasketController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Post(BasketItem item)
        {
            return await mediator.Send(new Add.Command { Item = item });

        }

        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(Guid productId)
        {
            return await mediator.Send(new Remove.Command { ProductId = productId });

        }

        [HttpDelete]
        public async Task<ActionResult<Domain.Basket>> Get()
        {
            return await mediator.Send(new Get.Query());

        }




    }
}
