using AthEna_WebApi.Models;
using AthEna_WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AthEna_WebApi.Repositories
{
    public class ContactsRepository : InitialRepository
    {
       
        public List<Contact> GetAllContacts()
        {
            try
            {
                var contactsList = db.Contacts.ToList();
                return contactsList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Contact GetContact(Guid contactGuid)
        {
            try
            {
                var contact = db.Contacts.Where(w => w.ContactId == contactGuid).FirstOrDefault();
                return contact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic CreateNewContact(Contact newContact)
        {
            try
            {   
                var contactToAdd = new Contact()//attempt to create a new object
                {
                    ContactId = Guid.NewGuid(),
                    FirstName = newContact.FirstName,
                    IdCardNum = newContact.IdCardNum,
                    LastName = newContact.LastName,
                    SocialSecurityNum = newContact.SocialSecurityNum
                };

                db.Add(contactToAdd);
                var savingResult = db.SaveChanges();

                if(savingResult!=0)//check if an error has occured
                    return contactToAdd.ContactId;
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
       

        

    }
}
