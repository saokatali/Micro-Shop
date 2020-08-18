using Catalog.API.Application.Messages.Commands.Category;
using Catalog.API.Application.Messages.Queries.Category;
using Catalog.API.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new All.Query()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await mediator.Send(new ById.Query { Id=id }));
        }

       

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create(CategoryDto category)
        {

           await mediator.Send(new Create.Command { Category = category });
           return Created();

        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(Guid id, CategoryDto category)
        {

            await mediator.Send(new Update.Command {Id=id, Category = category });
            return Ok();

        }
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {

            await mediator.Send(new Delete.Command { Id = id});
            return Ok();

        }




    }
}