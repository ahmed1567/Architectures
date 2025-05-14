using BookStore.Application.Interfaces;
using BookStore.Domain;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<List<Order>> GetByBookIdAsync(Guid bookId)
    {
        return await _context.Orders.Where(o => o.BookId == bookId).ToListAsync();
    }
}