using TaskCli.Commands;
using TaskCli.Storage;

namespace TaskCli.Cli;

public sealed class CommandRouter
{
    public int Route(ParsedArgs args, ITaskStore store, TextWriter output, TextWriter error)
    {
        ICommand? command = args.Command?.ToLowerInvariant() switch
        {
            "add" => new AddCommand(),
            "list" => new ListCommand(),
            "done" => new DoneCommand(),
            "remove" or "rm" => new RemoveCommand(),
            _ => null,
        };

        if (command is null)
        {
            PrintUsage(error);
            return args.Command is null ? 0 : 1;
        }

        return command.Execute(args, store, output, error);
    }

    private static void PrintUsage(TextWriter error)
    {
        error.WriteLine("Usage: tasks <add|list|done|remove> [options]");
        error.WriteLine("  add <title> [--priority low|med|high] [--tag <tag>]");
        error.WriteLine("  list [--status open|done|all]");
        error.WriteLine("  done <id>");
        error.WriteLine("  remove <id>");
    }
}
