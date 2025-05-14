namespace BookStore.Application.Dtos;

public record BookWithInventoryDto(Guid Id, string Title, string Author, decimal Price, int Quantity);