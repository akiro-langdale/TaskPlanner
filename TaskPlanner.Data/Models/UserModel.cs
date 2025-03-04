namespace TaskPlanner.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using TaskPlanner.Data.Models.Base;
    public class UserModel : Model
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        [RegularExpression(@"[0-9]\d{18}$")]
        public string? TelegramId { get; set; }

        [RegularExpression(@"[0-9]\d{10}$")]
        public string? DiscordId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}