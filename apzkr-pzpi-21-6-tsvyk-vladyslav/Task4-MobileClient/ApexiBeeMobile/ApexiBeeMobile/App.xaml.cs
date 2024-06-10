using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Resources;
using ApexiBeeMobile.Services;
using ApexiBeeMobile.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApexiBeeMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Resource.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            DependencyService.Register<IAuthService, AuthService>();
            DependencyService.Register<IApiaryService, ApiaryService>();
            DependencyService.Register<ISensorService, SensorService>();
            Preferences.Set("ApiUrl", "http://192.168.0.103:5181");
            MainPage = new AppShell();
            Shell.Current.GoToAsync("//AuthPage");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
