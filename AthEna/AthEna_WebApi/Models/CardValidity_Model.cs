using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.Models
{
    public class CardValidity_Model
    {
        public bool Validity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
