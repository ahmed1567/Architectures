using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos;
using TaskManager.Application.UseCases;

namespace JobManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly CreateJobUseCase _createJobUseCase;
    private readonly GetAllJobsUseCase _getAllJobsUseCase;
    private readonly CompleteJobUseCase _completeJobUseCase;

    public JobController(
        CreateJobUseCase createJobUseCase,
        GetAllJobsUseCase getAllJobsUseCase,
        CompleteJobUseCase completeJobUseCase)
    {
        _createJobUseCase = createJobUseCase;
        _getAllJobsUseCase = getAllJobsUseCase;
        _completeJobUseCase = completeJobUseCase;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateJobRequest request)
    {
        var JobDto = _createJobUseCase.Execute(request.Title, request.Description);
        return Ok(JobDto);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var jobs = _getAllJobsUseCase.Execute();
        return Ok(jobs);
    }

    [HttpPost("{id}/complete")]
    public IActionResult Complete(Guid id)
    {
        var jobDto = _completeJobUseCase.Execute(id);
        return Ok(jobDto);
    }
}

public record CreateJobRequest(string Title, string? Description);