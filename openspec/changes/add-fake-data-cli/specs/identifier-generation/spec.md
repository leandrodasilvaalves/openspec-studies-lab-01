# Spec Delta

## Purpose

Define a geração de identificadores UUID e as opções de apresentação da CLI, permitindo controlar a presença de traços e a caixa das letras na saída.

## ADDED Requirements

### Requirement: Geração de GUID

O sistema DEVE (SHALL) gerar um GUID válido no formato padrão com traços e letras minúsculas quando nenhuma flag de formatação for informada.

#### Scenario: GUID padrão

- **WHEN** o comando `/guid` é executado sem flags de formatação
- **THEN** o sistema apresenta um GUID no formato de 8-4-4-4-12 caracteres hexadecimais em minúsculas

#### Scenario: GUID gerado é interpretável como identificador

- **WHEN** o comando `/guid` é executado
- **THEN** o valor apresentado é aceito como um GUID válido por um analisador de GUID

### Requirement: Remoção de traços

A flag `--no-dashes` DEVE (SHALL) remover todos os traços do GUID apresentado, mantendo os 32 caracteres hexadecimais.

#### Scenario: GUID sem traços

- **WHEN** o comando `/guid` é executado com a flag `--no-dashes`
- **THEN** o valor apresentado não contém nenhum hífen e possui 32 caracteres hexadecimais

### Requirement: Caixa das letras

A flag `--uppercase` DEVE (SHALL) converter as letras do GUID para maiúsculas; sem ela, as letras DEVEM (SHALL) permanecer em minúsculas.

#### Scenario: GUID em maiúsculas

- **WHEN** o comando `/guid` é executado com a flag `--uppercase`
- **THEN** todas as letras do valor apresentado estão em maiúsculas e nenhum dígito é alterado

#### Scenario: GUID em minúsculas por padrão

- **WHEN** o comando `/guid` é executado sem a flag `--uppercase`
- **THEN** todas as letras do valor apresentado estão em minúsculas

### Requirement: Flags de formatação combináveis

As flags `--no-dashes` e `--uppercase` DEVEM (SHALL) poder ser usadas juntas, aplicando ambos os efeitos na mesma saída.

#### Scenario: GUID sem traços e em maiúsculas

- **WHEN** o comando `/guid` é executado com `--no-dashes` e `--uppercase`
- **THEN** o valor apresentado possui 32 caracteres hexadecimais maiúsculos e nenhum hífen
