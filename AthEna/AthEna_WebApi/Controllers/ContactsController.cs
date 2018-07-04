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


        [Route("api/Contact/{contactGuid?}")]
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

        //TODO: add to repository.
        [Route("api/CreateNewContact")]
        [HttpPost]
        public IActionResult CreateNewContact([FromBody]ContactIncomingViewModel newContact)
        {
            try
            {
                var newContactsGuid = ContactsRepo.CreateNewContact(newContact); //on creation, we get the new contact's guid...
                if (newContactsGuid != false)
                    return Ok(newContactsGuid);
                return BadRequest(_config["StatusCodesText:WrongInput"]);
            }
            catch (Exception e)
            {
                return StatusCode(500, _config["StatusCodesText:ServerErr"]);
            }
        }    

    }
}
