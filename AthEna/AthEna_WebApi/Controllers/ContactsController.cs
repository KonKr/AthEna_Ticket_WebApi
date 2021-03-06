﻿using System;
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

    public class ContactsController : Controller
    {
        private ContactsRepository ContactsRepo;
        private IConfiguration _config;

        public ContactsController(IConfiguration configuration)
        {
            ContactsRepo = new ContactsRepository();
            _config = configuration;
        }


        [Route("api/ContactWithCard/{contact_IdentityCardNum?}")] //get info about a specific contact or all...
        [HttpGet]
        public IActionResult GetContact_WithCard(string contact_IdentityCardNum)
        {
            try
            {
                if(contact_IdentityCardNum == null)//If the Identity Card Number is not specified...
                    return Ok(ContactsRepo.GetAllContactsWithCards()); //Return all contacts...
                return Ok(ContactsRepo.GetContactWithCard(contact_IdentityCardNum)); //Return specific contact...
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
            
        }


        [Route("api/ContactWithCard")] //Create a new Contact and its associated card...
        [HttpPost]
        public IActionResult CreateNewContact_WithCard([FromBody]ContactWithCard_ViewModel newContactWithCard)
        {
            try
            {
                if (ModelState.IsValid)//checking model state
                {
                    var contactWithCard_CreationResult = ContactsRepo.CreateNewContactWithCard(newContactWithCard);
                    if (contactWithCard_CreationResult.GetType() == typeof(ContactWithCard_Created_ViewModel) )
                        return Ok(contactWithCard_CreationResult); //if the creation is successful return the id of the new contact as well as the id of the new card...
                    return BadRequest(); //if not... return bad request...
                }
                return BadRequest(ModelState); //if model state is not valid
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }      
        }    


    }
}
