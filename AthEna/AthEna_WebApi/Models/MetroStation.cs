using System;
using System.Collections.Generic;

namespace AthEna_WebApi.Models
{
    public partial class MetroStation
    {
        public MetroStation()
        {
            ValidationActivity = new HashSet<ValidationActivity>();
        }

        public int Id { get; set; }
        public Guid StationId { get; set; }
        public string StationName { get; set; }
        public int IsOnLine { get; set; }
        public int? IsAlsoOnLine { get; set; }

        public ICollection<ValidationActivity> ValidationActivity { get; set; }
    }
}
