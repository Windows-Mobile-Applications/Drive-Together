using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using DriveTogether.Views;

namespace DriveTogether.Pages
{
    public sealed partial class SearchResultsDetailsPage : Page
    {
        public SearchResultsDetailsPage()
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var result = (e.Parameter as SearchResultsView);
            this.tbSeats.Text = result.Seats;
            this.fullName.Text = result.FullName;
            this.tbFromCity.Text = result.FromCity;
            this.tbToCity.Text = result.ToCity;
            this.tbDate.Text = result.Date;
            this.tbTime.Text = result.Time;
        }
    }
}
