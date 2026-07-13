using TaskCli.Cli;
using TaskCli.Commands;
using TaskCli.Model;
using Xunit;

namespace TaskCli.Tests;

public class DoneRemoveCommandTests
{
    [Fact]
    public void Done_MarksTaskDone()
    {
        var store = new InMemoryTaskStore(new[]
        {
            new TaskItem { Id = 1, Title = "x", State = TaskState.Open },
        });

        var code = new DoneCommand().Execute(
            ArgParser.Parse(new[] { "done", "1" }), store, TextWriter.Null, TextWriter.Null);

        Assert.Equal(0, code);
        Assert.Equal(TaskState.Done, store.Load().Single().State);
    }

    [Fact]
    public void Remove_DeletesTask()
    {
        var store = new InMemoryTaskStore(new[]
        {
            new TaskItem { Id = 1, Title = "x" },
        });

        var code = new RemoveCommand().Execute(
            ArgParser.Parse(new[] { "remove", "1" }), store, TextWriter.Null, TextWriter.Null);

        Assert.Equal(0, code);
        Assert.Empty(store.Load());
    }

    [Fact]
    public void Done_UnknownId_ReturnsError()
    {
        var store = new InMemoryTaskStore();

        var code = new DoneCommand().Execute(
            ArgParser.Parse(new[] { "done", "99" }), store, TextWriter.Null, TextWriter.Null);

        Assert.Equal(1, code);
    }
}
