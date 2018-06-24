using System;
using System.Collections.Generic;

namespace AthEna_WebApi.Models
{
    public partial class Vehicles
    {
        public Vehicles()
        {
            ValidationActivity = new HashSet<ValidationActivity>();
        }

        public int Id { get; set; }
        public Guid VehicleId { get; set; }
        public string LicensePlate { get; set; }

        public ICollection<ValidationActivity> ValidationActivity { get; set; }
    }
}
