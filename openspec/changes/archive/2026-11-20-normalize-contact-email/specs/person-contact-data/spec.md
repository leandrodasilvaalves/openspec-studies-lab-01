# Spec Delta

## ADDED Requirements

### Requirement: E-mail sem diacríticos e com caixa controlada

Todo `email` gerado pela CLI DEVE (SHALL) ser composto apenas por caracteres ASCII sem diacríticos. Sem `--uppercase`, o `email` DEVE (SHALL) ser retornado inteiramente em minúsculas; com `--uppercase` em `/person`, o `email` DEVE (SHALL) ser retornado inteiramente em maiúsculas.

#### Scenario: E-mail de /contact em minúsculas e sem diacríticos

- **WHEN** o comando `/contact` é executado
- **THEN** `email` contém apenas caracteres ASCII, não contém diacríticos e é idêntico à sua versão convertida para minúsculas

#### Scenario: E-mail de /person --contact em minúsculas e sem diacríticos

- **WHEN** a CLI é invocada com `/person --contact`
- **THEN** `email` contém apenas caracteres ASCII, não contém diacríticos e é idêntico à sua versão convertida para minúsculas, mesmo quando `first_name` ou o domínio gerados possuem letras maiúsculas ou acentos

#### Scenario: E-mail de /person --uppercase --contact em maiúsculas e sem diacríticos

- **WHEN** a CLI é invocada com `/person --uppercase --contact`
- **THEN** `email` contém apenas caracteres ASCII, não contém diacríticos e é idêntico à sua versão convertida para maiúsculas

## MODIFIED Requirements

### Requirement: Contato associado à pessoa com --contact
Quando a flag `--contact` for informada ao comando `/person`, o sistema DEVE (SHALL) retornar, além de `first_name`, `last_name` e `full_name`, também `phone` e `email` gerados para a pessoa da mesma execução.

#### Scenario: Pessoa com contato
- **WHEN** a CLI é invocada com `/person --contact`
- **THEN** a saída contém exatamente as linhas `first_name`, `last_name`, `full_name`, `phone` e `email`, nessa ordem

#### Scenario: Contato coerente com a pessoa
- **WHEN** a CLI é invocada com `/person --contact`
- **THEN** o prefixo do `email` corresponde ao `first_name` exibido na mesma saída após remoção de diacríticos e conversão para minúsculas

#### Scenario: Comportamento padrão preservado
- **WHEN** a CLI é invocada com `/person` sem `--contact`
- **THEN** apenas `first_name`, `last_name` e `full_name` são retornados

### Requirement: Formato do e-mail do contato de pessoa
O `email` gerado para `/person --contact` DEVE (SHALL) ser composto pelo `first_name` da pessoa sem diacríticos, seguido de um número aleatório de exatamente 4 dígitos e de um domínio aleatório fornecido pelo Bogus, com a caixa definida pela presença de `--uppercase`.

#### Scenario: E-mail com prefixo e sufixo esperados
- **WHEN** a CLI é invocada com `/person --contact`
- **THEN** `email` corresponde ao padrão `<first_name sem diacríticos em minúsculas><4 dígitos>@<domínio>`, sem caracteres adicionais entre o nome e os dígitos

#### Scenario: E-mail com caixa alta
- **WHEN** a CLI é invocada com `/person --uppercase --contact`
- **THEN** o `email` inteiro é retornado em maiúsculas, e seu prefixo corresponde ao `first_name` exibido na saída após remoção de diacríticos e conversão para maiúsculas
