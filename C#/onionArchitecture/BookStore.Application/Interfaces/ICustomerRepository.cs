using BookStore.Domain;

namespace BookStore.Application.Interfaces;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<Customer> GetByIdAsync(Guid id);
}