using System.Text.Json;
using System.Text.Json.Serialization;
using TaskCli.Model;

namespace TaskCli.Storage;

public sealed class JsonTaskStore : ITaskStore
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
    };

    private readonly string _path;

    public JsonTaskStore(string path) => _path = path;

    public IReadOnlyList<TaskItem> Load()
    {
        if (!File.Exists(_path))
        {
            return new List<TaskItem>();
        }

        var json = File.ReadAllText(_path);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<TaskItem>();
        }

        return JsonSerializer.Deserialize<List<TaskItem>>(json, Options)
            ?? new List<TaskItem>();
    }

    public void Save(IReadOnlyList<TaskItem> tasks)
    {
        var dir = Path.GetDirectoryName(_path);
        if (!string.IsNullOrEmpty(dir))
        {
            Directory.CreateDirectory(dir);
        }

        var json = JsonSerializer.Serialize(tasks, Options);
        File.WriteAllText(_path, json);
    }
}
