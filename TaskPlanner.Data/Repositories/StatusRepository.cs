namespace TaskPlanner.Data.Repositories
{
    using TaskPlanner.Data.DBContext;
    using TaskPlanner.Data.Models;
    using TaskPlanner.Data.Repositories.Interfaces;
    public class StatusRepository : IStatusRepository
    {
        private readonly TaskPlannerDBContext _context;
        public StatusRepository(TaskPlannerDBContext context)
        {
            _context = context;
        }

        public StatusModel GetStatusById(int statusId)
        {
            var status = _context.Statuses.Find(statusId);
            if (status == null)
            {
                throw new KeyNotFoundException($"Status with ID {statusId} not found.");
            }
            return status;
        }

        public IEnumerable<StatusModel> GetAllStatuses() => _context.Statuses.ToList();

        public void AddStatus(StatusModel status)
        {
            _context.Statuses.Add(status);
            _context.SaveChanges();
        }

        public void UpdateStatus(StatusModel status)
        {
            _context.Statuses.Update(status);
            _context.SaveChanges();
        }

        public void DeleteStatus(int statusId)
        {
            var status = GetStatusById(statusId);
            if (status != null)
            {
                _context.Statuses.Remove(status);
                _context.SaveChanges();
            }
        }
    }
}