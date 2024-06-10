using System;
using System.Collections.Generic;
using System.Text;

namespace ApexiBeeMobile.Models
{
    public class Sensor
    {
        public Guid SensorTypeId { get; set; }
        public SensorType SensorType { get; set; }
        public Guid HiveId { get; set; }
        public SensorReading LastReading { get; set; }
    }
}
