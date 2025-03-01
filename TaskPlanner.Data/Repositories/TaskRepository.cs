namespace TaskPlanner.Data.Repositories
{    
    using TaskPlanner.Data.DBContext;
    using TaskPlanner.Data.Models;
    using TaskPlanner.Data.Repositories.Interfaces;
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskPlannerDBContext _context;
        public TaskRepository(TaskPlannerDBContext context)
        {
            _context = context;
        }

        public TaskModel GetTaskById(int taskId)
        {
            var task = _context.Tasks.Find(taskId);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} not found.");
            }
            return task;
        }

        public IEnumerable<TaskModel> GetAllTasks() => _context.Tasks.ToList();

        public void AddTask(TaskModel task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskModel task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int taskId)
        {
            var task = GetTaskById(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}