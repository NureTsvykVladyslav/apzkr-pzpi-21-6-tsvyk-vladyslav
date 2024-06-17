using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApexiBeeMobile.Models;

namespace ApexiBeeMobile.Interfaces
{
    interface ISensorService
    {
        Task<IEnumerable<Sensor>> GetLastReadingSensorsOfHive(Guid hiveId);

        Task<Sensor> GetSensorWithType(Guid sensorId);
    }
}
