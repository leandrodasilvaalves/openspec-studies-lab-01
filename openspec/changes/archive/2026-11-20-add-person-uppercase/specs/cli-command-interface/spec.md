# Spec Delta

## MODIFIED Requirements

### Requirement: Flags por comando

Cada flag DEVE (SHALL) ser aceita apenas pelos comandos aos quais se aplica: `--no-punctuation` por `/cpf` e `/cnpj`; `--no-dashes` e `--uppercase` por `/guid`; `--uppercase` por `/person`.

#### Scenario: Flag aplicada ao comando correto
- **WHEN** a CLI é invocada com `/cpf --no-punctuation`, `/guid --uppercase` ou `/person --uppercase`
- **THEN** o sistema executa o comando com a formatação solicitada

#### Scenario: Flag aplicada a comando incorreto
- **WHEN** a CLI é invocada com uma flag em um comando que não a suporta
- **THEN** o sistema apresenta uma mensagem de erro e o uso correto na saída de erro, e finaliza com código de saída diferente de zero
