using Catalog.API.Core.Dto;
using Catalog.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
    public class CatalogController : ControllerBase
    {

        CatalogDbContext ctx;

        public CatalogController(CatalogDbContext ctx)
        {
            this.ctx = ctx;
         
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CatalogDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            
            
            return Ok(await ctx.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CatalogDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            await Task.CompletedTask;

            return Ok();
        }

    }
}
