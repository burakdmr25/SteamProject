using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BurakSteam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BurakSteam.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet'lerimizi ekliyoruz
        public DbSet<Game> Games { get; set; }
        public DbSet<GameDetails> GameDetails { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; } // ContactForm DbSet'i eklendi

        // Entity ilişkilerini yapılandırma
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Game ve GameDetails arasındaki birebir ilişkiyi tanımlıyoruz
            modelBuilder.Entity<Game>()
                .HasOne(g => g.GameDetails)
                .WithOne(gd => gd.Game)
                .HasForeignKey<GameDetails>(gd => gd.GameId)
                .OnDelete(DeleteBehavior.Cascade); // Game silindiğinde, ilgili GameDetails silinsin
        }

        // Lazy loading'i etkinleştiriyoruz
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies(); // Lazy loading proxy desteği
            }
        }
    }

    // Kullanıcı sınıfını burada tanımladık
    public class ApplicationUser : IdentityUser
    {
        // Purchases koleksiyonunu virtual olarak işaretledik
        public virtual ICollection<Purchase>? Purchases { get; set; }
    }
}
