using TaskCli.Cli;
using TaskCli.Storage;

namespace TaskCli;

public static class Program
{
    public static int Main(string[] args)
    {
        var parsed = ArgParser.Parse(args);

        var storePath = Environment.GetEnvironmentVariable("TASKCLI_STORE")
            ?? Path.Combine(Directory.GetCurrentDirectory(), "tasks.json");

        var store = new JsonTaskStore(storePath);
        var router = new CommandRouter();
        return router.Route(parsed, store, Console.Out, Console.Error);
    }
}
