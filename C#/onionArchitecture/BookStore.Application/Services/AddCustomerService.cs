using BookStore.Application.Dtos;
using BookStore.Application.Interfaces;
using BookStore.Domain;

namespace BookStore.Application.Services;

public class AddCustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public AddCustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerDto> ExecuteAsync(string name, string email)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var customer = new Customer(name, email);
            await _unitOfWork.CustomerRepository.AddAsync(customer);

            await _unitOfWork.CommitAsync();
            return new CustomerDto(customer.Id, customer.Name, customer.Email);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}