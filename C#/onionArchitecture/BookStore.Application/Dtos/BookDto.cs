namespace BookStore.Application.Dtos;

public record BookDto(Guid Id, string Title, string Author, decimal Price);