namespace DriveTogether.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Net.NetworkInformation;
    using System.Threading.Tasks;

    using Windows.Networking.Connectivity;
    using Windows.UI.Popups;

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }

            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // Working only via Local machine. Doesn't work in mobile emulator.
        public async Task<bool> IsConnectedToInternet()
        {
            // bool isConnected = NetworkInterface.GetIsNetworkAvailable();
            bool isConnected = true;
            if (!isConnected)
            {
                await new MessageDialog("No internet connection is avaliable.").ShowAsync();
            }
            else
            {
                ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                NetworkConnectivityLevel connection = InternetConnectionProfile.GetNetworkConnectivityLevel();
                if (connection == NetworkConnectivityLevel.None || connection == NetworkConnectivityLevel.LocalAccess)
                {
                    isConnected = false;
                }
            }

            // return isConnected;
            // Returning true so we can easily test with mobile emulator
            return true;
        }
    }
}
