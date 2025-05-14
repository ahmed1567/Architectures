using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Domain;

namespace BookStore.Application.Services;

public class CreateBookService
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BookDto> ExecuteAsync(string title, string author, decimal price)
    {
        var book = new Book(title, author, price);
        await _unitOfWork.BookRepository.AddAsync(book);
        
        var inventory = new Inventory(book.Id, 10); // Initial stock of 10
        await _unitOfWork.InventoryRepository.AddAsync(inventory);
        
        await _unitOfWork.CommitAsync();
        return new BookDto(book.Id, book.Title, book.Author, book.Price);
    }

    public async Task<BookDto> ApplyDiscountAsync(Guid id, decimal discountPercentage)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        book.ApplyDiscount(discountPercentage);
        await _unitOfWork.CommitAsync();
        return new BookDto(book.Id, book.Title, book.Author, book.Price);
    }
}