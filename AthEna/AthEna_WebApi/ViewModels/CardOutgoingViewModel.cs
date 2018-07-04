using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.ViewModels
{
    public class CardOutgoingViewModel
    {
        public Guid CardId { get; set; }
        public Guid ContactId { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime lastRecharedOn { get; set; }
        public DateTime ChargeExpiresOn { get; set; }
    }
}
