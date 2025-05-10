using TaskManager.Application.Ports;
using TaskManager.Domain;

namespace TaskManager.Infrastructure.Persistence;

public class JobRepository : IJobRepository
{
    private readonly AppDbContext _context;

    public JobRepository(AppDbContext context)
    {
        _context = context;
    }

    public Job Save(Job task)
    {
        _context.Jobs.Add(task);
        _context.SaveChanges();
        return task;
    }
}