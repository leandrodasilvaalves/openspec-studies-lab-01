# Proposal

## Why
Currently, the `FakeDataCli` command-line tool lacks support for flexible output formats (like JSON) and limits users to generating a single piece of data per command execution. This change will enhance the user experience by providing JSON output for `/person` and `/contact` commands and introducing a parameter to generate multiple data entries.

## What Changes
- Add support for JSON output format for `/person` and `/contact` commands.
- Introduce a new parameter `-n` / `--number` for all commands to specify the number of entries to generate.
- The default behavior when `-n` is not provided will remain unchanged (generating a single entry).
- The `-n` parameter will have a maximum limit of 100 entries. Requests for more than 100 entries will return an error message.

## Capabilities

### New Capabilities
- `cli-output`: Enhances the CLI's capability to output data in JSON format.
- `cli-batch-generation`: Adds the capability to generate multiple data entries via the `-n`/`--number` parameter.

### Modified Capabilities
- N/A

## Impact
- `src/FakeDataCli/CliDispatcher.cs`: Will need to handle the new flag/parameter.
- `src/FakeDataCli/Generators/`: Will need to update the generator logic to support batch generation.
- CLI command usage and output format documentation will need updates.
