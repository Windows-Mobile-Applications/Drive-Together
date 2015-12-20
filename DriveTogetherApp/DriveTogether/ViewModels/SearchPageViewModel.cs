namespace DriveTogether.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class SearchPageViewModel : BaseViewModel
    {
        public ObservableCollection<SearchResultViewModel> results;

        public IEnumerable<SearchResultViewModel> Results
        {
            get
            {
                if (this.results == null)
                {
                    this.results = new ObservableCollection<SearchResultViewModel>();
                }

                return this.results;
            }
            set
            {
                if (this.results == null)
                {
                    this.results = new ObservableCollection<SearchResultViewModel>();
                }

                this.results.Clear();
                foreach (var item in value)
                {
                    this.results.Add(item);
                }
            }
        }
    }
}
