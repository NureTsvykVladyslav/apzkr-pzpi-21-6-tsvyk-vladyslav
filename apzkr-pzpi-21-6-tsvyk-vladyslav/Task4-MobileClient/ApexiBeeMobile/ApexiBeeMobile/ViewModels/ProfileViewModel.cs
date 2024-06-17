using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using ApexiBeeMobile.DTO;
using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Localisation;
using ApexiBeeMobile.Models;
using Xamarin.Forms;

namespace ApexiBeeMobile.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;

        public event PropertyChangedEventHandler PropertyChanged;

        public User CurrentBeekeeper { get; set; }

        private TimeSpan TimeOffsetValue = new DateTimeOffset(DateTime.Now).Offset;

        public string TimeOffset
        {
            get
            {
                int offset = TimeOffsetValue.Hours;
                if (offset > 0)
                {
                    return $"GTM+{offset}";
                }

                if (offset == 0)
                {
                    return "GTM 0";
                }
                else
                {
                    return $"GTM{offset}";
                }
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

        public async Task LoadBeekeeper()
        {
            Guid cynologistId = _authService.GetUserAccountIdFromToken();
            CurrentBeekeeper = await _authService.GetUserById(cynologistId);
            OnPropertyChanged(nameof(CurrentBeekeeper));
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
