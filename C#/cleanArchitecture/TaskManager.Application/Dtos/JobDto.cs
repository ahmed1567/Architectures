namespace TaskManager.Application.Dtos;

public record JobDto(Guid Id, string Title, string? Description, bool IsCompleted, DateTime CreatedAt);