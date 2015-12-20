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
                        FullName = "Jane Doe",
                        Seats = "3",
                        FromCity = "Varna",
                        ToCity = "Burgas",
                        Date = "13.04.2015",
                        Time = "12:15"
                    },
                    new SearchResultViewModel()
                    {
                        FullName = "Jane Doe",
                        Seats = "3",
                        FromCity = "Varna",
                        ToCity = "Burgas",
                        Date = "13.04.2015",
                        Time = "12:15"
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
            this.FirstRow.Height = new GridLength(1, GridUnitType.Star);
            this.SecondRow.Height = new GridLength(1.7, GridUnitType.Star);
            this.ShowHidenMenu.Visibility = Visibility.Collapsed;
        }
        
    }
}
