using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApexiBeeMobile.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    class ApiaryDetailViewModel
    {
        public Apiary CurrentApiary { get; set; }

        public ObservableCollection<Hive> Hives { get; set; }

        private IApiaryService apiaryService { get; set; }
        private ISensorService sensorService { get; set; }

        public ApiaryDetailViewModel()
        {
            Hives = new ObservableCollection<Hive>();
            apiaryService = DependencyService.Get<IApiaryService>();
            sensorService = DependencyService.Get<ISensorService>();
        }

        public string Id
        {
            set
            {
                Guid apiaryId = Guid.Parse(value);
                LoadHives(apiaryId);
            }
            get
            {
                return CurrentApiary.Id.ToString();
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
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to load hive sensor data");
            }
        }

    }
}
