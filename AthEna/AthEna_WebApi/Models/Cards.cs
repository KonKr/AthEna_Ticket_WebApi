using System;
using System.Collections.Generic;

namespace AthEna_WebApi.Models
{
    public partial class Cards
    {
        public Cards()
        {
            ValidationActivity = new HashSet<ValidationActivity>();
        }

        public int Id { get; set; }
        public Guid CardId { get; set; }
        public Guid ContactId { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime LastRechargedOn { get; set; }
        public DateTime ChargeExpiresOn { get; set; }

        public Contacts Contact { get; set; }
        public ICollection<ValidationActivity> ValidationActivity { get; set; }
    }
}
