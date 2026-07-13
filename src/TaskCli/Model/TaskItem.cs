namespace TaskCli.Model;

public sealed class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public Priority Priority { get; set; } = Priority.Medium;

    public string? Tag { get; set; }

    public TaskState State { get; set; } = TaskState.Open;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
