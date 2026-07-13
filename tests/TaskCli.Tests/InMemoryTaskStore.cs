using TaskCli.Model;
using TaskCli.Storage;

namespace TaskCli.Tests;

/// <summary>Test double so command tests never touch the filesystem.</summary>
internal sealed class InMemoryTaskStore : ITaskStore
{
    private List<TaskItem> _tasks;

    public InMemoryTaskStore(IEnumerable<TaskItem>? seed = null)
        => _tasks = seed?.ToList() ?? new List<TaskItem>();

    public IReadOnlyList<TaskItem> Load() => _tasks;

    public void Save(IReadOnlyList<TaskItem> tasks) => _tasks = tasks.ToList();
}
