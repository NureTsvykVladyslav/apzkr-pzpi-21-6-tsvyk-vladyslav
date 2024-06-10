using System;
using System.Collections.Generic;
using System.Text;

namespace ApexiBeeMobile.Models
{
    public class SensorReading
    {
        public DateTime ReadingDate { get; set; }
        public double Value { get; set; }
        public Guid SensorId { get; set; }
    }
}
