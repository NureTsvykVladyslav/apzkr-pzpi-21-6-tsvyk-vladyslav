using ApexiBeeMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApexiBeeMobile.Interfaces
{
    interface ISensorService
    {
        Task<IEnumerable<Sensor>> GetLastReadingSensorsOfHive(Guid hiveId);

        Task<Sensor> GetSensorWithType(Guid sensorId);
    }
}
