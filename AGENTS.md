# AGENTS.md

## Project

.NET 10 console CLI (`FakeDataCli`) generating Brazilian fake data with Bogus (`pt_BR`). Solution file: `FakeDataCli.slnx`.

## Commands

- Build: `dotnet build`
- Test all: `dotnet test`
- Run CLI: `dotnet run --project src/FakeDataCli -- /person`
- Single test: `dotnet test --filter "FullyQualifiedName~<TestName>"`

## Architecture

- Entry point: `src/FakeDataCli/Program.cs` → `CliDispatcher.Run`.
- `src/FakeDataCli/CliDispatcher.cs`: slash-command dispatch, flag validation, usage/help.
- `src/FakeDataCli/Generators/`: `PersonGenerator`, `ContactGenerator`, `BrazilianDocumentGenerator`, `IdentifierGenerator`.
- `src/FakeDataCli/SharedFaker.cs`: single shared `Faker` instance using locale `pt_BR`.
- Tests: `tests/FakeDataCli.Tests/` (xUnit), reference the CLI project.

## CLI contract

- Commands: `/person`, `/contact`, `/cpf`, `/cnpj`, `/guid`, `/help`.
- Flags:
  - `/cpf`, `/cnpj`: `--no-punctuation`
  - `/guid`: `--no-dashes`, `--uppercase`
  - `/person`: `--uppercase`, `--contact`
- Output labels are English (`first_name`, `last_name`, `full_name`, `phone`, `email`); commands/flags are English except `cpf`/`cnpj`.
- `/person --contact` appends `phone` and `email`; the e-mail is `<first_name><4 digits>@<Bogus domain>`.
- Default behavior must be preserved when optional flags are absent.

## OpenSpec

- Specs live in `openspec/specs/`; active changes in `openspec/changes/`.
- Use `openspec status --change "<name>" --json`, `openspec instructions ...`, and `openspec validate` rather than hand-editing metadata.
- Do not manually scaffold change directories; use `openspec new change`.

### OpenSpec Post-Archive Workflow
Whenever the `/opsx-archive` command (or the `openspec-archive-change` skill) finishes archiving a change, execute the following steps in strict order:

1. **Update `AGENTS.md`:** Review the archived change and update `AGENTS.md` if the implementation introduced new architectural patterns, dependencies, CLI commands, directory structures, or conventions. Keep updates concise and remove obsolete rules.
2. **Stage All Changes:** Run `git status` and stage all modified source files, updated specs in `openspec/specs/`, the archived change in `openspec/changes/archive/`, and the updated `AGENTS.md` using `git add -A`.
3. **Commit:** Create a single descriptive commit summarizing the feature and archive (e.g., `feat(<change-name>): implement and archive spec`). Do NOT push unless explicitly requested.



## Constraints

- Do not add new package references unless explicitly required; Bogus is the only runtime dependency.
- Keep `ImplicitUsings` and nullable reference types enabled.
- Preserve the existing exit-code contract: success `0`, invalid arguments `2`.
- Run `dotnet test` before considering a change complete.