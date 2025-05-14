using BookStore.Application.Interfaces;
using BookStore.Domain;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        var book = await _context.Books.FindAsync(id);
        return book ?? throw new InvalidOperationException("Book not found.");
    }
}