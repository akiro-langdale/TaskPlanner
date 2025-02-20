namespace TaskPlanner.App.ViewModels
{
    using TaskPlanner.App.ViewModels.Base;
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            Title = "My MainWindow";
        }

        #region Title : string - window Title

        /// <summary>Fields - window Title </summary>
        private string _title = string.Empty;

        /// <summary>Properties - window Title </summary>
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion
    }
}
