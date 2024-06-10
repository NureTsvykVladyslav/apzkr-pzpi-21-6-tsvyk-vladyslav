using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Resources;
using ApexiBeeMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ApexiBeeMobile.ViewModels
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        public string Password { get; set; } = "";
        public string Username { get; set; } = "";
        public bool IsMessageExists { get; set; }
        public string MessageText { get; set; } = "";

        private IAuthService _authService = DependencyService.Get<IAuthService>();

        public AuthViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
        }

        public async void Login()
        {
            if (Password == "" || Username == "")
            {
                IsMessageExists = true;
                MessageText = Resource.CredentialsRequired;
                ChangedPropertyMessage();
                return;
            }
            try
            {
                bool authResult = await _authService.SignIn(Username, Password);
                if (authResult == true)
                {
                    Reset();

                    var newPage = new ApiaryPage();
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync($"//{nameof(ApiaryPage)}");

                }
                else
                {
                    IsMessageExists = true;
                    MessageText = Resource.IncorrectCredentials;
                    ChangedPropertyMessage();
                }
            }
            catch(Exception ex)
            {
                IsMessageExists = true;
                MessageText = Resource.ServerError;
                ChangedPropertyMessage();
            }
        }

        private void ChangedPropertyMessage()
        {
            OnPropertyChanged(nameof(IsMessageExists));
            OnPropertyChanged(nameof(MessageText));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Reset()
        {
            Password = "";
            Username = "";
            IsMessageExists = false;
            MessageText = "";
            ChangedPropertyMessage();
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(Username));
        }
    }
}
