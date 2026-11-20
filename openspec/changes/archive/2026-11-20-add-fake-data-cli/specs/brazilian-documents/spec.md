# Spec Delta

## Purpose

Garante que os documentos brasileiros gerados sejam matematicamente válidos e que a CLI permita escolher entre a apresentação pontuada e apenas dígitos.

## ADDED Requirements

### Requirement: CPF com dígitos verificadores válidos

O sistema DEVE (SHALL) gerar um CPF de 11 dígitos cujos dois dígitos verificadores sejam calculados pelo algoritmo oficial, de modo que o resultado passe em validação independente.

#### Scenario: CPF gerado é aceito por validação oficial

- **WHEN** o comando `/cpf` é executado
- **THEN** o valor apresentado possui 11 dígitos e seus dígitos verificadores são confirmados por uma validação independente do gerador

#### Scenario: CPF gerado não é uma sequência inválida conhecida

- **WHEN** o comando `/cpf` é executado
- **THEN** o valor apresentado não é uma sequência repetida de um mesmo dígito

### Requirement: CNPJ com dígitos verificadores válidos

O sistema DEVE (SHALL) gerar um CNPJ de 14 dígitos cujos dois dígitos verificadores sejam calculados pelo algoritmo oficial, de modo que o resultado passe em validação independente.

#### Scenario: CNPJ gerado é aceito por validação oficial

- **WHEN** o comando `/cnpj` é executado
- **THEN** o valor apresentado possui 14 dígitos e seus dígitos verificadores são confirmados por uma validação independente do gerador

#### Scenario: CNPJ gerado não é uma sequência inválida conhecida

- **WHEN** o comando `/cnpj` é executado
- **THEN** o valor apresentado não é uma sequência repetida de um mesmo dígito

### Requirement: Formatação do documento

A saída DEVE (SHALL) ser pontuada por padrão (`000.000.000-00` para CPF e `00.000.000/0000-00` para CNPJ) e conter apenas dígitos quando a flag `--no-punctuation` for informada.

#### Scenario: Saída pontuada por padrão

- **WHEN** o comando `/cpf` ou `/cnpj` é executado sem flags de formatação
- **THEN** o valor apresentado contém os separadores de pontuação do respectivo documento

#### Scenario: Saída sem pontuação

- **WHEN** o comando `/cpf` ou `/cnpj` é executado com a flag `--no-punctuation`
- **THEN** o valor apresentado contém somente os 11 ou 14 dígitos, sem pontos, barras ou hífens

#### Scenario: Flag de formatação não altera a validade

- **WHEN** o comando `/cpf` ou `/cnpj` é executado com `--no-punctuation`
- **THEN** o valor sem pontuação continua passando na validação independente dos dígitos verificadores
