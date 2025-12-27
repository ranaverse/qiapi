using System.ComponentModel.DataAnnotations;

namespace qiapi.Models
{
    public class Insight
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; } // Short phrase

        public string Content { get; set; } // Full reflection

        public int? Depth { get; set; } // Optional 1-10 or tier

        public string Resonance { get; set; } // e.g. "Empowerment", "Grief", "Liberation"

        public string Unlocked { get; set; } // e.g. "Voice", "Trust", "Root safety"

        public string Frequency { get; set; } // Symbolic or chakra-based

        public int? TriggeredBySignalId { get; set; }
        public Signal TriggeredBySignal { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
