using TaskCli.Cli;
using TaskCli.Storage;

namespace TaskCli.Commands;

/// <summary>
/// A CLI verb. Commands never touch Console directly: they write to the supplied
/// TextWriters so tests can capture output, and return a process exit code
/// (0 = success, 1 = user error).
/// </summary>
public interface ICommand
{
    int Execute(ParsedArgs args, ITaskStore store, TextWriter output, TextWriter error);
}
