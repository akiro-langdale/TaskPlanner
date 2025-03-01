namespace TaskPlanner.Data.Repositories.Interfaces
{
    using TaskPlanner.Data.Models;
    public interface IPriorityRepository
    {
        PriorityModel GetPriorityById(int priorityId);
        IEnumerable<PriorityModel> GetAllPriorities();
        void AddPriority(PriorityModel priority);
        void UpdatePriority(PriorityModel priority);
        void DeletePriority(int priorityId);
    }
}