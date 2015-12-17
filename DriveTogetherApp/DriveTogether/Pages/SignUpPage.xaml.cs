namespace DriveTogether.Pages
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();

        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
