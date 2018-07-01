using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.ViewModels
{
    public class ContactViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdCardNum { get; set; }
        public int SocialSecurityNum { get; set; }
    }
}
