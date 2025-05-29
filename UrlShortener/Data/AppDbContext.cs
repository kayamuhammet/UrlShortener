using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UrlModel> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique ShortCode
            modelBuilder.Entity<UrlModel>()
                .HasIndex(u => u.ShortCode)
                .IsUnique();
        }
    }   
}