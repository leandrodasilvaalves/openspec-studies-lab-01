# Tasks

## 1. Aceitação da flag e ajuda

- [x] 1.1 Adicionar `--cpf` a `PersonFlags` em `CliDispatcher` e incluir casos de rejeição (`/person --no-punctuation`, `/contact --cpf`) nos testes de flags; verificar com `dotnet test --filter "FullyQualifiedName~UnsupportedFlagReturnsInvalidArguments"`.
        - [x] 1.2 Atualizar a linha de `/person` em `WriteUsage` para documentar `[--cpf]` e ajustar `HelpReturnsSuccessAndListsCommandsAndFlags` para exigir `--cpf` na saída; verificar com `dotnet test --filter "FullyQualifiedName~HelpReturnsSuccessAndListsCommandsAndFlags"`.

## 2. Saída de pessoa com CPF

- [x] 2.1 Em `WritePerson`, quando `--cpf` estiver presente, gerar o CPF com `BrazilianDocumentGenerator.GenerateCpfDigits` e imprimir `cpf: ` com `FormatCpf` após o bloco de contato; verificar que `/person --cpf` produz exatamente `first_name`, `last_name`, `full_name`, `cpf` nessa ordem em teste novo.
- [x] 2.2 Cobrir validade e pontuação do CPF no teste de `/person --cpf`, usando `DocumentValidator.IsValidCpf` e o padrão `^\d{3}\.\d{3}\.\d{3}-\d{2}$`; verificar com `dotnet test --filter "FullyQualifiedName~PersonWithCpf"`.
- [x] 2.3 Adicionar testes de combinação: `/person --uppercase --cpf` mantém pessoa em caixa alta e CPF válido; `/person --contact --cpf` e `/person --cpf --contact` produzem as seis linhas na mesma ordem, sem duplicatas; verificar com `dotnet test --filter "FullyQualifiedName~PersonWithCpf"`.
- [x] 2.4 Garantir que `/person` sem `--cpf` continua sem a linha `cpf` e que `/person --contact` permanece inalterado; verificar com os testes existentes de pessoa e contato.
- [x] 2.5 Verificar que `/person --cpf --no-punctuation` retorna código `2`, escreve erro/uso em stderr e não gera dados, por meio de teste dedicado.

## 3. Verificação integrada

- [x] 3.1 Executar `dotnet build` e `dotnet test` completos e confirmar zero falhas.

## Workflow follow-up

- Arquivar a mudança após a revisão do projeto ser aprovada.
- Atualizar os specs principais e `AGENTS.md` conforme o fluxo de pós-arquivamento.
