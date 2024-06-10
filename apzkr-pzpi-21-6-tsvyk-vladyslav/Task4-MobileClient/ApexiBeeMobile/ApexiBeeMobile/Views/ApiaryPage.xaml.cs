using ApexiBeeMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApexiBeeMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApiaryPage : ContentPage
    {
        ApiariesViewModel apiariesViewModel { get; set; }

        public ApiaryPage()
        {
            apiariesViewModel = new ApiariesViewModel();
            this.BindingContext = apiariesViewModel;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await apiariesViewModel.GetApiaries();
            base.OnAppearing();
        }
    }
}