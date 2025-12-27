using System.ComponentModel.DataAnnotations;

namespace qiapi.Models
{
    public class Mirror
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public int? ReflectedFromUserId { get; set; }
        public User ReflectedFromUser { get; set; }

        public string Content { get; set; }

        public int? LinkedInsightId { get; set; }
        public Insight LinkedInsight { get; set; }

        public int? LinkedHeartbeatId { get; set; }
        public Heartbeat LinkedHeartbeat { get; set; }

        public int? LinkedWhisperId { get; set; }
        public Whisper LinkedWhisper { get; set; }

        public string TruthResonanceScore { get; set; } // e.g. "Shallow", "Medium", "Deep", "Piercing"

        public string MirrorType { get; set; } // e.g. "Oracle", "Guide", "Self", "Symbolic"

        public string Transparency { get; set; } // e.g. "Foggy", "Clear", "Crystal"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
