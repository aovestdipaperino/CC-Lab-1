# Lab 1 — Onboard & ship a feature

**Time:** 45 minutes · **Language:** C# (.NET 8)

You've just joined a project you've never seen: `TaskCli`, a command-line task tracker.
Your job is to get comfortable driving Claude Code through the core loop and ship a small
feature the way you would on a real team.

## Setup
The `dotnet` commands are identical on every platform:
```bash
dotnet build
dotnet test      # everything should be green before you start
dotnet run --project src/TaskCli -- add "try me" --priority high
dotnet run --project src/TaskCli -- list
```

## Your tasks
1. **Have Claude explain the repo to you.** Ask for the architecture and where things live.
   Verify the explanation against the code — is it right?
2. **Pick issue #1** from `ISSUES.md`. Drive the core loop deliberately:
   **explore → plan → review the plan → let it code.** Reject the first plan at least once.
3. **Run the tests. Commit** with a Claude-generated message.

## Exit criteria
- Tests green, one commit, the feature demonstrably works from the CLI.

## Stretch
- Issue #2 (touches several files), or improve `CLAUDE.md` with something you learned.

Stuck at the 30-minute mark? See `hints.md`.
Wrecked your working copy? Run `.\reset.ps1` (Windows) or `./reset.sh` (macOS/Linux).
