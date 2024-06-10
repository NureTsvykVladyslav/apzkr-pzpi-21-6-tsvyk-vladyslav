using ApexiBeeMobile.DTO;
using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ApexiBeeMobile.Localisation;
using System.Windows.Input;

namespace ApexiBeeMobile.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private IAuthService _authService;

        public User CurrentBeekeeper { get; set; }

        TimeSpan timeOffset = new DateTimeOffset(DateTime.Now).Offset;

        public string TimeOffset
        {
            get
            {
                int offset = timeOffset.Hours;
                if (offset > 0)
                    return $"GTM+{offset}";
                if (offset == 0)
                    return "GTM 0";
                else
                    return $"GTM{offset}";
            }
        }

        public DateTime CurrentTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        public ICommand UpdateProfileCommand { get; }

        public ProfileViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
            LoadBeekeeper();
            UpdateProfileCommand = new Command(async () => await UpdateUserProfile());
        }

        public async void LoadBeekeeper()
        {
            Guid cynologistId = _authService.GetUserAccountIdFromToken();
            CurrentBeekeeper = await _authService.GetUserById(cynologistId);
            OnPropertyChanged(nameof(CurrentBeekeeper));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task UpdateUserProfile()
        {
            var updateUserModel = new UpdateUserModel
            {
                UserAccountId = CurrentBeekeeper.Id,
                FirstName = CurrentBeekeeper.FirstName,
                LastName = CurrentBeekeeper.LastName
            };

            var isSuccess = await this._authService.UpdateUserProfile(updateUserModel);
            if (isSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Localize.GetString("Success"), Localize.GetString("ProfileUpdateSuccess"), Localize.GetString("Ok"));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(Localize.GetString("Error"), Localize.GetString("ProfileUpdateError"), Localize.GetString("Ok"));
            }
        }
    }
}
