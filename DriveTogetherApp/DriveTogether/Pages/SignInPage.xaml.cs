namespace DriveTogether.Pages
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using SQLite.Net;
    using SQLite.Net.Async;
    using SQLite.Net.Platform.WinRT;

    using Common;
    using Models;
    using ViewModels;

    public sealed partial class SignInPage : Page
    {
        private const string InputErrorMessage = "Input error, all fields are required or invalid Username or Password!";

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public SignInPage()
            :this(new SignInViewModel())
        {
        }

        public SignInPage(SignInViewModel viewModel)
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.ViewModel = viewModel;
        }

        public SignInViewModel ViewModel
        {
            get
            {
                return this.DataContext as SignInViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }


        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void OnSignInUserButtonClick(object sender, RoutedEventArgs e)
        {
            this.progressRing.Visibility = Visibility.Visible;
            bool isInputValid = this.ViewModel.ValidateInput();
            if (!isInputValid)
            {
                this.progressRing.Visibility = Visibility.Collapsed;
                this.ShowInfoMessage(InputErrorMessage);
                return;
            }
            else
            {
                this.SignInUser();
            }
        }

        private async void SignInUser()
        {
            bool singInSuccess = await this.ViewModel.SignInAsync();
            this.progressRing.Visibility = Visibility.Collapsed;
            if (singInSuccess)
            {
                this.ViewModel.IsActive = true;
                if (this.KeepMeLoggedIn.IsChecked.Value)
                {
                    this.UserSignInStateUpdate();
                }
                this.Frame.Navigate(typeof(HomePage));
            }
            else
            {
                this.ShowInfoMessage(this.ViewModel.ServerErrorMessage);
            }
        }

        private async void ShowInfoMessage(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        private async void UserSignInStateUpdate()
        {
            var item = new SaveStateModel
            {
                IsSignedIn = this.ViewModel.IsActive,
                Email = this.EmailBox.Text,
                Password = this.PasswordBox.Password
            };

            await this.InsertStateAsync(item);
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

        private async Task<int> InsertStateAsync(SaveStateModel item)
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.InsertAsync(item);
            return result;
        }
    }
}
