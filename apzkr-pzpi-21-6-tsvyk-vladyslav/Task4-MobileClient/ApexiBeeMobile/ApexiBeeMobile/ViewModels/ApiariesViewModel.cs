using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Models;
using ApexiBeeMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApexiBeeMobile.ViewModels
{
    public class ApiariesViewModel
    {
        public ObservableCollection<Apiary> Apiaries { get; set; }
        private Apiary selectedApiary { get; set; }

        private IApiaryService _apiaryService = DependencyService.Get<IApiaryService>();

        public ApiariesViewModel()
        {
            Apiaries = new ObservableCollection<Apiary>();
            this._apiaryService = DependencyService.Get<IApiaryService>();
        }

        public async Task GetApiaries()
        {
            try
            {
                IEnumerable<Apiary> apiaries = await this._apiaryService.GetUserApiaries();
                Apiaries.Clear();

                // Adding loaded from server data to obserable collection
                foreach (var apiary in apiaries)
                    Apiaries.Add(apiary);
            }
            catch
            {
                Debug.WriteLine("Failed to load apiaries list");
                Apiaries = null;
            }
        }

        public Apiary SelectedApiary
        {
            get { return selectedApiary; }
            set
            {
                if (selectedApiary != value)
                {
                    Apiary tempApiary = new Apiary()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        CreationDate = value.CreationDate,
                        Description = value.Description,
                        BeekeeperId = value.BeekeeperId
                    };
                    selectedApiary = null;
                    Shell.Current.GoToAsync($"{nameof(ApiaryDetailsPage)}?Id={tempApiary.Id}");
                }
            }
        }
    }
}
