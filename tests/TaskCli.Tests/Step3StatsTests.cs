using TaskCli.Cli;
using TaskCli.Model;
using TaskCli.Storage;
using Xunit;

namespace TaskCli.Tests;

// STEP 3 — a `stats` command, registered in CommandRouter, printing counts by
// state and by priority. Exercised through the router (by command name) so the
// test does not depend on the concrete command type.
[Trait("step", "3")]
public class Step3StatsTests
{
    private static ITaskStore Seed() => new InMemoryTaskStore(new[]
    {
        new TaskItem { Id = 1, Title = "a", Priority = Priority.Low, State = TaskState.Open },
        new TaskItem { Id = 2, Title = "b", Priority = Priority.High, State = TaskState.Done },
        new TaskItem { Id = 3, Title = "c", Priority = Priority.High, State = TaskState.Open },
    });

    [Fact]
    public void Stats_ExitsZeroAndReportsCounts()
    {
        var writer = new StringWriter();
        var code = new CommandRouter().Route(
            ArgParser.Parse(new[] { "stats" }), Seed(), writer, TextWriter.Null);

        Assert.Equal(0, code);
        var text = writer.ToString().ToLowerInvariant();
        Assert.Contains("open", text);
        Assert.Contains("done", text);
        // 2 open, 1 done in the seed.
        Assert.Contains("2", text);
        Assert.Contains("1", text);
    }
}
