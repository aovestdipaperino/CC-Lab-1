using TaskCli.Cli;
using TaskCli.Commands;
using TaskCli.Storage;
using Xunit;

namespace TaskCli.Tests;

// STEP 5 — due dates. `add --due <date>` records a due date; `list --due-before
// <date>` shows only tasks due strictly before that date. This one touches the
// model, the parser usage, the add and list commands, and JSON persistence.
[Trait("step", "5")]
public class Step5DueDateTests
{
    private static void Add(ITaskStore store, string title, string due)
    {
        new AddCommand().Execute(
            ArgParser.Parse(new[] { "add", title, "--due", due }), store, TextWriter.Null, TextWriter.Null);
    }

    [Fact]
    public void List_DueBefore_FiltersByDueDate()
    {
        ITaskStore store = new InMemoryTaskStore();
        Add(store, "early", "2026-01-01");
        Add(store, "late", "2026-12-01");

        var writer = new StringWriter();
        new ListCommand().Execute(
            ArgParser.Parse(new[] { "list", "--status", "all", "--due-before", "2026-06-01" }),
            store, writer, TextWriter.Null);

        var text = writer.ToString();
        Assert.Contains("early", text);
        Assert.DoesNotContain("late", text);
    }
}
