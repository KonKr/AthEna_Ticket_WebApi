using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.ViewModels
{
    public class ContactWithCard_Output_VM
    {
        public Guid Contact_Guid { get; set; }
        public String Contact_FirstName { get; set; }
        public String Contact_LastName { get; set; }
        public String Contact_IdCardNum { get; set; }
        public int Contact_SocialSecurityNum { get; set; }
        public Guid Card_Guid { get; set; }
        public DateTime Card_RegisteredOn { get; set; }
        public DateTime Card_ChargeExpiresOn { get; set; }
    }
}
