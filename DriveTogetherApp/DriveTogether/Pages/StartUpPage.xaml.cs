namespace DriveTogether.Pages
{
    using DriveTogether.ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class StartUpPage : Page
    {
        public StartUpPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.DataContext = new StartUpViewModel(e.Parameter as Frame);
        }
    }
}
