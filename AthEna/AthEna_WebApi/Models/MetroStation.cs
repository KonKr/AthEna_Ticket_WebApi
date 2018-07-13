using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AthEna_WebApi.Models
{
    public partial class MetroStation
    {
        public MetroStation()
        {
            ValidationActivity = new HashSet<ValidationActivity>();
        }

        [JsonIgnore]
        public int Id { get; set; }

        public Guid StationId { get; set; }

        [Required]
        public string StationName { get; set; }

        [Range(1, 3)]
        public int IsOnLine { get; set; }

        public int? IsAlsoOnLine { get; set; }

        [JsonIgnore]
        public ICollection<ValidationActivity> ValidationActivity { get; set; }
    }
}
