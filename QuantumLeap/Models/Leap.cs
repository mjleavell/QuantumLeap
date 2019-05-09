using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumLeap.Models
{
    public class Leap
    {
        public int Id { get; set; }
        public int LeapeeEventId { get; set; }
        public int LeaperId { get; set; }
        public decimal Cost { get; set; }
    }
}
