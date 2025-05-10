using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos;
using TaskManager.Application.UseCases;

namespace JobManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly CreateJobUseCase _createJobUseCase;

    public JobController(CreateJobUseCase createJobUseCase)
    {
        _createJobUseCase = createJobUseCase;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateJobRequest request)
    {
        var JobDto = _createJobUseCase.Execute(request.Title, request.Description);
        return Ok(JobDto);
    }
}

public record CreateJobRequest(string Title, string? Description);