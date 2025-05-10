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

    public BookController(CreateBookService createBookService, GetAllBooksService getAllBooksService)
    {
        _createBookService = createBookService;
        _getAllBooksService = getAllBooksService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateBookRequest request)
    {
        var bookDto = _createBookService.Execute(request.Title, request.Author, request.Price);
        return Ok(bookDto);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var books = _getAllBooksService.Execute();
        return Ok(books);
    }
}

public record CreateBookRequest(string Title, string Author, decimal Price);