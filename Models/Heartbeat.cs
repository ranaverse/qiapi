using System.ComponentModel.DataAnnotations;

namespace qiapi.Models
{
    public class Heartbeat
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string Emotion { get; set; }  // e.g. "Joy", "Sadness", "Calm"

        public int? Intensity { get; set; }  // Optional: 1–10 scale?

        public string Notes { get; set; }  // Optional space to elaborate

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional link to a signal that triggered it
        public int? SignalId { get; set; }
        public Signal Signal { get; set; }
    }

}
