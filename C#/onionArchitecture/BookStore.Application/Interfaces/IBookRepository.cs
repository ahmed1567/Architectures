using BookStore.Domain;

namespace BookStore.Application.Interfaces;

public interface IBookRepository
{
    Task AddAsync(Book book);
    Task<List<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(Guid id);
}
