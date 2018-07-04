using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthEna_WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Debugging")]
    public class DebuggingController : Controller
    {
        [Route("test")]
        [HttpPost]
        public IActionResult Test()
        {
            return Ok("asdf");
        }
    }
}