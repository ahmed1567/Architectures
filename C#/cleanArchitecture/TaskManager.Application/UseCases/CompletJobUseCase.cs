using TaskManager.Application.Dtos;
using TaskManager.Application.Ports;
using TaskManager.Domain;

namespace TaskManager.Application.UseCases;

public class CompleteJobUseCase
{
    private readonly IJobRepository _repository;

    public CompleteJobUseCase(IJobRepository repository)
    {
        _repository = repository;
    }

    public JobDto Execute(Guid id)
    {
        var job = _repository.GetById(id);
        job.Complete();
        _repository.Save(job);
        return new JobDto(job.Id, job.Title, job.Description, job.IsCompleted, job.CreatedAt);
    }
}