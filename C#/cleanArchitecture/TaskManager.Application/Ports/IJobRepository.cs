using TaskManager.Domain;

namespace TaskManager.Application.Ports;

public interface IJobRepository
{
    Job Save(Job task);
    List<Job> GetAll();
    Job GetById(Guid id);
}