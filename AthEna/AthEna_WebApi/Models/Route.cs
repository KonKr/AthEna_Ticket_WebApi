using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AthEna_WebApi.Models
{
    public partial class Route
    {
        public Route()
        {
            ValidationActivity = new HashSet<ValidationActivity>();
        }

        [JsonIgnore]
        public int Id { get; set; }
        public Guid RouteId { get; set; }
        
        [Required]
        public string RouteCodeNum { get; set; }

        [Required]
        public string RouteStartPoint { get; set; }

        [Required]
        public string RouteEndPoint { get; set; }

        [JsonIgnore]
        public ICollection<ValidationActivity> ValidationActivity { get; set; }
    }
}
