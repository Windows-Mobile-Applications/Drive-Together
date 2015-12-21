using Windows.UI.Xaml.Input;

namespace DriveTogether.Pages
{
    using System;
    using System.IO;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using SQLite.Net;
    using SQLite.Net.Async;
    using SQLite.Net.Platform.WinRT;

    using Models;
    using ViewModels;
    using System.Collections.Generic;
    public sealed partial class HomePage : Page
    {
        public HomePage()
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

        private void OnLogOutButtonClick(object sender, RoutedEventArgs e)
        {
            this.DropTable();
            this.Frame.Navigate(typeof (MainPage));
        }

        private SQLiteAsyncConnection GetDbConnectionAsync()
        {
            var dbFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            var connectionFactory =
                new Func<SQLiteConnectionWithLock>(
                    () =>
                        new SQLiteConnectionWithLock(
                            new SQLitePlatformWinRT(),
                            new SQLiteConnectionString(dbFilePath, storeDateTimeAsTicks: false)));

            var asyncConnection = new SQLiteAsyncConnection(connectionFactory);

            return asyncConnection;
        }

        private async void DropTable()
        {
            var connection = this.GetDbConnectionAsync();
            await connection.DeleteAllAsync<SaveStateModel>();
        }

        private void OnAddTripButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof (AddTripPage));
        }

        private void OnSearchButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SearchPage));
        }

        private void OnUserIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyProfilePage));
        }

        private void OnSearchResultDoubleTap(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SearchResultsDetailsPage), sender);
        }
    }
}