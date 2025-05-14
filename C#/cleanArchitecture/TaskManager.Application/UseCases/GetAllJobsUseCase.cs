using TaskManager.Application.Dtos;
using TaskManager.Application.Ports;
using TaskManager.Domain;

namespace TaskManager.Application.UseCases;

public class GetAllJobsUseCase
{
    private readonly IJobRepository _repository;

    public GetAllJobsUseCase(IJobRepository repository)
    {
        _repository = repository;
    }



        public List<JobDto> Execute()
    {
        var books = _repository.GetAll();
        return books.Select(j => new JobDto(j.Id, j.Title, j.Description, j.IsCompleted,j.CreatedAt)).ToList();
    }
}