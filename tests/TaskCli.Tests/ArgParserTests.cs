using TaskCli.Cli;
using Xunit;

namespace TaskCli.Tests;

public class ArgParserTests
{
    [Fact]
    public void Parse_ExtractsCommand()
    {
        var parsed = ArgParser.Parse(new[] { "list" });
        Assert.Equal("list", parsed.Command);
    }

    [Fact]
    public void Parse_CollectsPositionals()
    {
        var parsed = ArgParser.Parse(new[] { "add", "buy", "milk" });
        Assert.Equal("add", parsed.Command);
        Assert.Equal(new[] { "buy", "milk" }, parsed.Positionals);
    }

    [Fact]
    public void Parse_OptionWithValue()
    {
        var parsed = ArgParser.Parse(new[] { "add", "task", "--priority", "high" });
        Assert.Equal("high", parsed.GetOption("priority"));
    }

    [Fact]
    public void Parse_BareFlagHasNullValue()
    {
        var parsed = ArgParser.Parse(new[] { "list", "--json" });
        Assert.True(parsed.HasFlag("json"));
        Assert.Null(parsed.GetOption("json"));
    }
}
