using Microsoft.EntityFrameworkCore;
using Zadanie7.Models;

namespace Zadanie7.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Trip> Trips { get; set; }
}