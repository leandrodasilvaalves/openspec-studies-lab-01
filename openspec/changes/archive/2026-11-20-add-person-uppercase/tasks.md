# Tasks

## 1. Gerador de pessoa

- [x] 1.1 Adicionar parâmetro opcional `uppercase` (default `false`) a `PersonGenerator.Generate`, aplicando `ToUpperInvariant()` a `FirstName`, `LastName` e `FullName` quando true, e verificar com teste xUnit que o padrão permanece inalterado e que a flag produz caixa alta
- [x] 1.2 Garantir que `FullName` permaneça a composição de `FirstName` e `LastName` em ambos os modos e verificar com teste xUnit

## 2. Despacho de comandos

- [x] 2.1 Adicionar `--uppercase` à lista de flags aceitas por `/person` em `CliDispatcher` e propagar para `WritePerson`/`PersonGenerator.Generate`, e verificar com `dotnet run --project src/FakeDataCli -- /person --uppercase` que os valores saem em caixa alta
- [x] 2.2 Atualizar a linha de uso de `/person` para `/person [--uppercase]` e verificar que `/help` lista `--uppercase`
- [x] 2.3 Ajustar o cenário de flag não suportada para não usar `/person --uppercase` (ex.: `/person --no-punctuation`) e verificar que retorna código 2 com mensagem de uso

## 3. Verificação

- [x] 3.1 Executar `dotnet test` na solução e verificar que todos os testes passam
- [x] 3.2 Executar smoke de `/person`, `/person --uppercase`, `/help` e `/guid --uppercase` e verificar saída e códigos de saída

## Workflow follow-up

- Arquivar o change após validação e sincronizar as specs principais.
- Verificar o resultado arquivado.
