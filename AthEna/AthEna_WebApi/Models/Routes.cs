using System;
using System.Collections.Generic;

namespace AthEna_WebApi.Models
{
    public partial class Routes
    {
        public Routes()
        {
            ValidationActivity = new HashSet<ValidationActivity>();
        }

        public int Id { get; set; }
        public Guid RouteId { get; set; }
        public string RouteCodeNum { get; set; }
        public string RouteStartPoint { get; set; }
        public string RouteEndPoint { get; set; }

        public ICollection<ValidationActivity> ValidationActivity { get; set; }
    }
}
