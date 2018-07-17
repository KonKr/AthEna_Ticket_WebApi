using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AthEna_WebApi.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            ValidationActivity = new HashSet<ValidationActivity>();
        }

        [JsonIgnore]
        public int Id { get; set; }
        public Guid VehicleId { get; set; }

        [Required]
        public string LicensePlate { get; set; }

        [JsonIgnore]
        public ICollection<ValidationActivity> ValidationActivity { get; set; }
    }
}
