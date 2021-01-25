using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController: ControllerBase
    {



        protected  IMediator Mediator => (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        [NonAction]
        public IActionResult Created()
        {
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
