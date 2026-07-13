# Hints — Issue #1 (`--json`)

Peel these one at a time. Try the loop yourself first.

<details><summary>Hint 1 — where the flag surfaces</summary>

`ArgParser` already turns `--json` into a bare flag. Inside `ListCommand` you can read it
with `args.HasFlag("json")`. You don't need to touch the parser.
</details>

<details><summary>Hint 2 — where the branch goes</summary>

`ListCommand.Execute` builds `result` (the filtered, ordered list) and then calls
`TaskTableFormatter.Format`. Branch *after* `result` is built: if the flag is set, serialize
`result`; otherwise format the table as today.
</details>

<details><summary>Hint 3 — serializing consistently</summary>

Reuse the same options `JsonTaskStore` uses (indented, camelCase, `JsonStringEnumConverter`)
so the CLI output matches the stored format. Simplest path: expose those options or a small
`ToJson` helper the command can call. Write to `output`, not `Console`.
</details>

<details><summary>Hint 4 — the test</summary>

Capture output with a `StringWriter`, run `list --json` against a seeded `InMemoryTaskStore`,
then `JsonSerializer.Deserialize<List<TaskItem>>(...)` the captured text and assert the count
and a title. That proves it's valid JSON, not just JSON-ish text.
</details>
