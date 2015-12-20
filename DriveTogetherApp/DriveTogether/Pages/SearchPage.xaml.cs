using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace DriveTogether.Pages
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
        }

        private void OnSearchButtonClick(object sender, RoutedEventArgs e)
        {
            this.SearchOptions.Visibility = Visibility.Collapsed;
            this.FirstRow.Height = new GridLength(0);
            this.SecondRow.Height = new GridLength(1, GridUnitType.Star);
            this.ShowHidenMenu.Visibility = Visibility.Visible;
        }

        private void ShowHidenMenu_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            this.SearchOptions.Visibility = Visibility.Visible;
            this.FirstRow.Height = new GridLength(1, GridUnitType.Star);
            this.SecondRow.Height = new GridLength(1.7, GridUnitType.Star);
            this.ShowHidenMenu.Visibility = Visibility.Collapsed;
        }
    }
}
