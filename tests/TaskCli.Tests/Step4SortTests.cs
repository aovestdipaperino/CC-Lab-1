using TaskCli.Cli;
using TaskCli.Commands;
using TaskCli.Model;
using Xunit;

namespace TaskCli.Tests;

// STEP 4 — `list --sort priority` orders the output high → medium → low
// (ties keep ascending id order).
[Trait("step", "4")]
public class Step4SortTests
{
    private static InMemoryTaskStore Seed() => new(new[]
    {
        new TaskItem { Id = 1, Title = "task-low", Priority = Priority.Low, State = TaskState.Open },
        new TaskItem { Id = 2, Title = "task-high", Priority = Priority.High, State = TaskState.Open },
        new TaskItem { Id = 3, Title = "task-medium", Priority = Priority.Medium, State = TaskState.Open },
    });

    [Fact]
    public void List_SortPriority_OrdersHighestFirst()
    {
        var writer = new StringWriter();
        new ListCommand().Execute(
            ArgParser.Parse(new[] { "list", "--status", "all", "--sort", "priority" }),
            Seed(), writer, TextWriter.Null);

        var text = writer.ToString();
        var high = text.IndexOf("task-high", StringComparison.Ordinal);
        var medium = text.IndexOf("task-medium", StringComparison.Ordinal);
        var low = text.IndexOf("task-low", StringComparison.Ordinal);

        Assert.True(high >= 0 && medium >= 0 && low >= 0, "all tasks should be listed");
        Assert.True(high < medium, "high should come before medium");
        Assert.True(medium < low, "medium should come before low");
    }
}
