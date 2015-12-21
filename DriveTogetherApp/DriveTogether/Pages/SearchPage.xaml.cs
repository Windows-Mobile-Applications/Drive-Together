using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using DriveTogether.ViewModels;


namespace DriveTogether.Pages
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
            this.DataContext = new SearchPageViewModel
            {
                Results = new List<SearchResultViewModel>()
                {
                    new SearchResultViewModel()
                    {
                        FullName = "John Doe",
                        Seats = "4",
                        FromCity = "Sofia",
                        ToCity = "Plovdiv",
                        Date = "12.03.2015",
                        Time = "15:10"
                    },
                    new SearchResultViewModel()
                    {
                        FullName = "Jane Doe",
                        Seats = "3",
                        FromCity = "Varna",
                        ToCity = "Burgas",
                        Date = "13.04.2015",
                        Time = "12:15"
                    },
                    new SearchResultViewModel()
                    {
                        FullName = "Pesho Kolev",
                        Seats = "5",
                        FromCity = "Sofia",
                        ToCity = "Burgas",
                        Date = "23.06.2015",
                        Time = "14:00"
                    },
                    new SearchResultViewModel()
                    {
                        FullName = "Gosho Petkov",
                        Seats = "2",
                        FromCity = "Varna",
                        ToCity = "Burgas",
                        Date = "5.11.2015",
                        Time = "15:15"
                    }
                }
            };

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
            this.FirstRow.Height = new GridLength(2, GridUnitType.Star);
            this.SecondRow.Height = new GridLength(1.7, GridUnitType.Star);
            this.ShowHidenMenu.Visibility = Visibility.Collapsed;
        }

        private void OnUserIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyProfilePage));
        }

        private void OnHomeIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        private void OnRateIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RateTripPage));
        }

        private void OnSearchResultDoubleTap(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SearchResultsDetailsPage), sender);
        }
    }
}
