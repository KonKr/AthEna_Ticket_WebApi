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

    public class ContactsController : Controller
    {
        private ContactsRepository ContactsRepo;
        private IConfiguration _config;
        public ContactsController(IConfiguration configuration)
        {
            ContactsRepo = new ContactsRepository();
            _config = configuration;
        }


        [Route("api/Contact/{contactGuid?}")] //get info about a specific contact or all...
        [HttpGet]
        public IActionResult GetContacts(Guid contactGuid)
        {
            try
            {
                if(contactGuid == Guid.Empty)
                    return Ok(ContactsRepo.GetAllContacts()); //to return all contacts
                return Ok(ContactsRepo.GetContact(contactGuid));
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
                    var contactWithCard_CreationResult = ContactsRepo.CreateNewContact_WithCard(newContactWithCard);
                    if (contactWithCard_CreationResult.GetType() == typeof(KeyValuePair) )
                        return Ok(contactWithCard_CreationResult); //if the creation is successful return the id of the new contact...
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
