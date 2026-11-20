# Tasks

## 1. Normalização no gerador de contato

- [x] 1.1 Em `src/FakeDataCli/Generators/ContactGenerator.cs`, adicionar um método privado que remove diacríticos por decomposição Unicode (`FormD`, descartar `UnicodeCategory.NonSpacingMark`, recompor em `FormC`) e aplica caixa invariante ao e-mail completo; verificar que `dotnet build` compila sem avisos novos
- [x] 1.2 Fazer `Generate()` retornar o e-mail sempre em minúsculas e sem diacríticos; verificar com teste unitário que o e-mail gerado é ASCII, não muda ao aplicar `ToLowerInvariant` e não sofre alteração ao remover diacríticos novamente
- [x] 1.3 Alterar `GenerateForPerson(PersonData person, bool uppercase = false)` para normalizar o e-mail conforme `uppercase`; verificar com `PersonData("José", "Conceição", ...)` que o prefixo é `jose` por padrão e `JOSE` com `uppercase: true`, mantendo o padrão `<prefixo><4 dígitos>@<domínio>`
- [x] 1.4 Atualizar `tests/FakeDataCli.Tests/PersonContactGeneratorTests.cs` com os casos determinísticos acima e asserções de ausência de diacríticos no laço de `/contact`; verificar que `dotnet test --filter "FullyQualifiedName~PersonContactGeneratorTests"` passa

## 2. Propagação de `--uppercase` na CLI

- [x] 2.1 Em `src/FakeDataCli/CliDispatcher.cs`, passar a flag `uppercase` já calculada em `WritePerson` para `GenerateForPerson(person, uppercase)`; verificar que `dotnet build` compila
- [x] 2.2 Em `tests/FakeDataCli.Tests/CliDispatcherTests.cs`, adicionar cenários para `/contact`, `/person --contact` e `/person --uppercase --contact` verificando respectivamente: e-mail minúsculo e sem diacríticos, e-mail minúsculo e sem diacríticos com prefixo igual ao `first_name` normalizado, e e-mail inteiro em maiúsculas e sem diacríticos; verificar que o teste direcionado passa
- [x] 2.3 Confirmar que `/contact --uppercase` continua rejeitado com código de saída `2` e que a ordem das linhas de `/person --contact` permanece `first_name`, `last_name`, `full_name`, `phone`, `email`; verificar pelos testes da CLI

## 3. Verificação integrada

- [x] 3.1 Executar `dotnet test` e confirmar que toda a suíte passa
- [x] 3.2 Executar `dotnet run --project src/FakeDataCli -- /contact` e `dotnet run --project src/FakeDataCli -- /person --uppercase --contact` algumas vezes e confirmar visualmente que nenhum e-mail contém acentos e que a caixa corresponde à flag

## Workflow follow-up

- Revisar e atualizar `AGENTS.md` se a normalização de e-mail alterar o contrato documentado da CLI.
- Arquivar a mudança após a revisão e o commit.
