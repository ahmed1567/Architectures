using Microsoft.EntityFrameworkCore;
using BookStore.Domain;

namespace BookStore.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure concurrency tokens
        modelBuilder.Entity<Book>()
            .Property(b => b.RowVersion)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();

        modelBuilder.Entity<Inventory>()
            .Property(i => i.RowVersion)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();

        modelBuilder.Entity<Order>()
            .Property(o => o.RowVersion)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();

        modelBuilder.Entity<Customer>()
            .Property(c => c.RowVersion)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();
    }
}