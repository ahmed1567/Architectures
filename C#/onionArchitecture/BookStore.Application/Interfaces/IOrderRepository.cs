using BookStore.Domain;

namespace BookStore.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<List<Order>> GetByBookIdAsync(Guid bookId);
}