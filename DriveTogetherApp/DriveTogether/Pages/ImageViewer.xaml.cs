
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace DriveTogether.Pages
{
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class ImageViewer : Page
    {
        public ImageViewer()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var uri = (e.Parameter as Image).Source;
            this.Image.Source = uri;
        }

        private void ImageOnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var scale = e.Delta.Scale;

            this.Image.Height *= scale;
            this.Image.Width *= scale;
        }

        private void ContainerOnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            //this.Image.Width = 360;
        }
    }
}
