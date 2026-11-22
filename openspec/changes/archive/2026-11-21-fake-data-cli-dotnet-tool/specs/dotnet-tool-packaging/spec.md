# Spec Delta

## Purpose

Define how the FakeDataCli application is packaged and exposed as a .NET tool whose command is `faker`, invocável como `faker /{command}`.

## ADDED Requirements

### Requirement: Tool package identifier
A package built from the FakeDataCli project SHALL be installable as a .NET tool whose command is `faker`.

#### Scenario: Global tool installation
- **WHEN** the packed package is installed with `dotnet tool install -g --source <dir> fakedatacli`
- **THEN** the tool is available with the command `faker`

### Requirement: Invocation of the tool command
After global installation, the application SHALL be invocable as `faker /{command}` and generate fake data.

#### Scenario: `faker` runs a generation command
- **WHEN** the application is invoked as `faker /person --uppercase` after the tool is installed globally
- **THEN** the `/person` operation runs, the person data is printed, and the process exits with code 0

#### Scenario: `faker /person --contact`
- **WHEN** the application is invoked as `faker /person --contact` after the tool is installed globally
- **THEN** the application prints `first_name`, `last_name`, `full_name`, `phone` and `email`, and exits with code 0

#### Scenario: `faker` passes through flags
- **WHEN** the application is invoked as `faker /cpf --no-punctuation` after the tool is installed globally
- **THEN** the application generates a bare CPF digit string, following the accepted contracts of `cli-command-interface`

#### Scenario: `faker` without arguments
- **WHEN** the application is invoked as `faker` without arguments
- **THEN** the usage message is presented and the process exits with code 0
