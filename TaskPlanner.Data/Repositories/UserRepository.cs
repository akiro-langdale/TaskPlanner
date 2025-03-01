namespace TaskPlanner.Data.Repositories
{
    using TaskPlanner.Data.DBContext;
    using TaskPlanner.Data.Models;
    using TaskPlanner.Data.Repositories.Interfaces;
    public class UserRepository : IUserRepository
    {
        private readonly TaskPlannerDBContext _context;
        public UserRepository(TaskPlannerDBContext context)
        {
            _context = context;
        }

        public UserModel GetUserById(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            return user;
        }

        public IEnumerable<UserModel> GetAllUsers() => _context.Users.ToList();

        public void AddUser(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(UserModel user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}