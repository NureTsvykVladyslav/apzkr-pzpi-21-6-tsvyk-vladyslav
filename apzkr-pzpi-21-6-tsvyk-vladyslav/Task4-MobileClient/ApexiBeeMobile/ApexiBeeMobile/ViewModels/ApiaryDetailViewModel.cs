using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Models;
using Xamarin.Forms;

namespace ApexiBeeMobile.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class ApiaryDetailViewModel
    {
        private readonly IApiaryService apiaryService;

        private readonly ISensorService sensorService;

        public ApiaryDetailViewModel()
        {
            Hives = new ObservableCollection<Hive>();
            apiaryService = DependencyService.Get<IApiaryService>();
            sensorService = DependencyService.Get<ISensorService>();
        }

        public Apiary CurrentApiary { get; set; }

        public ObservableCollection<Hive> Hives { get; set; }

        public string Id
        {
            get
            {
                return CurrentApiary.Id.ToString();
            }

            set
            {
                Guid apiaryId = Guid.Parse(value);
                _ = LoadHives(apiaryId);
            }
        }

        public async Task LoadHives(Guid apiaryId)
        {
            try
            {
                IEnumerable<Hive> hives = await apiaryService.GetApiaryHives(apiaryId);
                Hives.Clear();

                // Adding loaded from server data to obserable collection
                foreach (Hive hive in hives)
                {
                    await LoadHiveSensorData(hive);
                    Hives.Add(hive);
                }
            }
            catch
            {
                Debug.WriteLine("Failed to load hives list");
                Hives = null;
            }
        }

        public async Task LoadHiveSensorData(Hive hive)
        {
            try
            {
                IEnumerable<Sensor> lastReadings = await sensorService.GetLastReadingSensorsOfHive(hive.Id);
                hive.Sensors = lastReadings;
            }
            catch
            {
                Debug.WriteLine("Failed to load hive sensor data");
            }
        }

    }
}
