namespace TaskPlanner.Data.Repositories.Interfaces
{
    using TaskPlanner.Data.Models;
    public interface IUserRepository
    {
        UserModel GetUserById(int userId);
        IEnumerable<UserModel> GetAllUsers();
        void AddUser(UserModel user);
        void UpdateUser(UserModel user);
        void DeleteUser(int userId);
    }
}