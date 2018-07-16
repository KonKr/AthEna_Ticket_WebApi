using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthEna_WebApi.Models;
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


        [Route("api/Card/{cardId?}")]//get a card or all cards
        [HttpGet]
        public IActionResult GetCards(Guid cardId)
        {
            try
            {
                if (cardId == Guid.Empty)// if cardId is not set...
                    return Ok(CardsRepo.GetAllCards()); //retrieve all cards...
                return Ok(CardsRepo.GetCard(cardId)); //else get specified card
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }

        [Route("api/Card/Recharge")]
        [HttpPost]
        public IActionResult RechargeCard([FromBody]RechargeCard_ViewModel rechargeCardInfo)
        {
            try
            {
                if (ModelState.IsValid)//checking model state
                {
                    var cardRechargeResult = CardsRepo.RechargeCard(rechargeCardInfo);
                    if (cardRechargeResult.GetType() == typeof(CardChargeExpirationDate_ViewModel))
                        return Ok(cardRechargeResult); //if the creation is successful return the new Expiration date 
                    return BadRequest(); //if not... return bad request...
                }
                return BadRequest(ModelState); //if model state is not valid
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }

        [Route("api/Card/Validate/Bus")]
        [HttpPost]
        public IActionResult ValidateCard([FromBody]ValidateCard_Bus_ViewModel validationInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var validationResult = CardsRepo.ValidateOnBus(validationInfo);

                    //if the returned value is datetime, then it represents the expiration date of the ATH.ENA card...
                    if (validationResult.GetType() == typeof(DateTime))
                        return Ok(_config["TicketValidationResult:SuccessfulValidation"] + validationResult);

                    //if the returned value is bool, and more specific "false" then the ticket has expired.
                    if (validationResult.GetType() == typeof(bool))
                        return BadRequest(_config["TicketValidationResult:FailedValidation_dueTo_ExpiredSubscription"]);

                    //if none of the above, return a general error
                    return BadRequest(_config["TicketValidationResult:FailedValidation_dueTo_InternalServerError"]);
                }
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }


        //[Route("api/Card")] //create a card...
        //[HttpPost]
        //public IActionResult CreateNewCard([FromBody] Card newCard)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var cardCreationResult = CardsRepo.CreateNewCard(newCard);
        //            if (cardCreationResult.GetType() == typeof(Guid))
        //                return Ok((Guid)cardCreationResult); //if the creation is successful return the id of the new card...
        //            return BadRequest(); //if not... return bad request...
        //        }
        //        return BadRequest(ModelState); //if wrong input...
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, _config["StatusCodesText:ServerErr"]);
        //    }
        //}



    }
}