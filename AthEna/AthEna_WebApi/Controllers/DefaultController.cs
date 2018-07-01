using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthEna_WebApi.Models;
using AthEna_WebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthEna_WebApi.Controllers
{
    [Produces("application/json")]

    public class DefaultController : Controller
    {
        private ContactsRepository ContactsRepo;
        public DefaultController()
        {
            ContactsRepo = new ContactsRepository();
        }

        //TODO: add status codes responses to appsettings and use them from there
        //TODO: add connection string to appsettings.

        [Route("api/Contact/{contactGuid?}")]
        [HttpGet]
        public IActionResult Get(Guid contactGuid)
        {
            try
            {
                var res = ContactsRepo.GetAllContacts();
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(500, "Παρουσιάστηκε κάποιο εσωτερικό σφάλμα.");
            }
            
        }

        //TODO: add to repository.
        [Route("api/CreateNewContact")]
        [HttpGet]
        public IActionResult CreateNewContact()
        {
            var db = new AthEnaDBContext();
            var res = db.Contacts.ToList();
            var contactToAdd = new Contact
            {
                FirstName = "Kapoios",
                LastName = "Kapoiou",
                ContactId = Guid.NewGuid(),
                IdCardNum = "AE12314",
                SocialSecurityNum = 000123
            };
            db.Add(contactToAdd);
            db.SaveChanges();
            return Ok(contactToAdd.ContactId);
        }


    }
}
