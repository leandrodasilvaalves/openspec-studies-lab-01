# Spec Delta

## Purpose

Lets users generate multiple data entries for a command at once, with a limit.

## ADDED Requirements

### Requirement: Batch data generation
The system SHALL accept a `-n` or `--number` parameter to generate multiple entries for any command.

#### Scenario: Generate multiple entries
- **WHEN** user runs `/person -n 5`
- **THEN** system outputs 5 person entries

#### Scenario: Exceed generation limit
- **WHEN** user runs `/person -n 101`
- **THEN** system outputs an error message indicating the limit (100) was exceeded

#### Scenario: Default behavior without parameter
- **WHEN** user runs `/person`
- **THEN** system outputs a single entry
