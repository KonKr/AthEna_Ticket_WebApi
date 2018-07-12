using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthEna_WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AthEna_WebApi.Controllers
{
    [Produces("application/json")]
    
    public class AdminController : Controller
    {
        private IConfiguration _config;
        public AdminController(IConfiguration Configuration)
        {
            _config = Configuration;
        }

        [Route("api/Admin/Test")]
        [BasicAuthentication]
        public IActionResult Hello()
        {
            try
            {                
                return Ok("Hello!!");
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }

    }
}