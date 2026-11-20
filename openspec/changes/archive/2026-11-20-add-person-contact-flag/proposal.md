# Proposal

## Why

O comando `/person` produz apenas nome, sobrenome e nome completo, enquanto o contato é gerado por um comando separado. A nova regra permite solicitar contato junto com a pessoa em uma única invocação, com um e-mail determinístico no formato `primeiroNome + 4 dígitos + domínio aleatório`, sem alterar o comportamento padrão.

## What Changes

- O comando `/person` passa a aceitar a flag `--contact`.
- Quando `--contact` estiver presente, a saída de `/person` inclui também `phone` e `email` da pessoa gerada, após `first_name`, `last_name` e `full_name`.
- O `email` gerado para `/person --contact` começa com o `first_name` da pessoa, seguido de um número aleatório de 4 dígitos e de um domínio aleatório fornecido pelo Bogus.
- Quando `--contact` estiver ausente, o comportamento atual de `/person` é preservado exatamente.
- O comando `/contact` mantém seu comportamento atual; a regra de e-mail se aplica somente ao contato gerado para uma pessoa.
- A linha de uso de `/person` passa a ser `/person [--uppercase] [--contact]`.
- A regra "Flags por comando" passa a incluir `--contact` entre as flags de `/person`.

## Capabilities

### New Capabilities

Nenhuma.

### Modified Capabilities

- `cli-command-interface`: aceitação de `--contact` por `/person`, uso/help atualizado e cenário de flag aplicada ao comando correto.
- `person-contact-data`: geração de contato associado à pessoa e formato do e-mail com prefixo de primeiro nome + 4 dígitos + domínio Bogus.

## Impact

- **Código:** `src/FakeDataCli/CliDispatcher.cs` (despacho, uso e saída combinada), `src/FakeDataCli/Generators/ContactGenerator.cs` (geração de e-mail para pessoa) e possivelmente `PersonGenerator.cs`.
- **Testes:** `tests/FakeDataCli.Tests/CliDispatcherTests.cs` e `tests/FakeDataCli.Tests/PersonContactGeneratorTests.cs`.
- **Dependências:** nenhuma nova; Bogus já fornece domínio aleatório.
- **Compatibilidade:** sem impacto quebrável — `--contact` é opcional e o padrão permanece idêntico.
