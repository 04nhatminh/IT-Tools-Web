using Microsoft.EntityFrameworkCore;
using IT_Tools_Web.Models;

namespace IT_Tools_Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.AccountId, f.ToolId })
                .IsUnique();

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Account)
                .WithMany(a => a.Favorites)
                .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Tool)
                .WithMany(t => t.Favorites)
                .HasForeignKey(f => f.ToolId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
