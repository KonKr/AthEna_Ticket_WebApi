using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthEna_WebApi.Repositories;
using AthEna_WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AthEna_WebApi.Controllers
{
    [Produces("application/json")]
    public class CardsController : Controller
    {

        private CardsRepository CardsRepo;
        private IConfiguration _config;
        public CardsController(IConfiguration Configuration)
        {
            CardsRepo = new CardsRepository();
            _config = Configuration;
        }

        [Route("api/Card/{cardId?}")]
        [HttpGet]
        public IActionResult GetCards(Guid cardId)
        {
            try
            {
                if(cardId == Guid.Empty)
                    return Ok(CardsRepo.GetAllCards());
                return Ok(CardsRepo.GetCard(cardId));
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }


    }
}