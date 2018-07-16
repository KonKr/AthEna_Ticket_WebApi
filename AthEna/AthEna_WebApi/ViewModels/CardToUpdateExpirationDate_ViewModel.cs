using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.ViewModels
{
    public class CardToUpdateExpirationDate_ViewModel
    {
        public int Id { get; set; }
        public Guid CardId { get; set; }
        public DateTime LastRechargedOn { get; set; }
        public DateTime ChargeExpiresOn { get; set; }
    }
}
