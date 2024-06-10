using System;
using System.Collections.Generic;
using System.Text;

namespace ApexiBeeMobile.Models
{
    public class Hive
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfFrames { get; set; }

        public IEnumerable<Sensor> Sensors { get; set; }
    }
}
