# Proposal

## Why

O comando `/person` hoje permite incluir dados de contato com `--contact`, mas não permite obter um CPF na mesma execução. Quem precisa de uma pessoa fictícia com documento válido precisa rodar dois comandos e não há garantia de que o CPF pertença à mesma execução. Adicionar `--cpf` ao `/person` resolve isso preservando o comportamento atual.

## What Changes

- O comando `/person` passa a aceitar `--cpf`.
- Com `--cpf`, a saída contém os campos de pessoa (`first_name`, `last_name`, `full_name`) seguidos de `cpf`, gerado na mesma execução.
- `--cpf` é combinável com as flags existentes de `/person`: `--uppercase` e `--contact`.
- O CPF exibido por `/person --cpf` é sempre pontuado (`000.000.000-00`); `--no-punctuation` continua restrita a `/cpf` e `/cnpj`.
- A ajuda e o uso passam a documentar `--cpf` em `/person`.
- Sem `--cpf`, a saída e o comportamento atuais de `/person` são preservados.

## Capabilities

### New Capabilities

Nenhuma.

### Modified Capabilities

- `cli-command-interface`: `/person` passa a aceitar `--cpf`, e `/help` e o uso listam essa flag; `--cpf` aplicada a outros comandos é rejeitada.
- `person-contact-data`: a saída de `/person --cpf` inclui um campo `cpf` com documento válido, combinável com `--uppercase` e `--contact`.

## Impact

- `src/FakeDataCli/CliDispatcher.cs`: validação de flags de `/person`, escrita de CPF, uso/ajuda.
- `src/FakeDataCli/Generators/BrazilianDocumentGenerator.cs`: reutilização de `GenerateCpfDigits`/`FormatCpf` (sem alteração de contrato).
- `tests/FakeDataCli.Tests/CliDispatcherTests.cs`: cobertura de `--cpf` isolado, combinado e rejeitado em outros comandos.
- Specs em `openspec/specs/cli-command-interface/spec.md` e `openspec/specs/person-contact-data/spec.md` após arquivamento.
- Sem novas dependências.
