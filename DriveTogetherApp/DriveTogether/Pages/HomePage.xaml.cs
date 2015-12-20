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

    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private void OnLogOutButtonClick(object sender, RoutedEventArgs e)
        {
            this.DropTable();
            this.Frame.Navigate(typeof(MainPage));
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

        private void ImageOnTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ImageViewer), sender as Image);
        }
    }
}
