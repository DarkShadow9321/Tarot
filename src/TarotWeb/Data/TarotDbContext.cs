using Microsoft.EntityFrameworkCore;
using TarotWeb.Models;
namespace TarotWeb.Data;
public sealed class TarotDbContext : DbContext
{
    public TarotDbContext(DbContextOptions<TarotDbContext> options) : base(options) { }
    public DbSet<TarotCard> TarotCards => Set<TarotCard>();
    public DbSet<TarotReading> TarotReadings => Set<TarotReading>();
    public DbSet<ReadingCard> ReadingCards => Set<ReadingCard>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TarotCard>().HasIndex(c => c.SortOrder).IsUnique();
        builder.Entity<ReadingCard>().HasIndex(c => new { c.TarotReadingId, c.Position }).IsUnique();
        builder.Entity<ReadingCard>().HasIndex(c => new { c.TarotReadingId, c.TarotCardId }).IsUnique();
        builder.Entity<ReadingCard>().HasOne(c => c.TarotReading).WithMany(r => r.Cards).HasForeignKey(c => c.TarotReadingId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ReadingCard>().HasOne(c => c.TarotCard).WithMany(r => r.ReadingCards).HasForeignKey(c => c.TarotCardId).OnDelete(DeleteBehavior.Restrict);
    }
}
