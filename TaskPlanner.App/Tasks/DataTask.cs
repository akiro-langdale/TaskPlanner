using System.Windows.Data;
using TaskPlanner.Data.Models;

namespace TaskPlanner.App.Tasks
{
    public class DataTask
    {
        #region CurrentTask
        private TaskModel _currentTask;
        public TaskModel CurrentTask
        {
            get { return _currentTask; }
            set { _currentTask = value; }
        }
        #endregion

        #region CurrentUser
        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        #endregion

        #region CurrentTaskAssignment
        private TaskAssignmentModel _currentTaskAssignment;
        public TaskAssignmentModel CurrentTaskAssignment
        {
            get { return _currentTaskAssignment; }
            set { _currentTaskAssignment = value; }
        }
        #endregion
    }
}
