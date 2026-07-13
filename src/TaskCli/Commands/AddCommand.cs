using TaskCli.Cli;
using TaskCli.Model;
using TaskCli.Storage;

namespace TaskCli.Commands;

public sealed class AddCommand : ICommand
{
    public int Execute(ParsedArgs args, ITaskStore store, TextWriter output, TextWriter error)
    {
        if (args.Positionals.Count == 0)
        {
            error.WriteLine("add: a task title is required");
            return 1;
        }

        var title = string.Join(' ', args.Positionals);
        var tasks = store.Load().ToList();
        var nextId = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1;

        var item = new TaskItem
        {
            Id = nextId,
            Title = title,
            Priority = ParsePriority(args.GetOption("priority")),
            Tag = args.GetOption("tag"),
            State = TaskState.Open,
        };

        tasks.Add(item);
        store.Save(tasks);

        output.WriteLine($"Added task #{item.Id}: {item.Title}");
        return 0;
    }

    private static Priority ParsePriority(string? value) => value?.ToLowerInvariant() switch
    {
        "low" => Priority.Low,
        "high" => Priority.High,
        _ => Priority.Medium,
    };
}
