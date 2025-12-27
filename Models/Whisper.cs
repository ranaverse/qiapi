using System.ComponentModel.DataAnnotations;

namespace qiapi.Models
{
    public class Whisper
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public string Content { get; set; } // The received whisper

        public string Tone { get; set; } // e.g. "Soft", "Urgent", "Cryptic"

        public string Channel { get; set; } // e.g. "Dream", "Intuition", "Music", "Meditation"

        public string FeltResonance { get; set; } // Emotional echo

        public string ClarityLevel { get; set; } // e.g. "Fuzzy", "Clear", "Crystal"

        public string TrustLevel { get; set; } // e.g. "Medium", "High", "Uncertain"

        public string NoticedBecause { get; set; } // Why it stood out

        public string PointingTo { get; set; } // The direction or theme it gestured toward

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? LinkedSignalId { get; set; }
        public Signal LinkedSignal { get; set; }

        public int? LinkedHeartbeatId { get; set; }
        public Heartbeat LinkedHeartbeat { get; set; }

        public int? ResultedInsightId { get; set; }
        public Insight ResultedInsight { get; set; }

    }

}
