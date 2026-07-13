# TaskCli — project guide for Claude Code

A small command-line task tracker. .NET 8, no third-party runtime dependencies.

## Commands
- Build:  `dotnet build`
- Test:   `dotnet test`
- Run:    `dotnet run --project src/TaskCli -- <args>`
  - e.g. `dotnet run --project src/TaskCli -- add "write the talk" --priority high --tag work`
  - e.g. `dotnet run --project src/TaskCli -- list --status open`

## Layout
- `src/TaskCli/` — the CLI application
  - `Program.cs` — entry point; resolves the store and routes the command
  - `Cli/ArgParser.cs` — turns argv into `ParsedArgs` (command, positionals, options)
  - `Cli/CommandRouter.cs` — maps a command name to an `ICommand`
  - `Commands/` — one class per verb (add, list, done, remove); all implement `ICommand`
  - `Model/` — `TaskItem` plus the `Priority` and `TaskState` enums
  - `Storage/` — `ITaskStore` + `JsonTaskStore` (persists to `tasks.json`)
  - `Output/` — `TaskTableFormatter` renders the list table
- `tests/TaskCli.Tests/` — xUnit tests; use `InMemoryTaskStore`, not the filesystem

## Conventions
- Commands never touch `Console` directly. They take `TextWriter output`/`error` so tests can capture them.
- Commands return an `int` exit code: `0` = success, `1` = user error.
- The store path comes from the `TASKCLI_STORE` env var, else `./tasks.json`. Tests set their own path or use `InMemoryTaskStore`.
- Enums serialize as camelCase strings (see `JsonTaskStore.Options`). Keep it that way so existing `tasks.json` files stay readable.

## Adding a command
Add a class in `Commands/`, register it in `CommandRouter.Route`, and add a test next to the others.
