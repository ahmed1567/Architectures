using BookStore.Domain;

namespace BookStore.Application.Interfaces;

public interface IBookRepository
{
    void Save(Book book);
    List<Book> GetAll();
}