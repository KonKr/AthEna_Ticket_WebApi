using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.ViewModels
{
    public class ValidateCard_Metro_ViewModel
    {
        [Required]
        public Guid? ValidatingCardId { get; set; }

        [Required]
        public Guid? ValidatingAtStationId { get; set; }

        [Required]
        public bool ValidationOnEmbarkation { get; set; }
    }
}
