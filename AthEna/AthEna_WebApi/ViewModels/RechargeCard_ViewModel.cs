using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.ViewModels
{
    public class RechargeCard_ViewModel
    {
        [Required]
        public Guid? CardId { get; set; }

        [Range(1,int.MaxValue)]
        public int ChargeEuros { get; set; }
    }
}
