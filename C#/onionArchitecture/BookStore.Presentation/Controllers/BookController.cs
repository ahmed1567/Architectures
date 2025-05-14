using Microsoft.AspNetCore.Mvc;
using BookStore.Application.Dtos;
using BookStore.Application.Services;

namespace BookStore.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly CreateBookService _createBookService;
    private readonly GetAllBooksService _getAllBooksService;
    private readonly PurchaseBookService _purchaseBookService;
    private readonly AddCustomerService _addCustomerService;

    public BookController(
        CreateBookService createBookService,
        GetAllBooksService getAllBooksService,
        PurchaseBookService purchaseBookService,
        AddCustomerService addCustomerService)
    {
        _createBookService = createBookService;
        _getAllBooksService = getAllBooksService;
        _purchaseBookService = purchaseBookService;
        _addCustomerService = addCustomerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
    {
        var bookDto = await _createBookService.ExecuteAsync(request.Title, request.Author, request.Price);
        return Ok(bookDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _getAllBooksService.ExecuteAsync();
        return Ok(books);
    }

    [HttpPost("{id}/discount")]
    public async Task<IActionResult> ApplyDiscount(Guid id, [FromBody] DiscountRequest request)
    {
        var bookDto = await _createBookService.ApplyDiscountAsync(id, request.DiscountPercentage);
        return Ok(bookDto);
    }

    [HttpPost("{id}/purchase")]
    public async Task<IActionResult> Purchase(Guid id, [FromBody] PurchaseRequest request)
    {
        var orderDto = await _purchaseBookService.ExecuteAsync(id, request.Quantity, request.CustomerId);
        return Ok(orderDto);
    }

    [HttpPost("customer")]
    public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequest request)
    {
        var customerDto = await _addCustomerService.ExecuteAsync(request.Name, request.Email);
        return Ok(customerDto);
    }
}

public record CreateBookRequest(string Title, string Author, decimal Price);
public record DiscountRequest(decimal DiscountPercentage);
public record PurchaseRequest(int Quantity, Guid CustomerId);
public record AddCustomerRequest(string Name, string Email);