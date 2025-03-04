namespace TaskPlanner.App.ViewModels
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Windows.Input;
    using System.Windows;
    using TaskPlanner.App.Commands;
    using TaskPlanner.App.Tasks;
    using TaskPlanner.App.Utils;
    using TaskPlanner.App.ViewModels.Base;
    using TaskPlanner.App.Views.Windows;
    using TaskPlanner.Data.DBContext;
    using TaskPlanner.Data.Models;

    public class TasksListViewModel : ViewModel
    {
        private readonly TaskPlannerDBContext _dbContext;
        private readonly DataTask _dataTask;
        private readonly NavigationTask _navigationTask;
        private AddEditTaskWindow _addEditTaskWindow;

        //public IEnumerable<TaskModel> Tasks => _dbContext.Tasks.Where(x => x. == dataTask.CurrentTask).ToList();
        public IEnumerable<TaskModel> Tasks => _dbContext.Tasks.ToList();

        #region CurrentTask
        private TaskModel _currentTask;
        public TaskModel CurrentTask { get => _currentTask; set => Set(ref _currentTask, value); }
        #endregion

        /// <summary>
        /// Add new record
        /// </summary>
        #region Command AddCommand
        public ICommand AddCommand { get; set; }

        private bool CanAddCommandExecute(object obj) => true;

        private void OnAddCommandExecuted(object obj)
        {
            CurrentTask = new TaskModel();
            CurrentTask.CreatedAt = DateTime.Now;
            CurrentTask.UpdatedAt = DateTime.Now;
            _addEditTaskWindow = ServiceLocator.GetService<AddEditTaskWindow>();
            _addEditTaskWindow.DataContext = this;
            _addEditTaskWindow.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Remove selected record
        /// </summary>
        #region Command RemoveCommand
        public ICommand RemoveCommand { get; set; }

        private bool CanRemoveCommandExecute(object obj) => CurrentTask != null;

        private void OnRemoveCommandExecuted(object obj)
        {
            if (MessageBox.Show($"Вы точно хотите удалить запись:\n{CurrentTask.Id}\n{CurrentTask.Title}\n{CurrentTask.CreatedAt.ToString("dd.MM.yyyy")}", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                TaskModel task = (TaskModel)obj;
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

        private bool CanEditCommandExecute(object obj) => CurrentTask != null;

        private void OnEditCommandExecuted(object obj)
        {
            TaskModel task = (TaskModel)obj;
            CurrentTask = task;
            CurrentTask.UpdatedAt = DateTime.Now;
            _addEditTaskWindow = ServiceLocator.GetService<AddEditTaskWindow>();
            _addEditTaskWindow.DataContext = this;
            _addEditTaskWindow.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Save data from adding/editing window and close it
        /// </summary>
        #region Command SaveAndCloseCommand
        public ICommand SaveAndCloseCommand { get; set; }

        private bool CanSaveAndCloseCommandExecute(object obj)
        {
            if (CurrentTask == null)
                return false;
            if (string.IsNullOrEmpty(CurrentTask.Title))
                return false;
            if (CurrentTask.DueDate < CurrentTask.UpdatedAt || CurrentTask.DueDate < CurrentTask.CreatedAt)
                return false;
            return true;
        }

        private void OnSaveAndCloseCommandExecuted(object obj)
        {
            //CurrentTask.Flat = dataStore.CurrentFlat;
            if (_dbContext.Tasks.Any(x => x.Id == CurrentTask.Id))
            {
                _dbContext.Tasks.Update(CurrentTask);
            }
            else
            {
                _dbContext.Tasks.Add(CurrentTask);
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
            CurrentTask = null;
            _addEditTaskWindow.Close();
        }

        #endregion

        public TasksListViewModel(TaskPlannerDBContext dbContext, DataTask dataTask, NavigationTask navigationTask)
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