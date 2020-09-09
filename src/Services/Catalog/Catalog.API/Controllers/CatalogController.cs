
using Catalog.API.Application.Messages.Commands.Catalog;
using Catalog.API.Application.Messages.Queries.Catalog;
using Catalog.API.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        private readonly IMediator mediator;

        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var data = await mediator.Send(new All.Query());
            return Ok(data);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await mediator.Send(new ById.Query { Id = id });

            return Ok(data);
        }


        [HttpGet("category/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCategory(Guid id)
        {
            return Ok(await mediator.Send(new ByCategory.Query { CategoryId = id }));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create(ProductDto product)
        {

            await mediator.Send(new Create.Command { Product = product });
            return Created();

        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(ProductDto product, Guid id)
        {

            await mediator.Send(new Update.Command { Product = product, Id=id });
            return Ok();

        }


        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {

            await mediator.Send(new Delete.Command { Id = id });
            return Ok();

        }

    }
}
