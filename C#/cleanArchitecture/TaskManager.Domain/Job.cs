namespace TaskManager.Domain;

public class Job
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Job(string title, string? description)
    {
        Id = new Guid();
        Title = title;
        Description = description;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
        Validate();
    }

    // Business rule: Title cannot be empty
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            throw new ArgumentException("Job title cannot be empty.");
        }
    }

        public void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Job is already completed.");
        IsCompleted = true;
    }
}