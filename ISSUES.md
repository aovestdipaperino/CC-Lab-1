# Issues

Five steps, in order, gradually harder. `./check.sh` (`.\check.ps1` on Windows)
runs them as a staged gate: build, then step 1 → step 5, stopping at the first
that fails. The existing test suite is already green — these steps are the
features you add. Getting through all five is not expected; go as far as you can.

Each step has tests tagged `[Trait("step", "N")]` in `tests/TaskCli.Tests/` — the
tests are the acceptance criteria.

---

## Step 1 — `list --json`
**Difficulty:** small

`tasks list` prints a text table; consumers want machine-readable output.

- `list --json` prints a JSON array of the (filtered) tasks instead of the table.
- Without `--json`, output is byte-for-byte unchanged.
- `--json` composes with the existing `--status` filter.

*Hint:* the flag arrives as `args.HasFlag("json")`. Keep the JSON shape consistent
with `JsonTaskStore` (camelCase, enums as strings).

---

## Step 2 — Filter `list` by tag and priority
**Difficulty:** medium (parser usage, the command, and tests)

- `list --tag work` shows only tasks with that tag.
- `list --priority high` shows only that priority.
- Filters combine with each other and with `--status`.
- Unknown priority value → friendly error, exit code 1.

---

## Step 3 — A `stats` command
**Difficulty:** medium-large

- `tasks stats` prints counts by state (open/done) and by priority.
- Registered in `CommandRouter`, with its own `ICommand` implementation.

---

## Step 4 — `list --sort priority`
**Difficulty:** medium

- `list --sort priority` orders the output high → medium → low.
- Ties keep the existing ascending-id order.

---

## Step 5 — Due dates
**Difficulty:** larger (model, storage, parser, two commands)

- `add <title> --due YYYY-MM-DD` records a due date on the task.
- `list --due-before YYYY-MM-DD` shows only tasks due strictly before that date.
- Due dates survive a save/load round-trip through `JsonTaskStore`.

*Hint:* add a nullable `DueDate` to `TaskItem`; keep the JSON camelCase and
tolerant of tasks that have no due date.
