# Lab 1 — Onboard & ship a feature

[![GitHub stars](https://img.shields.io/github/stars/aovestdipaperino/CC-Lab-1?logo=github&color=brightgreen)](https://github.com/aovestdipaperino/CC-Lab-1/stargazers)

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
2. **Work `ISSUES.md` in order.** It's five steps, gradually harder. Drive the core loop
   deliberately on the first one: **explore → plan → review the plan → let it code.**
   Reject the first plan at least once.
3. **Run the gate to see your progress:** `./check.sh` (macOS/Linux) or `.\check.ps1`
   (Windows). It builds, then runs step 1 → step 5, stopping at the first that fails.
4. **Commit** each working step with a Claude-generated message.

Unlike labs 2 and 3, there is no /goal gate here — you drive the loop yourself and run the
gate when you want to check in.

## Exit criteria
- At least step 1 green, one commit, the feature demonstrably works from the CLI.

## Going further
- The steps keep getting harder (a `stats` command, sorting, due dates that touch the model
  and storage). Nobody's expected to finish all five — get as far as you can, cleanly.

Stuck at the 30-minute mark? See `hints.md`.
Wrecked your working copy? Run `.\reset.ps1` (Windows) or `./reset.sh` (macOS/Linux).
