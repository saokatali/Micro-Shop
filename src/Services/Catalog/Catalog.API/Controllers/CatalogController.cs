using Catalog.API.Core.Dto;
using Catalog.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Catalog.API.Application.Messages.Commands.Catalog;
using Catalog.API.Application.Messages.Queries.Catalog;

namespace Catalog.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator mediator;

        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CatalogDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var data = await mediator.Send(new All.Query());
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CatalogDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await mediator.Send(new ById.Query { Id=id });

            return Ok(data);
        }

    }
}
