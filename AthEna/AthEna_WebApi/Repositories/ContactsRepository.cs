using AthEna_WebApi.Models;
using AthEna_WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AthEna_WebApi.Repositories
{
    public class ContactsRepository : InitialRepository
    {
        public List<ContactWithCard_Output_VM> GetAllContactsWithCards()
        {
            try
            {   
                var ContactsWithCards_List = db.Contacts
                                                   .Join(db.Cards,
                                                        contact => contact.ContactId,
                                                        card => card.ContactId,
                                                        (contact, card) => new ContactWithCard_Output_VM()
                                                        {
                                                            Contact_Guid = contact.ContactId,
                                                            Contact_FirstName = contact.FirstName,
                                                            Contact_LastName = contact.LastName,
                                                            Contact_IdCardNum = contact.IdCardNum,
                                                            Contact_SocialSecurityNum = contact.SocialSecurityNum,
                                                            Card_Guid = card.CardId,
                                                            Card_RegisteredOn = card.RegisteredOn,
                                                            Card_ChargeExpiresOn = card.ChargeExpiresOn
                                                        }
                                                    ).ToList();
                return ContactsWithCards_List;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ContactWithCard_Output_VM GetContactWithCard(String contact_IdentityCardNum)
        {
            try
            {               
                var ContactWithCard = db.Contacts
                                        .Where(w => w.IdCardNum == contact_IdentityCardNum)
                                        .Join(db.Cards,
                                            contact => contact.ContactId,
                                            card => card.ContactId,
                                            (contact, card) => new ContactWithCard_Output_VM()
                                            {
                                                Contact_Guid = contact.ContactId,
                                                Contact_FirstName = contact.FirstName,
                                                Contact_LastName = contact.LastName,
                                                Contact_IdCardNum = contact.IdCardNum,
                                                Contact_SocialSecurityNum = contact.SocialSecurityNum,
                                                Card_Guid = card.CardId,
                                                Card_RegisteredOn = card.RegisteredOn,
                                                Card_ChargeExpiresOn = card.ChargeExpiresOn
                                            }).FirstOrDefault();

                return ContactWithCard;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic CreateNewContactWithCard(ContactWithCard_ViewModel newContactWithCard)
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
                if (savingResult != 0)//check if an error has occured...
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
