using System;
using System.Collections.Generic;

namespace AthEna_WebApi.Models
{
    public partial class Contact
    {
        public Contact()
        {
            Cards = new HashSet<Card>();
        }

        public int Id { get; set; }
        public Guid ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdCardNum { get; set; }
        public int SocialSecurityNum { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
