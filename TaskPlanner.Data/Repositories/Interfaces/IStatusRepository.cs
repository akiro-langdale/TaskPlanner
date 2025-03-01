namespace TaskPlanner.Data.Repositories.Interfaces
{
    using TaskPlanner.Data.Models;
    public interface IStatusRepository
    {
        StatusModel GetStatusById(int statusId);
        IEnumerable<StatusModel> GetAllStatuses();
        void AddStatus(StatusModel status);
        void UpdateStatus(StatusModel status);
        void DeleteStatus(int statusId);
    }
}