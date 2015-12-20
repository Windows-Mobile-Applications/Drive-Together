using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace DriveTogether.Views
{
    public sealed partial class SearchResultsView : UserControl
    {
        public SearchResultsView()
        {
            this.InitializeComponent();
        }

        public string FullName
        {
            get { return (string)GetValue(FullNameProperty); }
            set { SetValue(FullNameProperty, value); }
        }

        public string Seats
        {
            get { return (string)GetValue(SeatsProperty); }
            set { SetValue(SeatsProperty, value); }
        }
        public string FromCity
        {
            get { return (string)GetValue(FromCityProperty); }
            set { SetValue(FromCityProperty, value); }
        }
        public string ToCity
        {
            get { return (string)GetValue(ToCityProperty); }
            set { SetValue(ToCityProperty, value); }
        }
        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }
        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FullNameProperty =
            DependencyProperty.Register("FullName", typeof(string), typeof(SearchResultsView), new PropertyMetadata(null, new PropertyChangedCallback(HandleFullNameChanged)));

        private static void HandleFullNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SearchResultsView;
            var newValue = e.NewValue.ToString();
            control.fullName.Text = newValue;
        }

        public static readonly DependencyProperty SeatsProperty =
            DependencyProperty.Register("Seats", typeof(string), typeof(SearchResultsView), new PropertyMetadata(null, new PropertyChangedCallback(HandleSeatsChanged)));

        private static void HandleSeatsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SearchResultsView;
            var newValue = e.NewValue.ToString();
            control.tbSeats.Text = newValue;
        }

        public static readonly DependencyProperty FromCityProperty =
            DependencyProperty.Register("FromCity", typeof(string), typeof(SearchResultsView), new PropertyMetadata(null, new PropertyChangedCallback(HandleFromCityChanged)));

        private static void HandleFromCityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SearchResultsView;
            var newValue = e.NewValue.ToString();
            control.tbFromCity.Text = newValue;
        }

        public static readonly DependencyProperty ToCityProperty =
            DependencyProperty.Register("ToCity", typeof(string), typeof(SearchResultsView), new PropertyMetadata(null, new PropertyChangedCallback(HandleToCityChanged)));

        private static void HandleToCityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SearchResultsView;
            var newValue = e.NewValue.ToString();
            control.tbToCity.Text = newValue;
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(string), typeof(SearchResultsView), new PropertyMetadata(null, new PropertyChangedCallback(HandleDateChanged)));

        private static void HandleDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SearchResultsView;
            var newValue = e.NewValue.ToString();
            control.tbDate.Text = newValue;
        }

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(string), typeof(SearchResultsView), new PropertyMetadata(null, new PropertyChangedCallback(HandleTimeChanged)));

        private static void HandleTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SearchResultsView;
            var newValue = e.NewValue.ToString();
            control.tbTime.Text = newValue;
        }
    }
}
