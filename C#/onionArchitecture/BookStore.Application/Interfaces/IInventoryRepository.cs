using BookStore.Domain;

namespace BookStore.Application.Interfaces;

public interface IInventoryRepository
{
    Task AddAsync(Inventory inventory);
    Task<Inventory> GetByBookIdAsync(Guid bookId);
}