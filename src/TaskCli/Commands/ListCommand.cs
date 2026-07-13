using TaskCli.Cli;
using TaskCli.Model;
using TaskCli.Output;
using TaskCli.Storage;

namespace TaskCli.Commands;

public sealed class ListCommand : ICommand
{
    public int Execute(ParsedArgs args, ITaskStore store, TextWriter output, TextWriter error)
    {
        IEnumerable<TaskItem> tasks = store.Load();

        var status = args.GetOption("status");
        if (!string.IsNullOrEmpty(status) && !status.Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            if (status.Equals("open", StringComparison.OrdinalIgnoreCase))
            {
                tasks = tasks.Where(t => t.State == TaskState.Open);
            }
            else if (status.Equals("done", StringComparison.OrdinalIgnoreCase))
            {
                tasks = tasks.Where(t => t.State == TaskState.Done);
            }
            else
            {
                error.WriteLine($"list: unknown status '{status}'");
                return 1;
            }
        }

        var result = tasks.OrderBy(t => t.Id).ToList();
        output.Write(TaskTableFormatter.Format(result));
        return 0;
    }
}
