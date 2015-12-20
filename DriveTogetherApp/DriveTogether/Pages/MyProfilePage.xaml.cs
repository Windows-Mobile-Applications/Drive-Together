using System;
using Windows.Media.Capture;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace DriveTogether.Pages
{
    public sealed partial class MyProfilePage : Page
    {
        public MyProfilePage()
        {
            this.InitializeComponent();
        }

        private void OnHomeIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        private void OnImageTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ImageViewer), sender as Image);
        }

        private void OnEditButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            this.OpenFiles();
        }

        private async void OpenFiles()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                this.ProfilePicture.Source = new BitmapImage(new Uri(file.Path));
            }
            else
            {
            }
        }

        private async void InitCamera()
        {
            var camera = new CameraCaptureUI();

            var photo = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo != null)
            {
                this.ProfilePicture.Source = new BitmapImage(new Uri(photo.Path));
            }
        }

        private void OnCameraButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            this.InitCamera();
        }
    }
}
