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
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel profileViewModel;
        public ProfilePage()
        {
            InitializeComponent();
        }

        public void Init()
        {
            profileViewModel = new ProfileViewModel();
            this.BindingContext = profileViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }
    }
}