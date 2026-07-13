using TaskCli.Model;

namespace TaskCli.Storage;

public interface ITaskStore
{
    IReadOnlyList<TaskItem> Load();

    void Save(IReadOnlyList<TaskItem> tasks);
}
