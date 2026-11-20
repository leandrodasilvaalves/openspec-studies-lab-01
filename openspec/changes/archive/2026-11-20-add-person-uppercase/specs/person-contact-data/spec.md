# Spec Delta

## ADDED Requirements

### Requirement: Dados de pessoa em caixa alta com --uppercase
Quando a flag `--uppercase` for informada ao comando `/person`, o sistema DEVE (SHALL) retornar `first_name`, `last_name` e `full_name` em letras maiúsculas, utilizando caixa invariante.

#### Scenario: Pessoa em caixa alta
- **WHEN** a CLI é invocada com `/person --uppercase`
- **THEN** `first_name`, `last_name` e `full_name` são retornados em letras maiúsculas

#### Scenario: full_name consistente em caixa alta
- **WHEN** a CLI é invocada com `/person --uppercase`
- **THEN** `full_name` contém exatamente o `first_name` seguido do `last_name` exibidos na mesma saída, ambos em caixa alta

#### Scenario: Comportamento padrão preservado sem a flag
- **WHEN** a CLI é invocada com `/person` sem `--uppercase`
- **THEN** `first_name`, `last_name` e `full_name` são retornados no formato padrão, sem alteração de caixa
