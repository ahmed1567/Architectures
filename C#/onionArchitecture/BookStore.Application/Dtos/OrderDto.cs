namespace BookStore.Application.Dtos;

public record OrderDto(Guid Id, Guid BookId, int Quantity, DateTime PurchaseDate,Guid customerId);