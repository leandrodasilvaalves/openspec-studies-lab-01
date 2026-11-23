# Spec Delta

## Purpose

Lets users generate multiple data entries and output them in a machine-readable JSON format.

## ADDED Requirements

### Requirement: JSON output for person and contact
The system SHALL provide a JSON output format for the `/person` and `/contact` commands.

#### Scenario: Generate person as JSON
- **WHEN** user runs `/person --json`
- **THEN** system outputs person data as a valid JSON object

#### Scenario: Generate contact as JSON
- **WHEN** user runs `/contact --json`
- **THEN** system outputs contact data as a valid JSON object
