# Spec Delta

## MODIFIED Requirements

### Requirement: Ajuda via /help

O comando `/help` DEVE (SHALL) apresentar a lista de comandos e flags disponíveis, sem gerar nenhum dado falso.

#### Scenario: Solicitação de ajuda

- **WHEN** a CLI é invocada com `/help`
- **THEN** a saída lista `/person`, `/contact`, `/cpf`, `/cnpj`, `/guid`, `--no-punctuation`, `--no-dashes`, `--uppercase` e `--contact`, e a aplicação finaliza com código de saída zero

#### Scenario: Ajuda não gera dados

- **WHEN** a CLI é invocada com `/help`
- **THEN** nenhum valor de pessoa, contato, documento ou identificador é apresentado

### Requirement: Flags por comando

Cada flag DEVE (SHALL) ser aceita apenas pelos comandos aos quais se aplica: `--no-punctuation` por `/cpf` e `/cnpj`; `--no-dashes` e `--uppercase` por `/guid`; `--uppercase` e `--contact` por `/person`.

#### Scenario: Flag aplicada ao comando correto
- **WHEN** a CLI é invocada com `/cpf --no-punctuation`, `/guid --uppercase`, `/person --uppercase` ou `/person --contact`
- **THEN** o sistema executa o comando com a formatação solicitada

#### Scenario: Flag aplicada a comando incorreto
- **WHEN** a CLI é invocada com uma flag em um comando que não a suporta
- **THEN** o sistema apresenta uma mensagem de erro e o uso correto na saída de erro, e finaliza com código de saída diferente de zero
