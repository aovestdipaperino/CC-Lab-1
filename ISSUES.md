# Issues

Work them in order. #1 is the lab; #2 and #3 are stretch goals.

---

## Issue #1 — Add a `--json` output flag to `list`
**Difficulty:** small

`tasks list` currently prints a text table. Consumers want machine-readable output.

**Acceptance criteria**
- `tasks list --json` prints a JSON array of the (filtered) tasks instead of the table.
- Without `--json`, output is byte-for-byte unchanged.
- `--json` composes with the existing `--status` filter.
- A test covers the JSON path (assert it parses and contains the expected task).

*Hint:* the flag arrives as `args.HasFlag("json")`. Keep the JSON shape consistent with
`JsonTaskStore` (camelCase, enums as strings).

---

## Issue #2 — Filter `list` by tag and priority
**Difficulty:** medium (touches the parser usage, the command, and tests)

**Acceptance criteria**
- `tasks list --tag work` shows only tasks with that tag.
- `tasks list --priority high` shows only that priority.
- Filters combine with each other and with `--status`.
- Unknown priority value → friendly error, exit code 1.
- Tests cover each filter and a combination.

---

## Issue #3 — A `stats` command
**Difficulty:** larger

**Acceptance criteria**
- `tasks stats` prints counts by state (open/done) and by priority.
- Registered in `CommandRouter`, with its own `ICommand` implementation and tests.
