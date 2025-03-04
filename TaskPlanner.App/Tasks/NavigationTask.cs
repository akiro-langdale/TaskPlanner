using TaskPlanner.App.ViewModels.Base;

namespace TaskPlanner.App.Tasks
{
    public class NavigationTask
    {
        #region CurrentViewModel
        private ViewModel _currentViewModel;
        public ViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }
        #endregion

        #region CurrentViewModelChanged
        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
        #endregion
    }
}
