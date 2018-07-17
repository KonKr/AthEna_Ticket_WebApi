using AthEna_WebApi.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AthEna_WebApi.Models
{
    public partial class Contact
    {
        public Contact()
        {
            Cards = new HashSet<Card>();
        }

        [JsonIgnore]
        public int Id { get; set; }

        public Guid ContactId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string IdCardNum { get; set; }

        [Range(1, int.MaxValue)]
        public int SocialSecurityNum { get; set; }

        public ICollection<Card> Cards { get; set; }
       
    }
}
