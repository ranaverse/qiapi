using System.ComponentModel.DataAnnotations;

namespace qiapi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string RoleCode { get; set; }  // e.g. "user", "guide", "admin"

        public string SoulRole { get; set; }  // e.g. "Soul Walker", "Mirror", "Keeper"

        public string SoulSignature { get; set; }  // Optional poetic alias

        public int? FrequencyLevel { get; set; }  // Optional symbolic resonance

        public DateTime? ActivatedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }

}
