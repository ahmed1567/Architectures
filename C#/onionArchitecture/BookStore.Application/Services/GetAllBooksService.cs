using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;

namespace BookStore.Application.Services;

public class GetAllBooksService
{
    private readonly IBookRepository _repository;

    public GetAllBooksService(IBookRepository repository)
    {
        _repository = repository;
    }

    public List<BookDto> Execute()
    {
        var books = _repository.GetAll();
        return books.Select(b => new BookDto(b.Id, b.Title, b.Author, b.Price)).ToList();
    }
}