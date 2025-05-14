using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;

namespace BookStore.Application.Services;

public class GetAllBooksService
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBooksService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<BookWithInventoryDto>> ExecuteAsync()
    {
        var books = await _unitOfWork.BookRepository.GetAllAsync();
        var result = new List<BookWithInventoryDto>();

        foreach (var book in books)
        {
            var inventory = await _unitOfWork.InventoryRepository.GetByBookIdAsync(book.Id);
            result.Add(new BookWithInventoryDto(book.Id, book.Title, book.Author, book.Price, inventory.Quantity));
        }

        return result;
    }
}