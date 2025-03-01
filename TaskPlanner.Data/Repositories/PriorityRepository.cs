namespace TaskPlanner.Data.Repositories
{
    using TaskPlanner.Data.DBContext;
    using TaskPlanner.Data.Models;
    using TaskPlanner.Data.Repositories.Interfaces;
    public class PriorityRepository : IPriorityRepository
    {
        private readonly TaskPlannerDBContext _context;
        public PriorityRepository(TaskPlannerDBContext context)
        {
            _context = context;
        }

        public PriorityModel GetPriorityById(int priorityId)
        {
            var priority = _context.Priorities.Find(priorityId);
            if (priority == null)
            {
                throw new KeyNotFoundException($"Priority with ID {priorityId} not found.");
            }
            return priority;
        }

        public IEnumerable<PriorityModel> GetAllPriorities() => _context.Priorities.ToList();

        public void AddPriority(PriorityModel priority)
        {
            _context.Priorities.Add(priority);
            _context.SaveChanges();
        }

        public void UpdatePriority(PriorityModel priority)
        {
            _context.Priorities.Update(priority);
            _context.SaveChanges();
        }

        public void DeletePriority(int priorityId)
        {
            var priority = GetPriorityById(priorityId);
            if (priority != null)
            {
                _context.Priorities.Remove(priority);
                _context.SaveChanges();
            }
        }
    }
}