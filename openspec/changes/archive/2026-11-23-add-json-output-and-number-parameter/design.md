# Design

## Context

Current CLI implementation uses `FakeDataCli` to run commands like `/person` and `/contact`. The CLI parses incoming arguments and dispatches them to respective generators. See `proposal.md` for motivation.

## Goals / Non-Goals

**Goals:**
- Implement `--json` flag for JSON output in `/person` and `/contact`.
- Implement `-n` / `--number` flag for batch generation across all commands.
- Enforce a 100-entry limit on batch generation.

**Non-Goals:**
- Changes to the underlying data generation logic of Bogus.
- Supporting other output formats (e.g., XML) beyond the current text-based and new JSON format.

## Decisions

- **Command Parsing**: Update `CliDispatcher.Run` to identify `--json` and `-n` flags. Since the project uses a custom slash-command dispatcher, parsing logic will be extended to validate these new inputs.
- **JSON Serialization**: Use `System.Text.Json` (built into .NET 10) for JSON serialization, as it's efficient and standard.
- **Error Handling**: When `-n` exceeds 100, print a clean error message to standard error and exit with code 2 (as per existing contract).

## Risks / Trade-offs

- **Risk**: Performance impact of generating 100 entries.
- **Mitigation**: Bogus is highly efficient; generating 100 entries should be well within acceptable latency for a CLI tool.
- **Trade-off**: The current contract for `/person` and `/contact` output will be slightly altered to allow JSON. We will ensure that the JSON structure is consistent and well-defined.
