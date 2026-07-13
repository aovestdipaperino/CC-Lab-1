using TaskCli.Model;
using TaskCli.Storage;
using Xunit;

namespace TaskCli.Tests;

public class JsonTaskStoreTests : IDisposable
{
    private readonly string _path =
        Path.Combine(Path.GetTempPath(), $"taskcli-{Guid.NewGuid():N}.json");

    [Fact]
    public void Load_MissingFile_ReturnsEmpty()
    {
        var store = new JsonTaskStore(_path);
        Assert.Empty(store.Load());
    }

    [Fact]
    public void SaveThenLoad_RoundTrips()
    {
        var store = new JsonTaskStore(_path);
        store.Save(new List<TaskItem>
        {
            new() { Id = 1, Title = "first", Priority = Priority.High, Tag = "work" },
        });

        var loaded = store.Load();

        Assert.Single(loaded);
        Assert.Equal("first", loaded[0].Title);
        Assert.Equal(Priority.High, loaded[0].Priority);
        Assert.Equal("work", loaded[0].Tag);
    }

    public void Dispose()
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
    }
}
