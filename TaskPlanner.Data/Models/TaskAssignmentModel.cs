namespace TaskPlanner.Data.Models
{
    using TaskPlanner.Data.Models.Base;
    public class TaskAssignmentModel : Model
    {
        public required int TaskId { get; set; }
        public required int UserId { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}