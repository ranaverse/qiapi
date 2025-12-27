using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using qiapi.Models;

namespace QI.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Heartbeat> Heartbeats { get; set; }
        public DbSet<Signal> Signals { get; set; }
        public DbSet<Insight> Insights { get; set; }
        public DbSet<Whisper> Whispers { get; set; }
        public DbSet<Mirror> Mirrors { get; set; }
        public DbSet<AuthCredential> AuthCredential { get; set; }

    }
}
