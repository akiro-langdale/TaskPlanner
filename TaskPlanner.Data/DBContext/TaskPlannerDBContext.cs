namespace TaskPlanner.Data.DBContext
{
    using Microsoft.EntityFrameworkCore;
    using TaskPlanner.Data.Models;
    public class TaskPlannerDBContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<PriorityModel> Priorities { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<TaskAssignmentModel> TaskAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=localhost;Database=TaskPlannerDB;Trusted_Connection=True;"); // Cтрока подключения
    }
}