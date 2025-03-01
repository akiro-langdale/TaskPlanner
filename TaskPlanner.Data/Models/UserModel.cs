namespace TaskPlanner.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserModel
    {
        public required int UserId { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }

        [RegularExpression(@"[0-9]\d{18}$")]
        public string? TelegramId { get; set; }

        [RegularExpression(@"[0-9]\d{10}$")]
        public string? DiscordId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}