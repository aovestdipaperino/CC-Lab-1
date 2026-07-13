using TaskCli.Cli;
using TaskCli.Commands;
using TaskCli.Model;
using Xunit;

namespace TaskCli.Tests;

public class ListCommandTests
{
    private static InMemoryTaskStore Seed() => new(new[]
    {
        new TaskItem { Id = 1, Title = "open-low", Priority = Priority.Low, State = TaskState.Open, Tag = "home" },
        new TaskItem { Id = 2, Title = "done-high", Priority = Priority.High, State = TaskState.Done, Tag = "work" },
        new TaskItem { Id = 3, Title = "open-high", Priority = Priority.High, State = TaskState.Open, Tag = "work" },
    });

    [Fact]
    public void List_All_ShowsEveryTask()
    {
        var writer = new StringWriter();
        new ListCommand().Execute(
            ArgParser.Parse(new[] { "list", "--status", "all" }), Seed(), writer, TextWriter.Null);

        var text = writer.ToString();
        Assert.Contains("open-low", text);
        Assert.Contains("done-high", text);
        Assert.Contains("open-high", text);
    }

    [Fact]
    public void List_StatusOpen_HidesDone()
    {
        var writer = new StringWriter();
        new ListCommand().Execute(
            ArgParser.Parse(new[] { "list", "--status", "open" }), Seed(), writer, TextWriter.Null);

        var text = writer.ToString();
        Assert.Contains("open-low", text);
        Assert.DoesNotContain("done-high", text);
    }
}
