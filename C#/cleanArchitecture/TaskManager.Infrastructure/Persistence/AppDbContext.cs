using Microsoft.EntityFrameworkCore;
using TaskManager.Domain;

namespace TaskManager.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Job> Jobs { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}