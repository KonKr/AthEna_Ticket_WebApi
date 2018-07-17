using AthEna_WebApi.Models;
using AthEna_WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AthEna_WebApi.Repositories
{
    public class ContactsRepository : InitialRepository
    {       
        public List<Contact> GetAllContacts_WithCards()
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

        public Contact GetContact_WithCard(Guid contactGuid)
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

        public dynamic CreateNewContact_WithCard(ContactWithCard_ViewModel newContactWithCard)
        {
            try
            {
                //attempt to create the contact first...
                var contactToAdd = new Contact()//attempt to create a new object...
                {
                    ContactId = Guid.NewGuid(),
                    FirstName = newContactWithCard.FirstName,
                    IdCardNum = newContactWithCard.IdCardNum,
                    LastName = newContactWithCard.LastName,
                    SocialSecurityNum = newContactWithCard.SocialSecurityNum                    
                };
                db.Add(contactToAdd);

                //attempt to create the associated card...
                var cardToAdd = new Card()//attempt to create a new object based on the creation of the contact first...
                {
                    CardId = new Guid(),
                    ContactId = contactToAdd.ContactId,
                    RegisteredOn = DateTime.Now,
                    LastRechargedOn = DateTime.Now,
                    ChargeExpiresOn = DateTime.Now,
                };
                db.Add(cardToAdd);


                var savingResult = db.SaveChanges();
                if(savingResult!=0)//check if an error has occured...
                    return new ContactWithCard_Created_ViewModel() { NewContactId = contactToAdd.ContactId, NewCardId = cardToAdd.CardId };
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }      

    }
}
