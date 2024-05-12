using Microsoft.EntityFrameworkCore;
using Zadanie7.Models;

namespace Zadanie7.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>()
            .HasKey(t => t.IdTrip);
    }

    public DbSet<Trip> Trip { get; set; }
}