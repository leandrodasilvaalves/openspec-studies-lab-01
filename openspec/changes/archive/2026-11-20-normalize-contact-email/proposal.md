# Proposal

## Why

Os comandos `/contact` e `/person --contact` geram e-mails com acentos e letras maiúsculas herdados dos dados pt_BR do Bogus (por exemplo, `José1234@Exemplo.com`). Endereços com diacríticos não são válidos em uso real e quebram comparações, cadastros e integrações que esperam e-mail normalizado.

## What Changes

- Normalizar todo e-mail gerado por `/contact` e por `/person --contact`: remover diacríticos (por exemplo, `ç` → `c`, `é` → `e`) e converter para minúsculas.
- Em `/person --uppercase --contact`, o e-mail passa a ser retornado inteiro em maiúsculas e sem diacríticos, acompanhando a caixa alta dos dados de pessoa.
- Manter inalterados o formato do e-mail (`<first_name><4 dígitos>@<domínio>`), a validade do formato de endereço e todos os demais campos e comportamentos existentes.
- `/contact` continua sem aceitar `--uppercase`; nele o e-mail é sempre minúsculo e sem diacríticos.

## Capabilities

### New Capabilities

Nenhuma.

### Modified Capabilities

- `person-contact-data`: o e-mail de `/contact` e de `/person --contact` passa a ser sempre sem diacríticos e com caixa definida (minúscula por padrão, maiúscula com `--uppercase`); os cenários de coerência entre e-mail e `first_name` são ajustados para considerar a remoção de diacríticos.

## Impact

- Código afetado: `src/FakeDataCli/Generators/ContactGenerator.cs` (normalização do e-mail) e `src/FakeDataCli/CliDispatcher.cs` (propagação de `--uppercase` para o gerador em `/person`).
- Testes afetados: `tests/FakeDataCli.Tests/PersonContactGeneratorTests.cs` e `tests/FakeDataCli.Tests/CliDispatcherTests.cs`.
- Dependências: nenhuma nova; a remoção de diacríticos usa `System.Text.NormalizationForm`, já disponível no runtime.
- Sem quebra de contrato de CLI: comandos, flags, rótulos e códigos de saída permanecem iguais.
