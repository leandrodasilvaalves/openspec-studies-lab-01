# Spec Delta

## ADDED Requirements

### Requirement: Contato associado à pessoa com --contact
Quando a flag `--contact` for informada ao comando `/person`, o sistema DEVE (SHALL) retornar, além de `first_name`, `last_name` e `full_name`, também `phone` e `email` gerados para a pessoa da mesma execução.

#### Scenario: Pessoa com contato
- **WHEN** a CLI é invocada com `/person --contact`
- **THEN** a saída contém exatamente as linhas `first_name`, `last_name`, `full_name`, `phone` e `email`, nessa ordem

#### Scenario: Contato coerente com a pessoa
- **WHEN** a CLI é invocada com `/person --contact`
- **THEN** o prefixo do `email` corresponde ao `first_name` exibido na mesma saída

#### Scenario: Comportamento padrão preservado
- **WHEN** a CLI é invocada com `/person` sem `--contact`
- **THEN** apenas `first_name`, `last_name` e `full_name` são retornados

### Requirement: Formato do e-mail do contato de pessoa
O `email` gerado para `/person --contact` DEVE (SHALL) ser composto pelo `first_name` da pessoa, seguido de um número aleatório de exatamente 4 dígitos e de um domínio aleatório fornecido pelo Bogus.

#### Scenario: E-mail com prefixo e sufixo esperados
- **WHEN** a CLI é invocada com `/person --contact`
- **THEN** `email` corresponde ao padrão `<first_name><4 dígitos>@<domínio>`, sem caracteres adicionais entre o nome e os dígitos

#### Scenario: E-mail com caixa alta
- **WHEN** a CLI é invocada com `/person --uppercase --contact`
- **THEN** o prefixo do `email` corresponde ao `first_name` em caixa alta exibido na saída
