using System.ComponentModel.DataAnnotations;

namespace qiapi.Models
{
    public class Signal
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public string Type { get; set; } // e.g. "Synchronicity", "Dream", "Message", "Body Sensation"

        public string Description { get; set; } // What the signal was, in words

        public string Channel { get; set; } // e.g. "Dream", "Intuition", "Conversation", "Nature"

        public string Resonance { get; set; } // e.g. "Curiosity", "Awe", "Fear", "Peace"

        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

        // Optional: Connect to Heartbeat it influenced
        public ICollection<Heartbeat> RelatedHeartbeats { get; set; }
    }

}
