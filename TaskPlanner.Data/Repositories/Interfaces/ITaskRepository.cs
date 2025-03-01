namespace TaskPlanner.Data.Repositories.Interfaces
{
    using TaskPlanner.Data.Models;
    public interface ITaskRepository
    {
        TaskModel GetTaskById(int taskId);
        IEnumerable<TaskModel> GetAllTasks();
        void AddTask(TaskModel task);
        void UpdateTask(TaskModel task);
        void DeleteTask(int taskId);
    }
}