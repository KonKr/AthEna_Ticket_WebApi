using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthEna_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthEna_WebApi.Controllers
{
    [Produces("application/json")]
    
    public class DefaultController : Controller
    {
        [Route("api/GetContactKonKri")]
        [HttpGet]
        public IActionResult Get()
        {
            var db = new AthEnaDBContext();
            var res = db.Contacts.ToList();
            return Ok(res);
        }
        
    }
}
