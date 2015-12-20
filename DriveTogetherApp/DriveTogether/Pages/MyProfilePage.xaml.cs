﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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
    }
}
