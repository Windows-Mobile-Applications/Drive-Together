namespace DriveTogether
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using SQLite.Net;
    using SQLite.Net.Async;
    using SQLite.Net.Platform.WinRT;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.Storage;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using Models;
    using Pages;
    using Parse;

    sealed partial class App : Application
    {
        private bool isSignedIn;
        private string email;
        private string password;

        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            this.InitializeComponent();
            this.Suspending += OnSuspending;

            // Initialize Parse Client
            ParseClient.Initialize("9Dnr7UUEmp4HUdnGMPoTrNlNrUdzgcCjip1LdqcU", "7zWzFZFPaRgaLrQCmwDKpUYiN5IXGBWn9eKcfGJR");


            this.InitAsync();
            this.GetAllStates();

            if (this.isSignedIn)
            {
                this.LogInCurrentUser();
            }
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

        private async void InitAsync()
        {
            var connection = this.GetDbConnectionAsync();
            await connection.CreateTableAsync<SaveStateModel>();
        }

        private async void GetAllStates()
        {
            try
            {
                var userData = await this.GetStateAsync();

                if (userData.Count > 0)
                {
                    foreach (var userItem in userData)
                    {
                        this.isSignedIn = userItem.IsSignedIn;
                        this.email = userItem.Email;
                        this.password = userItem.Password;
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
            return;
        }

        private async Task<List<SaveStateModel>> GetStateAsync()
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.Table<SaveStateModel>().ToListAsync();
            return result;
        }

        private static async Task<ParseUser> LogInInBackground(String email,
            String password)
        {
            await ParseUser.LogInAsync(email, password);
            return ParseUser.CurrentUser;

        }

        private async void LogInCurrentUser()
        {
            var currentUser = await LogInInBackground(this.email, this.password);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user. Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;


            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;
                rootFrame.Navigated += OnNavigated;

                Window.Current.Content = rootFrame;

                SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    rootFrame.CanGoBack ?
                    AppViewBackButtonVisibility.Visible :
                    AppViewBackButtonVisibility.Collapsed;
            }


            if (rootFrame.Content == null)
            {
                if (isSignedIn)
                {
                    rootFrame.Navigate(typeof(HomePage), e.Arguments);
                }
                else
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
            }


            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (((Frame)sender).CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }
    }
}
