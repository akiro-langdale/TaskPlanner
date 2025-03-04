namespace TaskPlanner.Data.Models.Base
{
    using System.ComponentModel.DataAnnotations;
    public class Model
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
