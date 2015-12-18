namespace DriveTogether.Pages
{
    using System;

    using ViewModels;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Popups;

    public sealed partial class SignUpPage : Page
    {
        private const string InputErrorMessage = "Input error, all fields are required!";
        private const string SuccesSignUpMessage = "You have successfully sign up!";
        
        public SignUpPage() 
            : this(new SignUpViewModel())
        {
        }

        public SignUpPage(SignUpViewModel viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
        }
        
        public SignUpViewModel ViewModel
        {
            get
            {
                return this.DataContext as SignUpViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Execute logic when back button is clicked
        /// </summary>
        /// <param name="sender">Gets the element sender</param>
        /// <param name="eventArg">Gets routed event arguments</param>
        private void OnBackButtonClick(object sender, RoutedEventArgs eventArg)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// Execute logic when sign up button is clicked
        /// </summary>
        /// <param name="sender">Gets the element sender</param>
        /// <param name="eventArg">Gets routed event arguments</param>
        public void OnSignUpUserButtonClick(object sender, RoutedEventArgs eventArg)
        {
            this.progressRing.Visibility = Visibility.Visible;
            bool isInputValid = this.ViewModel.ValidateInput();
            if (!isInputValid)
            {
                this.ShowInfoMessage(InputErrorMessage);
                return;
            }
            else
            {
                this.SignUpUser();
            }
        }

        /// <summary>
        /// Sign up a new user
        /// On success navigate to Sign In page
        /// On error show pop-up message for error
        /// </summary>
        private async void SignUpUser()
        {
            bool signUpSuccess = await this.ViewModel.SignUp();
            this.progressRing.Visibility = Visibility.Collapsed;
            if (signUpSuccess)
            {
                this.ShowInfoMessage(SuccesSignUpMessage);
                this.Frame.Navigate(typeof(SignInPage));
            }
            else
            {
                this.ShowInfoMessage(this.ViewModel.ServerErrorMessage);
            }
        }

        /// <summary>
        /// Show pop-up message dialog
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        private async void ShowInfoMessage(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
