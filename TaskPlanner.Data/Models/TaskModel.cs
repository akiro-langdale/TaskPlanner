namespace TaskPlanner.Data.Models
{
    public class TaskModel
    {
        public required int TaskId { get; set; }
        public required int StatusId { get; set; }
        public required int PriorityId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DueDate { get; set; }
    }
}