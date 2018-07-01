using AthEna_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.Repositories
{
    public class ContactsRepository : InitialRepository
    {
        public List<Contact> GetAllContacts()
        {
            try
            {
                var res = db.Contacts.ToList();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
