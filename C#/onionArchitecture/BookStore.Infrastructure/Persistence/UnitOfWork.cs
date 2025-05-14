using BookStore.Application.Interfaces;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly Dictionary<Type, object> _repositories;
    private int _transactionCount; // For nested transactions

    public IBookRepository BookRepository { get; }
    public IInventoryRepository InventoryRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public ICustomerRepository CustomerRepository { get; }

    public UnitOfWork(
        AppDbContext context,
        IBookRepository bookRepository,
        IInventoryRepository inventoryRepository,
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
        _transactionCount = 0;

        BookRepository = bookRepository;
        InventoryRepository = inventoryRepository;
        OrderRepository = orderRepository;
        CustomerRepository = customerRepository;

        // Register repositories for factory access
        _repositories[typeof(IBookRepository)] = bookRepository;
        _repositories[typeof(IInventoryRepository)] = inventoryRepository;
        _repositories[typeof(IOrderRepository)] = orderRepository;
        _repositories[typeof(ICustomerRepository)] = customerRepository;
    }

    public async Task BeginTransactionAsync()
    {
        if (_transactionCount == 0)
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            Console.WriteLine("Transaction started.");
        }
        _transactionCount++;
    }

    public async Task CommitAsync()
    {
        try
        {
            if (_transactionCount <= 0)
                throw new InvalidOperationException("No transaction to commit.");

            _transactionCount--;

            if (_transactionCount == 0)
            {
                if (!await ValidateAsync())
                    throw new InvalidOperationException("Validation failed before commit.");

                await _context.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    Console.WriteLine("Transaction committed.");
                    _transaction = null;
                }
            }
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine($"Concurrency error: {ex.Message}");
            await RollbackAsync();
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Commit failed: {ex.Message}");
            await RollbackAsync();
            throw;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transactionCount <= 0)
            return;

        _transactionCount--;

        if (_transactionCount == 0 && _transaction != null)
        {
            await _transaction.RollbackAsync();
            Console.WriteLine("Transaction rolled back.");
            _transaction = null;
        }

    }

    public async Task<bool> ValidateAsync()
    {
        // Example validation: Check if there are any pending changes
        var changes = _context.ChangeTracker.Entries()
            .Any(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

        if (!changes)
        {
            Console.WriteLine("Validation warning: No changes to commit.");
            return false;
        }

        // Add more validation logic as needed (e.g., business rules)
        return true;
    }

    public T GetRepository<T>() where T : class
    {
        if (_repositories.TryGetValue(typeof(T), out var repository))
        {
            return (T)repository;
        }
        throw new InvalidOperationException($"Repository {typeof(T).Name} not found.");
    }

    public void Dispose()
    {
        if (_transactionCount > 0)
        {
            Console.WriteLine("Warning: Disposing UnitOfWork with active transactions.");
            _transaction?.Rollback();
        }
        _transaction?.Dispose();
        _context.Dispose();
    }
}