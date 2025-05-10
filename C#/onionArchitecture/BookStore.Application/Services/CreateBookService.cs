using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Domain;

namespace BookStore.Application.Services;

public class CreateBookService
{
    private readonly IBookRepository _repository;

    public CreateBookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public BookDto Execute(string title, string author, decimal price)
    {
        var book = new Book(title, author, price);
        _repository.Save(book);
        return new BookDto(book.Id, book.Title, book.Author, book.Price);
    }
}