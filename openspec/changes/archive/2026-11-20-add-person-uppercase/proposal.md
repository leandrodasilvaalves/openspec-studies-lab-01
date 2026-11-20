# Proposal

## Why

A CLI já gera dados de pessoa, mas não permite formatar o resultado em caixa alta, ao contrário do comando `/guid`, que já suporta `--uppercase`. Uniformizar essa capacidade evita inconsistência na interface e atende à nova regra de caixa alta em Pessoa, mantendo o comportamento padrão quando a flag não é informada.

## What Changes

- O comando `/person` passa a aceitar a flag `--uppercase`.
- Quando `--uppercase` estiver presente, `first_name`, `last_name` e `full_name` são retornados em letras maiúsculas (caixa invariante).
- Quando ausente, o comportamento padrão existente é preservado.
- `PersonGenerator.Generate` ganha o parâmetro opcional `uppercase` (default `false`), aplicando a caixa dentro do gerador, espelhando `IdentifierGenerator.Format`.
- A linha de uso de `/person` passa a ser `/person [--uppercase]`.
- A regra "Flags por comando" passa a incluir `--uppercase` para `/person` além de `/guid`.
- O cenário de flag incorreta deixa de usar `/person --uppercase` como exemplo e passa a usar outra combinação inválida (ex.: `/person --no-punctuation`).

## Capabilities

### New Capabilities

Nenhuma.

### Modified Capabilities

- `cli-command-interface`: a flag `--uppercase` passa a ser aceita também por `/person`; help/uso e o cenário de flag incorreta são ajustados.
- `person-contact-data`: novo cenário de geração de dados de pessoa em caixa alta com `--uppercase` e preservação do padrão sem a flag.

## Impact

- **Código:** `src/FakeDataCli/CliDispatcher.cs` (despacho e uso) e `src/FakeDataCli/Generators/PersonGenerator.cs` (parâmetro `uppercase`).
- **Testes:** `tests/FakeDataCli.Tests/CliDispatcherTests.cs` (cenário de flag incorreta) e possivelmente `PersonContactGeneratorTests.cs`.
- **Dependências:** nenhuma nova.
- **Compatibilidade:** sem impacto quebrável — o comportamento padrão de `/person` permanece idêntico; apenas uma nova flag opcional é adicionada.
