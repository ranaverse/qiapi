using System.ComponentModel.DataAnnotations;

namespace qiapi.Models
{
    public class AuthCredential
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key to User
        public string CredentialId { get; set; } // Base64 or byte[] encoded ID
        public string PublicKey { get; set; }
        public uint SignatureCounter { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
