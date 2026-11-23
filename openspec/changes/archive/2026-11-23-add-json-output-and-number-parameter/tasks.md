# Tasks

## 1. CLI Argument Parsing

- [x] 1.1 Update `CliDispatcher.cs` to identify and store `--json` and `-n` / `--number` flags. Verify by printing the parsed flags.
- [x] 1.2 Implement input validation for `-n` to enforce a limit of 100 entries. Verify with a test command running with `-n 101` and checking for error output.

## 2. Generator Updates

- [x] 2.1 Refactor `PersonGenerator` to support batch generation based on the number parameter. Verify by generating 5 persons and checking output count.
- [x] 2.2 Refactor `ContactGenerator` to support batch generation based on the number parameter. Verify by generating 3 contacts and checking output count.

## 3. JSON Output Implementation

- [x] 3.1 Implement JSON serialization for `Person` generator. Verify by generating a person with `--json` and checking if output is valid JSON.
- [x] 3.2 Implement JSON serialization for `Contact` generator. Verify by generating a contact with `--json` and checking if output is valid JSON.

## 4. Integration Verification

- [x] 4.1 Run existing tests (`dotnet test`) to ensure no regressions in default behavior.
- [x] 4.2 Verify `/person -n 5 --json` generates 5 person entries in JSON format.
- [x] 4.3 Verify default behavior (`/person`) still outputs the original format when no flags are present.

## Workflow follow-up

- Archive the change after the project's review requirements are satisfied.
- Verify the archived result.
