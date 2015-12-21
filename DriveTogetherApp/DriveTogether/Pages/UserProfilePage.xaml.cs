using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace DriveTogether.Pages
{
    public sealed partial class UserProfilePage : Page
    {
        public UserProfilePage()
        {
            this.InitializeComponent();
            
        }

        private void OnHomeIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        private void OnUserIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyProfilePage));
        }

        private void OnRateIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RateTripPage));
        }
    }
}
