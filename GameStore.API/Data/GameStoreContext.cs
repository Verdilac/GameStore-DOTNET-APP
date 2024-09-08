using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .Property(g => g.Price)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1,Name = "Fighting"},
            new {Id = 2,Name = "RPG"},
            new {Id = 3,Name = "FPS"},
            new {Id = 4,Name = "Racing"}
        );

    }
}