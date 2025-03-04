namespace TaskPlanner.App.ViewModels
{
    using TaskPlanner.App.Tasks;
    using TaskPlanner.App.Utils;
    using TaskPlanner.App.ViewModels.Base;

    public class BDViewerVeiwModel : ViewModel
    {
        private readonly NavigationTask navigationTask;

        public ViewModel CurrentViewModel => navigationTask.CurrentViewModel;

        public BDViewerVeiwModel(NavigationTask navigationTask)
        {
            this.navigationTask = navigationTask;
            navigationTask.CurrentViewModelChanged += NavigationTask_CurrentViewModelChanged;
            navigationTask.CurrentViewModel = ServiceLocator.GetService<UsersListViewModel>();
        }

        private void NavigationTask_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}