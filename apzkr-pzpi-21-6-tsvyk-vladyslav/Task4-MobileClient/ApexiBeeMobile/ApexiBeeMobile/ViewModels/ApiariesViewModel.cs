using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Models;
using ApexiBeeMobile.Views;
using Xamarin.Forms;

namespace ApexiBeeMobile.ViewModels
{
    public class ApiariesViewModel
    {
        private readonly IApiaryService _apiaryService;

        private Apiary selectedApiary;

        public ApiariesViewModel()
        {
            Apiaries = new ObservableCollection<Apiary>();
            this._apiaryService = DependencyService.Get<IApiaryService>();
        }

        public ObservableCollection<Apiary> Apiaries { get; set; }

        public Apiary SelectedApiary
        {
            get => selectedApiary;

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

        public async Task GetApiaries()
        {
            try
            {
                IEnumerable<Apiary> apiaries = await this._apiaryService.GetUserApiaries();
                Apiaries.Clear();

                // Adding loaded from server data to obserable collection
                foreach (var apiary in apiaries)
                {
                    Apiaries.Add(apiary);
                }
            }
            catch
            {
                Debug.WriteLine("Failed to load apiaries list");
                Apiaries = null;
            }
        }
    }
}
