using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Domain;

namespace BookStore.Application.Services;

public class PurchaseBookService
{
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseBookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDto> ExecuteAsync(Guid bookId, int quantity, Guid customerId)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Validate customer
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);

            // Check inventory (nested transaction)
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var inventory = await _unitOfWork.InventoryRepository.GetByBookIdAsync(bookId);
                if (inventory.Quantity < quantity)
                    throw new InvalidOperationException("Insufficient inventory.");

                var updatedInventory = new Inventory(bookId, inventory.Quantity - quantity);
                await _unitOfWork.InventoryRepository.AddAsync(updatedInventory);

                await _unitOfWork.CommitAsync(); // Commit nested transaction
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            // Create order
            var order = new Order(bookId, quantity, customerId);
            await _unitOfWork.OrderRepository.AddAsync(order);

            // Commit main transaction
            await _unitOfWork.CommitAsync();

            return new OrderDto(order.Id, order.BookId, order.Quantity, order.PurchaseDate, order.CustomerId);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}