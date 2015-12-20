using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace DriveTogether.Pages
{
    public sealed partial class AddTripPage : Page
    {
        public AddTripPage()
        {
            this.InitializeComponent();
        }

        private void OnUserIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyProfilePage));
        }

        private void OnHomeIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }
    }
}
