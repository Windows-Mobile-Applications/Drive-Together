using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using DriveTogether.Models;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;

namespace DriveTogether.Pages
{
    public sealed partial class MyProfilePage : Page
    {
        private string imageUrl;


        public MyProfilePage()
        {
            this.InitializeComponent();
            this.InitAsync();
            this.SetNewPicture();
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

        private void OnRateIconTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RateTripPage));
        }

        private async void OpenFiles()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                this.imageUrl = file.Path;

                this.UserPictureUpdate();

                // Application now has read/write access to the picked file
                var bitmapPic = ImageFromRelativePath(this, file.Path);
                this.ProfilePicture.Source = bitmapPic;

                //await this.SaveStreamAsync(picker.SuggestedStartLocation, )
            }

        }

        public static BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            BitmapImage result = new BitmapImage();
            result.UriSource = uri;
            return result;
        }

        private async void InitCamera()
        {
            var camera = new CameraCaptureUI();

            var photo = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo != null)
            {
                this.imageUrl = photo.Path;

                this.UserPictureUpdate();

                var bitmapPic = new BitmapImage(new Uri(photo.Path));
                this.ProfilePicture.Source = bitmapPic;

                StorageFile storageFile = await StorageFile.GetFileFromPathAsync(photo.Path);

                if (storageFile != null)
                {
                    IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read);
                    await ReencodeAndSavePhotoAsync(stream, "myPic.jpg");
                }
            }
        }

        private void OnCameraButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            this.InitCamera();
        }

        private static async Task ReencodeAndSavePhotoAsync(IRandomAccessStream stream, string filename)
        {
            using (var inputStream = stream)
            {
                var decoder = await BitmapDecoder.CreateAsync(inputStream);

                var file = await KnownFolders.PicturesLibrary.CreateFileAsync(filename, CreationCollisionOption.GenerateUniqueName);

                using (var outputStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);
                    
                    await encoder.FlushAsync();
                }
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
            await connection.CreateTableAsync<UserPictureModel>();
        }

        private async Task<int> InsertUserInfoAsync(UserPictureModel item)
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.InsertAsync(item);
            return result;
        }

        private async void UserPictureUpdate()
        {
            var item = new UserPictureModel()
            {
                ImageUrl = this.imageUrl
            };

            await this.InsertUserInfoAsync(item);
        }

        private async Task<List<UserPictureModel>> GetPictureAsync()
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.Table<UserPictureModel>().ToListAsync();
            return result;
        }

        private async void SetNewPicture()
        {
            try
            {
                var userData = await this.GetPictureAsync();

                if (userData.Count > 0)
                {
                    foreach (var userItem in userData)
                    {
                        this.ProfilePicture.Source = new BitmapImage(new Uri(userItem.ImageUrl)); 
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
    }
}
