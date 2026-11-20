# cli-command-interface Specification

## Purpose

Define a superfície observável da CLI: quais comandos slash e flags são aceitos, como a ajuda é apresentada e como a aplicação sinaliza sucesso ou erro.

## Requirements

### Requirement: Comandos slash em inglês

A CLI DEVE (SHALL) aceitar como primeiro argumento apenas os comandos `/person`, `/contact`, `/cpf`, `/cnpj`, `/guid` e `/help`.

#### Scenario: Comando válido é executado

- **WHEN** a CLI é invocada com um dos comandos válidos
- **THEN** o sistema executa a operação correspondente e finaliza com código de saída zero

#### Scenario: Comando desconhecido é rejeitado

- **WHEN** a CLI é invocada com um comando que não está na lista de comandos válidos
- **THEN** o sistema apresenta uma mensagem de erro e o uso correto na saída de erro, e finaliza com código de saída diferente de zero

### Requirement: Ajuda via /help

O comando `/help` DEVE (SHALL) apresentar a lista de comandos e flags disponíveis, sem gerar nenhum dado falso.

#### Scenario: Solicitação de ajuda

- **WHEN** a CLI é invocada com `/help`
- **THEN** a saída lista `/person`, `/contact`, `/cpf`, `/cnpj`, `/guid`, `--no-punctuation`, `--no-dashes`, `--uppercase` e `--contact`, e a aplicação finaliza com código de saída zero

#### Scenario: Ajuda não gera dados

- **WHEN** a CLI é invocada com `/help`
- **THEN** nenhum valor de pessoa, contato, documento ou identificador é apresentado

### Requirement: Invocação sem argumentos

Quando nenhum argumento for informado, a CLI DEVE (SHALL) apresentar o mesmo uso do `/help`.

#### Scenario: Nenhum argumento informado

- **WHEN** a CLI é invocada sem argumentos
- **THEN** o sistema apresenta a lista de comandos e flags disponíveis

### Requirement: Flags por comando

Cada flag DEVE (SHALL) ser aceita apenas pelos comandos aos quais se aplica: `--no-punctuation` por `/cpf` e `/cnpj`; `--no-dashes` e `--uppercase` por `/guid`; `--uppercase` e `--contact` por `/person`.

#### Scenario: Flag aplicada ao comando correto

- **WHEN** a CLI é invocada com `/cpf --no-punctuation`, `/guid --uppercase`, `/person --uppercase` ou `/person --contact`
- **THEN** o sistema executa o comando com a formatação solicitada

#### Scenario: Flag aplicada a comando incorreto

- **WHEN** a CLI é invocada com uma flag em um comando que não a suporta
- **THEN** o sistema apresenta uma mensagem de erro e o uso correto na saída de erro, e finaliza com código de saída diferente de zero

### Requirement: Formato da saída de dados

A saída de um comando de geração DEVE (SHALL) conter apenas os valores gerados, um por linha, com rótulo em inglês quando o comando produzir mais de um campo.

#### Scenario: Saída de comando com múltiplos campos

- **WHEN** o comando `/person` ou `/contact` é executado
- **THEN** cada campo gerado aparece em uma linha própria, no formato `label: value`, usando `first_name`, `last_name`, `full_name`, `phone` e `email` como rótulos

#### Scenario: Saída de comando com campo único

- **WHEN** o comando `/cpf`, `/cnpj` ou `/guid` é executado
- **THEN** apenas o valor gerado aparece na saída, sem rótulo ou texto adicional
