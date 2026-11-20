# Spec Delta

## Purpose

Define a geração de dados falsos de pessoa e contato com aparência brasileira, para que a CLI produza `first_name`, `last_name`, `full_name`, `phone` e `email` plausíveis no locale pt_BR.

## ADDED Requirements

### Requirement: Dados de pessoa no locale pt_BR

O sistema DEVE (SHALL) gerar `first_name`, `last_name` e `full_name` a partir de dados pt_BR, utilizando a biblioteca Bogus configurada com o locale `pt_BR`.

#### Scenario: Geração de dados de pessoa

- **WHEN** o comando `/person` é executado
- **THEN** o sistema apresenta `first_name`, `last_name` e `full_name`, cada um preenchido com um valor pt_BR não vazio

### Requirement: Campo `full_name` consistente

O valor de `full_name` DEVE (SHALL) ser a composição do `first_name` e do `last_name` gerados na mesma execução.

#### Scenario: `full_name` reúne `first_name` e `last_name`

- **WHEN** o comando `/person` é executado
- **THEN** `full_name` contém exatamente o `first_name` seguido do `last_name` exibidos na mesma saída

### Requirement: Dados de contato no locale pt_BR

O sistema DEVE (SHALL) gerar `phone` e `email` com formato válido, utilizando o locale `pt_BR` para a geração.

#### Scenario: Geração de dados de contato

- **WHEN** o comando `/contact` é executado
- **THEN** o sistema apresenta `phone` e `email`, ambos não vazios, com `email` em formato de endereço válido

#### Scenario: Campo `phone` em formato brasileiro

- **WHEN** o comando `/contact` é executado
- **THEN** `phone` contém um DDD de dois dígitos e um número separado em padrão brasileiro
