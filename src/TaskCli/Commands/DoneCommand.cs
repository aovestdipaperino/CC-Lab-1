using TaskCli.Cli;
using TaskCli.Model;
using TaskCli.Storage;

namespace TaskCli.Commands;

public sealed class DoneCommand : ICommand
{
    public int Execute(ParsedArgs args, ITaskStore store, TextWriter output, TextWriter error)
    {
        if (args.Positionals.Count == 0 || !int.TryParse(args.Positionals[0], out var id))
        {
            error.WriteLine("done: a numeric task id is required");
            return 1;
        }

        var tasks = store.Load().ToList();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
        {
            error.WriteLine($"done: no task with id {id}");
            return 1;
        }

        task.State = TaskState.Done;
        store.Save(tasks);

        output.WriteLine($"Completed task #{id}");
        return 0;
    }
}
