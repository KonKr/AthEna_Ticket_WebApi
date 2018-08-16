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
                    if(!(rechargeCardInfo.ChargeEuros == 30 || rechargeCardInfo.ChargeEuros == 60 || rechargeCardInfo.ChargeEuros == 200))
                        return BadRequest(_config["StatusCodesText:RechargeCardErr"]); //if not... return bad request...
                    if (cardRechargeResult.GetType() == typeof(bool))
                        return BadRequest(_config["StatusCodesText:RechargeCardErr2"]);
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
        public IActionResult ValidateCard_Bus([FromBody]ValidateCard_Bus_ViewModel validationInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //check the validity of the card...
                    var cardValidity = CardsRepo.CheckCardsValidity(validationInfo.ValidatingCardId);
                    if (!cardValidity.Validity)
                        return BadRequest(_config["TicketValidationResult:FailedValidation_dueTo_ExpiredSubscription"]);

                    //if card is valid, create entry in validation activity...
                    if (CardsRepo.ValidateCard_OnBus(validationInfo))//if validation is successful... display success message with card's expiration date...
                        return Ok(_config["TicketValidationResult:SuccessfulValidation"] + cardValidity.ExpirationDate);

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


        [Route("api/Card/Validate/Metro")]
        [HttpPost]
        public IActionResult ValidateCard_Metro([FromBody]ValidateCard_Metro_ViewModel validationInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!validationInfo.ValidationOnEmbarkation.Value)//if intent is to disembark...
                    {
                        //check if user has embarked within 5 hours ago...
                        if(!CardsRepo.HasEmbarkedBefore_Metro(validationInfo.ValidatingCardId))
                            return BadRequest(_config["TicketValidationResult:FailedValidation_dueTo_InternalServerError"]);
                    }                    

                    //check the validity of the card...
                    var cardValidity = CardsRepo.CheckCardsValidity(validationInfo.ValidatingCardId);
                    if (!cardValidity.Validity)
                        return BadRequest(_config["TicketValidationResult:FailedValidation_dueTo_ExpiredSubscription"]);

                    //if card is valid, create entry in validation activity...
                    if (CardsRepo.ValidateCard_OnMetro(validationInfo))//if validation is successful... display success message with card's expiration date...
                        return Ok(_config["TicketValidationResult:SuccessfulValidation"] + cardValidity.ExpirationDate);

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

    }
}