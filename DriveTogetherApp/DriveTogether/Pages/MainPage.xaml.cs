namespace DriveTogether.Pages
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private void OnSignInButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignInPage));
        }

        private void OnSignUpButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUpPage));
        }
    }
}
