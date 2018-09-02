using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AthEna_WebApi.Models
{
    public partial class ValidationActivity
    {
        [JsonIgnore]
        public int Id { get; set; }
        public Guid VactivityId { get; set; }
        public DateTime ValidatedOn { get; set; }
        public int? ValidatedAt { get; set; }
        public Guid? StationId { get; set; }
        public Guid? BusId { get; set; }
        public Guid? RouteId { get; set; }
        public Guid CardId { get; set; }

        [JsonIgnore]
        public Vehicle Bus { get; set; }

        [JsonIgnore]
        public Card Card { get; set; }

        [JsonIgnore]
        public Route Route { get; set; }

        [JsonIgnore]
        public MetroStation Station { get; set; }
    }
}
