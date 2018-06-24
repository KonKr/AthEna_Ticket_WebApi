using System;
using System.Collections.Generic;

namespace AthEna_WebApi.Models
{
    public partial class Contacts
    {
        public Contacts()
        {
            Cards = new HashSet<Cards>();
        }

        public int Id { get; set; }
        public Guid ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdCardNum { get; set; }
        public int SocialSecurityNum { get; set; }

        public ICollection<Cards> Cards { get; set; }
    }
}
