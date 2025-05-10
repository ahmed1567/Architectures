using BookStore.Application.Interfaces;
using BookStore.Domain;
using BookStore.Infrastructure.Persistence;

namespace BookStore.Infrastructure.Persistence;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Save(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public List<Book> GetAll()
    {
        return _context.Books.ToList();
    }
}