# Spec Delta

## ADDED Requirements

### Requirement: CPF associado à pessoa com --cpf

Quando a flag `--cpf` for informada ao comando `/person`, o sistema DEVE (SHALL) retornar, além de `first_name`, `last_name` e `full_name`, também `cpf`, gerado na mesma execução.

#### Scenario: Pessoa com CPF

- **WHEN** a CLI é invocada com `/person --cpf`
- **THEN** a saída contém exatamente as linhas `first_name`, `last_name`, `full_name` e `cpf`, nessa ordem

#### Scenario: CPF válido gerado na mesma execução

- **WHEN** a CLI é invocada com `/person --cpf`
- **THEN** `cpf` possui 11 dígitos cujos verificadores são confirmados por uma validação independente, e não é uma sequência repetida de um mesmo dígito

#### Scenario: Comportamento padrão preservado sem a flag

- **WHEN** a CLI é invocada com `/person` sem `--cpf`
- **THEN** nenhuma linha `cpf` é apresentada, e as demais flags e saídas permanecem inalteradas

### Requirement: Formatação do CPF em /person

O `cpf` apresentado por `/person --cpf` DEVE (SHALL) ser sempre pontuado no formato `000.000.000-00`, independentemente de outras flags de pessoa; a flag `--no-punctuation` NÃO DEVE ser aceita por `/person`.

#### Scenario: CPF sempre pontuado

- **WHEN** a CLI é invocada com `/person --cpf`
- **THEN** `cpf` corresponde ao padrão `^\d{3}\.\d{3}\.\d{3}-\d{2}$`

#### Scenario: Pontuação não pode ser removida em /person

- **WHEN** a CLI é invocada com `/person --cpf --no-punctuation`
- **THEN** o sistema rejeita a invocação com erro e código de saída de argumentos inválidos, sem gerar dados

### Requirement: Combinação de --cpf com outras flags de /person

A flag `--cpf` DEVE (SHALL) poder ser combinada com `--uppercase` e `--contact`, em qualquer ordem, produzindo uma única saída gerada na mesma execução.

#### Scenario: CPF com caixa alta

- **WHEN** a CLI é invocada com `/person --uppercase --cpf`
- **THEN** `first_name`, `last_name` e `full_name` são retornados em letras maiúsculas, e `cpf` mantém o formato pontuado e continua válido

#### Scenario: CPF com contato

- **WHEN** a CLI é invocada com `/person --contact --cpf`
- **THEN** a saída contém exatamente as linhas `first_name`, `last_name`, `full_name`, `phone`, `email` e `cpf`, nessa ordem

#### Scenario: Ordem das flags não altera a saída

- **WHEN** a CLI é invocada com `/person --cpf --contact` ou `/person --contact --cpf`
- **THEN** a ordem dos campos apresentados é a mesma nos dois casos, e `cpf`, `phone` e `email` aparecem uma única vez cada
