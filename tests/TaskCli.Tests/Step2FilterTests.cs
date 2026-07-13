using TaskCli.Cli;
using TaskCli.Commands;
using TaskCli.Model;
using Xunit;

namespace TaskCli.Tests;

// STEP 2 — `list --tag` and `list --priority`, composable, with a friendly
// error for an unknown priority.
[Trait("step", "2")]
public class Step2FilterTests
{
    private static InMemoryTaskStore Seed() => new(new[]
    {
        new TaskItem { Id = 1, Title = "home-low", Priority = Priority.Low, State = TaskState.Open, Tag = "home" },
        new TaskItem { Id = 2, Title = "work-high", Priority = Priority.High, State = TaskState.Open, Tag = "work" },
        new TaskItem { Id = 3, Title = "work-med", Priority = Priority.Medium, State = TaskState.Open, Tag = "work" },
    });

    private static string List(params string[] args)
    {
        var writer = new StringWriter();
        new ListCommand().Execute(ArgParser.Parse(args), Seed(), writer, TextWriter.Null);
        return writer.ToString();
    }

    [Fact]
    public void List_Tag_ShowsOnlyThatTag()
    {
        var text = List("list", "--status", "all", "--tag", "work");
        Assert.Contains("work-high", text);
        Assert.Contains("work-med", text);
        Assert.DoesNotContain("home-low", text);
    }

    [Fact]
    public void List_Priority_ShowsOnlyThatPriority()
    {
        var text = List("list", "--status", "all", "--priority", "high");
        Assert.Contains("work-high", text);
        Assert.DoesNotContain("home-low", text);
        Assert.DoesNotContain("work-med", text);
    }

    [Fact]
    public void List_TagAndPriority_Compose()
    {
        var text = List("list", "--status", "all", "--tag", "work", "--priority", "medium");
        Assert.Contains("work-med", text);
        Assert.DoesNotContain("work-high", text);
    }

    [Fact]
    public void List_UnknownPriority_ReturnsExitCodeOne()
    {
        var code = new ListCommand().Execute(
            ArgParser.Parse(new[] { "list", "--priority", "urgent" }), Seed(), TextWriter.Null, TextWriter.Null);
        Assert.Equal(1, code);
    }
}
