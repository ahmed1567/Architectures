namespace BookStore.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookRepository BookRepository { get; }
    IInventoryRepository InventoryRepository { get; }
    IOrderRepository OrderRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task<bool> ValidateAsync();
}