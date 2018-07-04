using AthEna_WebApi.Models;
using AthEna_WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AthEna_WebApi.Repositories
{
    public class ContactsRepository : InitialRepository
    {
       
        public List<ContactOutgoingViewModel> GetAllContacts()
        {
            try
            {
                var contactsList = db.Contacts.Select(s => new ContactOutgoingViewModel {
                                                            ContactId = s.ContactId,
                                                            FirstName = s.FirstName,
                                                            IdCardNum = s.IdCardNum,
                                                            LastName = s.LastName,
                                                            SocialSecurityNum = s.SocialSecurityNum
                                                        }).ToList();
                return contactsList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ContactOutgoingViewModel GetContact(Guid contactGuid)
        {
            try
            {
                var contact = db.Contacts.Where(w => w.ContactId == contactGuid).Select(s => new ContactOutgoingViewModel
                                                                                                {
                                                                                                    ContactId = s.ContactId,
                                                                                                    FirstName = s.FirstName,
                                                                                                    IdCardNum = s.IdCardNum,
                                                                                                    LastName = s.LastName,
                                                                                                    SocialSecurityNum = s.SocialSecurityNum
                                                                                                }).FirstOrDefault();
                return contact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic CreateNewContact(ContactIncomingViewModel newContact)
        {
            try
            {
                var contactToAdd = new Contact()
                {
                    ContactId = Guid.NewGuid(),
                    FirstName = newContact.FirstName,
                    IdCardNum = newContact.IdCardNum,
                    LastName = newContact.LastName,
                    SocialSecurityNum = newContact.SocialSecurityNum
                };

                db.Add(contactToAdd);
                db.SaveChanges();

                return contactToAdd.ContactId;                
            }
            catch (Exception e)
            {
                return false;
            }
        }

        

    }
}
