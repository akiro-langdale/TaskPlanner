namespace TaskPlanner.Data.Repositories
{
    using TaskPlanner.Data.DBContext;
    using TaskPlanner.Data.Models;
    using TaskPlanner.Data.Repositories.Interfaces;
    public class TaskAssignmentRepository : ITaskAssignmentRepository
    {
        private readonly TaskPlannerDBContext _context;
        public TaskAssignmentRepository(TaskPlannerDBContext context)
        {
            _context = context;
        }

        public TaskAssignmentModel GetTaskAssignmentById(int taskAssignmentId)
        {
            var taskAssignment = _context.TaskAssignments.Find(taskAssignmentId);
            if (taskAssignment == null)
            {
                throw new KeyNotFoundException($"TaskAssignment with ID {taskAssignmentId} not found.");
            }
            return taskAssignment;
        }

        public IEnumerable<TaskAssignmentModel> GetAllTaskAssignments() => _context.TaskAssignments.ToList();

        public void AddTaskAssignment(TaskAssignmentModel taskAssignment)
        {
            _context.TaskAssignments.Add(taskAssignment);
            _context.SaveChanges();
        }

        public void UpdateTaskAssignment(TaskAssignmentModel taskAssignment)
        {
            _context.TaskAssignments.Update(taskAssignment);
            _context.SaveChanges();
        }

        public void DeleteTaskAssignment(int taskAssignmentId)
        {
            var taskAssignment = GetTaskAssignmentById(taskAssignmentId);
            if (taskAssignment != null)
            {
                _context.TaskAssignments.Remove(taskAssignment);
                _context.SaveChanges();
            }
        }
    }
}