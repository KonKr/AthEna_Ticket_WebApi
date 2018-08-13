using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.ViewModels
{
    public class ContactWithCard_ViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string IdCardNum { get; set; }

        [Range(1, int.MaxValue)]
        public int SocialSecurityNum { get; set; }
                
    }
}
