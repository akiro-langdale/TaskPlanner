namespace TaskPlanner.Data.Models
{
    public class TaskAssignmentModel
    {
        public required int TaskAssignmentId { get; set; }
        public required int TaskId { get; set; }
        public required int UserId { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}