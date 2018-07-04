using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.ViewModels
{
    public class ContactIncomingViewModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String IdCardNum { get; set; }
        public int SocialSecurityNum { get; set; }
    }
}
