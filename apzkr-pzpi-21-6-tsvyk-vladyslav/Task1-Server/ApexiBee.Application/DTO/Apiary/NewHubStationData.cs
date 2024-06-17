using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexiBee.Application.DTO
{
    public class NewHubStationData
    {
        public Guid? ApiaryId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double CriticalHumidityHigh { get; set; }
        public double CriticalHumidityLow { get; set; }
        public double CriticalTemperatureHigh { get; set; }
        public double CriticalTemperatureLow { get; set; }
        public string SerialNumber { get; set; }
    }
}
