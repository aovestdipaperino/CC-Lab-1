using TaskCli.Cli;
using TaskCli.Commands;
using TaskCli.Model;
using Xunit;

namespace TaskCli.Tests;

public class AddCommandTests
{
    [Fact]
    public void Add_AssignsIncrementingIds()
    {
        var store = new InMemoryTaskStore();
        var command = new AddCommand();

        command.Execute(ArgParser.Parse(new[] { "add", "one" }), store, TextWriter.Null, TextWriter.Null);
        command.Execute(ArgParser.Parse(new[] { "add", "two" }), store, TextWriter.Null, TextWriter.Null);

        var tasks = store.Load();
        Assert.Equal(2, tasks.Count);
        Assert.Equal(1, tasks[0].Id);
        Assert.Equal(2, tasks[1].Id);
    }

    [Fact]
    public void Add_ParsesPriorityAndTag()
    {
        var store = new InMemoryTaskStore();

        new AddCommand().Execute(
            ArgParser.Parse(new[] { "add", "ship", "it", "--priority", "high", "--tag", "release" }),
            store,
            TextWriter.Null,
            TextWriter.Null);

        var task = store.Load().Single();
        Assert.Equal("ship it", task.Title);
        Assert.Equal(Priority.High, task.Priority);
        Assert.Equal("release", task.Tag);
    }

    [Fact]
    public void Add_MissingTitle_ReturnsError()
    {
        var store = new InMemoryTaskStore();

        var code = new AddCommand().Execute(
            ArgParser.Parse(new[] { "add" }), store, TextWriter.Null, TextWriter.Null);

        Assert.Equal(1, code);
        Assert.Empty(store.Load());
    }
}
