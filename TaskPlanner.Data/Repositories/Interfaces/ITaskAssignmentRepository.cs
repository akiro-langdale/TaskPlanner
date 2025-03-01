namespace TaskPlanner.Data.Repositories.Interfaces
{
    using TaskPlanner.Data.Models;
    public interface ITaskAssignmentRepository
    {
        TaskAssignmentModel GetTaskAssignmentById(int taskAssignmentId);
        IEnumerable<TaskAssignmentModel> GetAllTaskAssignments();
        void AddTaskAssignment(TaskAssignmentModel taskAssignment);
        void UpdateTaskAssignment(TaskAssignmentModel taskAssignment);
        void DeleteTaskAssignment(int taskAssignmentId);
    }
}