using DriveTogether.ViewModels;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System;

namespace DriveTogether.Pages
{
    public sealed partial class AddTripPage : Page
    {
        private const string InputErrorMessage = "Error, invalid input!";
        private const string SuccesAddingMessage = "You have successfully added a new trip!";

        public AddTripPage() 
            : this(new AddTripViewModel())
        {
        }

        public AddTripPage(AddTripViewModel viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
        }
        
        public AddTripViewModel ViewModel
        {
            get
            {
                return this.DataContext as AddTripViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        private void OnUserIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyProfilePage));
        }

        private void OnHomeIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        private void OnRateIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RateTripPage));
        }

        private void OnAddTripButtonClick(object sender, RoutedEventArgs e)
        {
            //this.progressRing.Visibility = Visibility.Visible;
            bool isInputValid = this.ViewModel.ValidateInput();
            if (!isInputValid)
            {
                this.ShowInfoMessage(InputErrorMessage);
                return;
            }
            else
            {
                this.AddNewTrip();
            }
        }

        private async void AddNewTrip()
        {
            bool submitSuccess = await this.ViewModel.AddNewTripData();
            if (submitSuccess)
            {
                this.ShowInfoMessage(SuccesAddingMessage);
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
    }
}
