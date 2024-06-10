using ApexiBeeMobile.ViewModels;
using ApexiBeeMobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ApexiBeeMobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ApiaryPage), typeof(ApiaryPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(ApiaryDetailsPage), typeof(ApiaryDetailsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AuthPage");
        }
    }
}
