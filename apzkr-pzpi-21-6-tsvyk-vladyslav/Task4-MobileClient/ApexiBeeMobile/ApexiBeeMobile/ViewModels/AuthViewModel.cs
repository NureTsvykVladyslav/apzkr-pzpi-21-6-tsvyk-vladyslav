using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Resources;
using ApexiBeeMobile.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApexiBeeMobile.ViewModels
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public bool IsMessageExists { get; set; }

        public string MessageText { get; set; } = string.Empty;

        public AuthViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
        }

        public async Task Login()
        {
            if (Password == string.Empty || Username == string.Empty)
            {
                IsMessageExists = true;
                MessageText = Resource.CredentialsRequired;
                ChangedPropertyMessage();
                return;
            }

            try
            {
                bool authResult = await _authService.SignIn(Username, Password);
                if (authResult)
                {
                    Reset();
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
            catch
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

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Reset()
        {
            Password = string.Empty;
            Username = string.Empty;
            IsMessageExists = false;
            MessageText = string.Empty;
            ChangedPropertyMessage();
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(Username));
        }
    }
}
