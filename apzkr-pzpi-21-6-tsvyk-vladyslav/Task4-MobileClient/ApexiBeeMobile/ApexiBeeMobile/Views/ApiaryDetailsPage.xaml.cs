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
    public partial class ApiaryDetailsPage : ContentPage
    {
        ApiaryDetailViewModel viewModel { get; set; }

        public ApiaryDetailsPage()
        {
            viewModel = new ApiaryDetailViewModel();
            this.BindingContext = viewModel;
            InitializeComponent();
        }
    }
}