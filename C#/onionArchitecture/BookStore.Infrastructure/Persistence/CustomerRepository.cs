using BookStore.Application.Interfaces;
using BookStore.Domain;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    public async Task<Customer> GetByIdAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        return customer ?? throw new InvalidOperationException("Customer not found.");
    }
}