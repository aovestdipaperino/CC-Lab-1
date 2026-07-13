using TaskCli.Cli;
using TaskCli.Storage;

namespace TaskCli.Commands;

public sealed class RemoveCommand : ICommand
{
    public int Execute(ParsedArgs args, ITaskStore store, TextWriter output, TextWriter error)
    {
        if (args.Positionals.Count == 0 || !int.TryParse(args.Positionals[0], out var id))
        {
            error.WriteLine("remove: a numeric task id is required");
            return 1;
        }

        var tasks = store.Load().ToList();
        var removed = tasks.RemoveAll(t => t.Id == id);
        if (removed == 0)
        {
            error.WriteLine($"remove: no task with id {id}");
            return 1;
        }

        store.Save(tasks);

        output.WriteLine($"Removed task #{id}");
        return 0;
    }
}
