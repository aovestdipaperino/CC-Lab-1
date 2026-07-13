using System.Text.Json;
using TaskCli.Cli;
using TaskCli.Commands;
using TaskCli.Model;
using Xunit;

namespace TaskCli.Tests;

// STEP 1 — `list --json` prints a JSON array instead of the table.
[Trait("step", "1")]
public class Step1JsonTests
{
    private static InMemoryTaskStore Seed() => new(new[]
    {
        new TaskItem { Id = 1, Title = "open-low", Priority = Priority.Low, State = TaskState.Open, Tag = "home" },
        new TaskItem { Id = 2, Title = "done-high", Priority = Priority.High, State = TaskState.Done, Tag = "work" },
        new TaskItem { Id = 3, Title = "open-high", Priority = Priority.High, State = TaskState.Open, Tag = "work" },
    });

    [Fact]
    public void List_Json_EmitsAJsonArray()
    {
        var writer = new StringWriter();
        var code = new ListCommand().Execute(
            ArgParser.Parse(new[] { "list", "--status", "all", "--json" }), Seed(), writer, TextWriter.Null);

        Assert.Equal(0, code);
        using var doc = JsonDocument.Parse(writer.ToString());
        Assert.Equal(JsonValueKind.Array, doc.RootElement.ValueKind);
        Assert.Equal(3, doc.RootElement.GetArrayLength());
    }

    [Fact]
    public void List_Json_ComposesWithStatusFilter()
    {
        var writer = new StringWriter();
        new ListCommand().Execute(
            ArgParser.Parse(new[] { "list", "--status", "open", "--json" }), Seed(), writer, TextWriter.Null);

        using var doc = JsonDocument.Parse(writer.ToString());
        Assert.Equal(2, doc.RootElement.GetArrayLength());
    }
}
