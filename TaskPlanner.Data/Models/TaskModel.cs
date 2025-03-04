namespace TaskPlanner.Data.Models
{
    using TaskPlanner.Data.Models.Base;
    public class TaskModel : Model
    {
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DueDate { get; set; }
    }
}