namespace TaskPlanner.App.ViewModels
{
    using Microsoft.EntityFrameworkCore;
    using System.Windows.Input;
    using System.Windows;
    using TaskPlanner.App.Commands;
    using TaskPlanner.App.Tasks;
    using TaskPlanner.App.Utils;
    using TaskPlanner.App.ViewModels.Base;
    using TaskPlanner.App.Views.Windows;
    using TaskPlanner.Data.DBContext;
    using TaskPlanner.Data.Models;

    public class UsersListViewModel : ViewModel
    {
        private readonly TaskPlannerDBContext _dbContext;
        private readonly DataTask _dataTask;
        private readonly NavigationTask _navigationTask;
        private AddEditUserWindow _addEditUserWindow;

        public IEnumerable<UserModel> Users => _dbContext.Users.ToList();

        #region CurrentTask
        private UserModel _currentUser;
        public UserModel CurrentUser { get => _currentUser; set => Set(ref _currentUser, value); }
        #endregion

        /// <summary>
        /// Add new record
        /// </summary>
        #region Command AddCommand
        public ICommand AddCommand { get; set; }

        private bool CanAddCommandExecute(object obj) => true;

        private void OnAddCommandExecuted(object obj)
        {
            CurrentUser = new UserModel();
            CurrentUser.CreatedAt = DateTime.Now;
            CurrentUser.UpdatedAt = DateTime.Now;
            _addEditUserWindow = ServiceLocator.GetService<AddEditUserWindow>();
            _addEditUserWindow.DataContext = this;
            _addEditUserWindow.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Remove selected record
        /// </summary>
        #region Command RemoveCommand
        public ICommand RemoveCommand { get; set; }

        private bool CanRemoveCommandExecute(object obj) => CurrentUser != null;

        private void OnRemoveCommandExecuted(object obj)
        {
            if (MessageBox.Show($"Вы точно хотите удалить запись:\n{CurrentUser.Id}\n{CurrentUser.Username}\n{CurrentUser.CreatedAt.ToString("dd.MM.yyyy")}", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                UserModel task = (UserModel)obj;
                _dbContext.Remove(task);
                _dbContext.SaveChanges();
            }
        }
        #endregion

        /// <summary>
        /// Open adding/editing window with selected record
        /// </summary>
        #region Command EditCommand
        public ICommand EditCommand { get; set; }

        private bool CanEditCommandExecute(object obj) => CurrentUser != null;

        private void OnEditCommandExecuted(object obj)
        {
            UserModel user = (UserModel)obj;
            CurrentUser = user;
            CurrentUser.UpdatedAt = DateTime.Now;
            _addEditUserWindow = ServiceLocator.GetService<AddEditUserWindow>();
            _addEditUserWindow.DataContext = this;
            _addEditUserWindow.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Save data from adding/editing window and close it
        /// </summary>
        #region Command SaveAndCloseCommand
        public ICommand SaveAndCloseCommand { get; set; }

        private bool CanSaveAndCloseCommandExecute(object obj)
        {
            if (CurrentUser == null)
                return false;
            if (string.IsNullOrEmpty(CurrentUser.Username))
                return false;
            return true;
        }

        private void OnSaveAndCloseCommandExecuted(object obj)
        {
            //CurrentTask.Flat = dataStore.CurrentFlat;
            if (_dbContext.Users.Any(x => x.Id == CurrentUser.Id))
            {
                _dbContext.Users.Update(CurrentUser);
            }
            else
            {
                _dbContext.Users.Add(CurrentUser);
            }
            _dbContext.SaveChanges();
            OnCloseCommandExecuted(obj);
        }

        #endregion

        /// <summary>
        /// Close adding/editing window
        /// </summary>
        #region Command CloseCommand
        public ICommand CloseCommand { get; set; }

        private bool CanCloseCommandExecute(object obj) => true;

        private void OnCloseCommandExecuted(object obj)
        {
            CurrentUser = null;
            _addEditUserWindow.Close();
        }

        #endregion

        public UsersListViewModel(TaskPlannerDBContext dbContext, DataTask dataTask, NavigationTask navigationTask)
        {
            _dbContext = dbContext;
            _dataTask = dataTask;
            _navigationTask = navigationTask;
            _dbContext.SavedChanges += OnAppDbContext_SavedChanges;


            AddCommand = new LambdaCommand(OnAddCommandExecuted, CanAddCommandExecute);
            RemoveCommand = new LambdaCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
            EditCommand = new LambdaCommand(OnEditCommandExecuted, CanEditCommandExecute);
            SaveAndCloseCommand = new LambdaCommand(OnSaveAndCloseCommandExecuted, CanSaveAndCloseCommandExecute);
            CloseCommand = new LambdaCommand(OnCloseCommandExecuted, CanCloseCommandExecute);
        }

        /// <summary>
        /// When changes saved in DB
        /// Need to update flats list
        /// </summary>
        private void OnAppDbContext_SavedChanges(object? sender, SavedChangesEventArgs e)
        {
            OnPropertyChanged(nameof(Tasks));
        }
    }
}