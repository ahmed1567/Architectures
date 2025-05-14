using BookStore.Application.Interfaces;
using BookStore.Domain;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence;

public class InventoryRepository : IInventoryRepository
{
    private readonly AppDbContext _context;

    public InventoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Inventory inventory)
    {
        await _context.Inventories.AddAsync(inventory);
    }

    public async Task<Inventory> GetByBookIdAsync(Guid bookId)
    {
        var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.BookId == bookId);
        return inventory ?? throw new InvalidOperationException("Inventory not found for book.");
    }
}