using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {



        protected IMediator Mediator => (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        [NonAction]
        public IActionResult Created()
        {
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
