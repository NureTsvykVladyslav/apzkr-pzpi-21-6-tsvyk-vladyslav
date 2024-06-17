using ApexiBeeMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApexiBeeMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfileViewModel profileViewModel;

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