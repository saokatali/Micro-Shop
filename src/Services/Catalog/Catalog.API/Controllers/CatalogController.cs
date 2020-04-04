using Catalog.API.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
    
namespace Catalog.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CatalogDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            await Task.CompletedTask;
            
            return Ok();
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
