using TaskManager.Application.Dtos;
using TaskManager.Application.Ports;
using TaskManager.Domain;

namespace TaskManager.Application.UseCases;

public class CreateJobUseCase
{
    private readonly IJobRepository _repository;

    public CreateJobUseCase(IJobRepository repository)
    {
        _repository = repository;
    }

    public JobDto Execute(string title, string? description)
    {
        var task = new Job(title, description);
        _repository.Save(task);
        return new JobDto(task.Id, task.Title, task.Description, task.IsCompleted, task.CreatedAt);
    }
}