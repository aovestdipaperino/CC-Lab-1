namespace TaskCli.Cli;

/// <summary>Result of parsing the command-line arguments.</summary>
public sealed class ParsedArgs
{
    public string? Command { get; init; }

    public IReadOnlyList<string> Positionals { get; init; } = Array.Empty<string>();

    public IReadOnlyDictionary<string, string?> Options { get; init; }
        = new Dictionary<string, string?>();

    public bool HasFlag(string name) => Options.ContainsKey(name);

    public string? GetOption(string name)
        => Options.TryGetValue(name, out var value) ? value : null;
}

/// <summary>
/// Minimal argv parser. Shape: &lt;command&gt; [positionals...] [--option value | --flag].
/// A "--name" followed by a non-dash token becomes an option with that value;
/// otherwise it is a boolean flag with a null value.
/// </summary>
public static class ArgParser
{
    public static ParsedArgs Parse(string[] args)
    {
        string? command = null;
        var positionals = new List<string>();
        var options = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        var i = 0;
        if (args.Length > 0 && !args[0].StartsWith("--", StringComparison.Ordinal))
        {
            command = args[0];
            i = 1;
        }

        for (; i < args.Length; i++)
        {
            var token = args[i];
            if (token.StartsWith("--", StringComparison.Ordinal))
            {
                var name = token[2..];
                if (i + 1 < args.Length && !args[i + 1].StartsWith("--", StringComparison.Ordinal))
                {
                    options[name] = args[i + 1];
                    i++;
                }
                else
                {
                    options[name] = null;
                }
            }
            else
            {
                positionals.Add(token);
            }
        }

        return new ParsedArgs
        {
            Command = command,
            Positionals = positionals,
            Options = options,
        };
    }
}
