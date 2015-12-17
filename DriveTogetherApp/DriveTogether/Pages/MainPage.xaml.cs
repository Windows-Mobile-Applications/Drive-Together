namespace DriveTogether.Pages
{
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Frame.Navigate(typeof(StartUpPage), this.Frame);
        }
    }
}
