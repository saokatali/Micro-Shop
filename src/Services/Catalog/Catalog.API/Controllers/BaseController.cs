using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    public class BaseController: ControllerBase
    {
        [NonAction]
        public IActionResult Created()
        {
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
