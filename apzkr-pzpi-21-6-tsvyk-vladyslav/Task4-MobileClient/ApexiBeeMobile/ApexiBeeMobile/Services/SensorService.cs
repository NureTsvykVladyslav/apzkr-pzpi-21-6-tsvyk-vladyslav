﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Models;
using Xamarin.Essentials;

namespace ApexiBeeMobile.Services
{
    public class SensorService : ServiceBase, ISensorService
    {
        public async Task<IEnumerable<Sensor>> GetLastReadingSensorsOfHive(Guid hiveId)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"{Preferences.Get("ApiUrl", string.Empty)}/api/Sensor/hive/{hiveId}/last");
            var sensors = JsonSerializer.Deserialize<IEnumerable<Sensor>>(result, options);
            foreach (Sensor sensor in sensors)
            {
                var sensorTypeId = sensor.SensorTypeId;
                var detailedSensor = await this.GetSensorWithType(sensorTypeId);
                sensor.SensorType = detailedSensor.SensorType;
            }

            return sensors;
        }

        public async Task<Sensor> GetSensorWithType(Guid sensorId)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"{Preferences.Get("ApiUrl", string.Empty)}/api/Sensor/brief/{sensorId}");
            return JsonSerializer.Deserialize<Sensor>(result, options);
        }
    }
}
